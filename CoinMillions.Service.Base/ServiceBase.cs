﻿// <copyright file="servicebase.cs" company="CoinMillions">
// Copyright (c) 2013 CoinMillions. All rights reserved.
// </copyright>
// <author>superreeen</author>
// <date>10.11.2013</date>
// <summary>Implements the servicebase class</summary>
namespace CoinMillions.Service.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CoinMillions.BitcoinClient;
    using CoinMillions.BitcoinClient.Data;
    using log4net;

    /// <summary> A service base. </summary>
    /// <remarks> superreeen, 10.11.2013. </remarks>
    /// <seealso cref="T:System.IDisposable"/>
    public class ServiceBase : IDisposable
    {
        /// <summary> The bet account. </summary>
        private const string BetAccount = "Bets";
        /// <summary> The pot account. </summary>
        private const string PotAccount = "Pot";
        /// <summary> The own account. </summary>
        private const string OwnAccount = "Own";
        /// <summary> The jackpot account. </summary>
        private const string JackpotAccount = "Jackpot";

        /// <summary> The bet address. </summary>
        private const string BetAddress = "mz7dc4UqEmmekLuDQcb6PCngFLHKhYAsQ4";
        /// <summary> The pot address. </summary>
        private const string PotAddress = "mo6xVdPofLjr516WdQp3UQp94fpFp6sLLf";
        /// <summary> The own address. </summary>
        private const string OwnAddress = "n2RGZCWuX8VytneKLwQYxAoQt2nwM8e2rt";
        /// <summary> The jackpot address. </summary>
        private const string JackpotAddress = "mnLjgQTZEnJ9STCCGFd2u3G96dahrHS6SC";

        /// <summary> The house fee. </summary>
        private const decimal HouseFee = 0.005M;
        /// <summary> The network fee. </summary>
        private const decimal NetworkFee = 0.0001M;
        /// <summary> The dust amount. </summary>
        private const decimal DustAmount = 0.00005430M;

        /// <summary> The log. </summary>
        private ILog m_Log = LogManager.GetLogger(typeof(ServiceBase));
        /// <summary> The client. </summary>
        private BitcoinClient m_Client;
        /// <summary> true if disposed. </summary>
        private bool m_Disposed = false;

        /// <summary> Initializes a new instance of the ServiceBase class. </summary>
        /// <remarks> superreeen, 10.11.2013. </remarks>
        /// <param name="host"> The host. </param>
        /// <param name="user"> The user. </param>
        /// <param name="password"> The password. </param>
        public ServiceBase(Uri host, string user, string password)
        {
            m_Client = new BitcoinClient(host, user, password);
            m_Client.NewBlockFound += NewBlockFound;
        }

        /// <summary> Creates a new block found. </summary>
        /// <remarks> superreeen, 10.11.2013. </remarks>
        /// <param name="sender"> Source of the event. </param>
        /// <param name="e"> Event information. </param>
        private void NewBlockFound(object sender, EventArgs e)
        {
            var blockCount = m_Client.GetBlockCount();
            ProcessBets();

            if (blockCount % 144 == 0)
                ProcessDraw();
        }

        /// <summary> Prevents a default instance of the ServiceBase class from being created. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        private ServiceBase()
        {

        }

        /// <summary> Applies the fee. </summary>
        /// <remarks> superreeen, 10.11.2013. </remarks>
        /// <exception cref="ObjectDisposedException"> Thrown when a supplied object has been disposed. </exception>
        /// <param name="address"> The address. </param>
        /// <param name="inputs"> The inputs. </param>
        /// <param name="outputs"> The outputs. </param>
        private void ApplyFee(string address, List<UnspentInput> inputs, Dictionary<string, decimal> outputs)
        {
            if (m_Disposed)
                throw new ObjectDisposedException("Object already disposed.");

            if (outputs.Count > 0 && inputs.Count > 0)
            {
                int size = (int)Math.Ceiling((inputs.Count * 148 + outputs.Count * 34 + 10 + inputs.Count) / 1024M);
                decimal fee = size * NetworkFee;
                m_Log.InfoFormat("Deducting fee {0} from {1}.", fee, address);
                outputs.AddOrUpdate(address, v => v - fee);
            }
        }

        /// <summary> Gets bet validation output. </summary>
        /// <remarks> superreeen, 10.11.2013. </remarks>
        /// <exception cref="ObjectDisposedException"> Thrown when a supplied object has been disposed. </exception>
        /// <param name="inputs"> The inputs. </param>
        /// <returns> The bet validation output. </returns>
        private Dictionary<string, decimal> GetBetValidationOutput(List<UnspentInput> inputs)
        {
            if (m_Disposed)
                throw new ObjectDisposedException("Object already disposed.");

            Dictionary<string, decimal> outputs = new Dictionary<string, decimal>();

            foreach (var item in inputs)
            {
                var raw = m_Client.QueryRawTransaction(item.TxId);
                var trans = m_Client.GetTransaction(item.TxId);
                decimal input = item.Amount;
                if (input > 0.01M && input <= 0.01989999M)
                {
                    m_Log.InfoFormat("Calculateing values for {0}.", trans.TxId);

                    decimal bet = 0.01M;
                    decimal house = bet * HouseFee;
                    decimal pot = bet - house;
                    decimal change = input - bet;

                    var transin = raw.Vin.SelectMany(o => m_Client.QueryRawTransaction(o.TxId).Vout.Where(v => v.N == o.Vout));
                    string source = transin.OrderByDescending(v => v.Value).SelectMany(v => v.ScriptPubKey.Addresses).First();

                    m_Log.InfoFormat("Distributing bet {0} to {1}(Pot): {2}, {3}(Own): {4}, {5}(Source): {6}.", trans.TxId, PotAddress, pot, OwnAddress, house, source, change);
                    outputs.AddOrUpdate(OwnAddress, v => v + house);
                    outputs.AddOrUpdate(PotAddress, v => v + pot);
                    outputs.AddOrUpdate(source, v => v + change);
                }
                else
                {
                    m_Log.InfoFormat("Absorbing invalid bet {0} with amount {1}.", trans.TxId, input);
                    outputs.AddOrUpdate(OwnAddress, v => v + input);
                }
            }
            ApplyFee(PotAddress, inputs, outputs);
            return outputs;
        }

        /// <summary> Gets the targets. </summary>
        /// <remarks> superreeen, 10.11.2013. </remarks>
        /// <param name="outputs"> The outputs. </param>
        /// <returns> The targets. </returns>
        private static List<RawTarget> GetTargets(Dictionary<string, decimal> outputs)
        {
            List<RawTarget> targets = new List<RawTarget>();
            foreach (KeyValuePair<string, decimal> output in outputs)
            {
                targets.Add(new RawTarget() { Address = output.Key, Amount = output.Value });
            }
            return targets;
        }

        /// <summary> Process the bets. </summary>
        /// <remarks> superreeen, 10.11.2013. </remarks>
        /// <exception cref="ObjectDisposedException"> Thrown when a supplied object has been disposed. </exception>
        public void ProcessBets()
        {
            if (m_Disposed)
                throw new ObjectDisposedException("Object already disposed.");

            m_Log.Info("Starting Processing of Bets.");
            List<UnspentInput> inputs = m_Client.ListUnspent().Where(u => u.Address.Equals(BetAddress)).ToList();
            m_Log.InfoFormat("Got {0} unspent inputs.", inputs.Count);
            Dictionary<string, decimal> outputs = GetBetValidationOutput(inputs);

            List<RawTarget> targets = GetTargets(outputs);

            if (targets.Count > 0)
            {
                var rawTrans = m_Client.CreateRawTransaction(inputs, targets);
                var signedRawTrans = m_Client.SignRawTransaction(rawTrans);
                var sentRawTrans = m_Client.SendRawTransaction(signedRawTrans.Hex);
            }
            m_Log.Info("Finished Processing of Bets.");
        }

        /// <summary> Gets the jackpot. </summary>
        /// <value> The jackpot. </value>
        public decimal Jackpot
        {
            get
            {
                if (m_Disposed)
                    throw new ObjectDisposedException("Object already disposed.");

                var unspent = m_Client.ListUnspent();
                var pot = unspent.Where(u => u.Address.Equals(PotAddress) || u.Address.Equals(JackpotAddress));
                var jackpot = pot.Sum(p => p.Amount);
                return jackpot;
            }
        }

        /// <summary> Process the draw. </summary>
        /// <remarks> superreeen, 10.11.2013. </remarks>
        /// <exception cref="ObjectDisposedException"> Thrown when a supplied object has been disposed. </exception>
        public void ProcessDraw()
        {
            if (m_Disposed)
                throw new ObjectDisposedException("Object already disposed.");

            m_Log.Info("Starting Processing of a Draw.");
            List<UnspentInput> inputs = m_Client.ListUnspent().Where(u => u.Address.Equals(PotAddress)).ToList();
            List<UnspentInput> jinputs = m_Client.ListUnspent().Where(u => u.Address.Equals(JackpotAddress)).ToList();

            var tickets = Ticket.FromInputs(m_Client, BetAddress, inputs);
            ulong blockHeight = m_Client.GetBlockCount();
            int[] drawnNumbers = Ticket.TicketFromHash(m_Client.GetBlock(m_Client.GetBlockHash(blockHeight)).MerkleRoot);
            m_Log.InfoFormat("Lucky Number for Block {0}: {1}.", blockHeight, String.Join(",", new List<int>(drawnNumbers).ConvertAll(i => i.ToString()).ToArray()));
            
            foreach (var item in tickets)
            {
                item.UpdateLot(drawnNumbers);
            }
            var pot = Jackpot;
            var winningGroup = tickets.GroupBy(i => i.Lot).OrderByDescending(g => g.Key.Gain);

            Dictionary<string, decimal> payouts = new Dictionary<string, decimal>();
            foreach (var group in winningGroup)
            {
                if (group.Key.Gain > 0M)
                {
                    var lotPot = pot * group.Key.Gain;
                    var amountPerTicket = lotPot / group.LongCount();
                    m_Log.InfoFormat("Lot {0} has a pot of {1} for {2} makes {3} per winner.", group.Key, lotPot, group.LongCount(), amountPerTicket);
                    foreach (var ticket in group)
                    {
                        payouts.AddOrUpdate(ticket.Source, v => v + amountPerTicket);
                    }
                }
            }
            m_Log.InfoFormat("Removeing winning dust below {0}, sorry guys.", DustAmount);
            payouts = payouts.Where(p => p.Value > DustAmount).ToDictionary(p => p.Key, p => p.Value);
            decimal winnings = payouts.Sum(p => p.Value);
            decimal move = pot - winnings;
            m_Log.InfoFormat("Total payout amount {0}, move to Jackpot {1}.", winnings, move);
            payouts.AddOrUpdate(JackpotAddress, v => v + move);

            var realinputs = inputs.Union(jinputs).ToList();

            ApplyFee(JackpotAddress, realinputs, payouts);

            var targets = GetTargets(payouts);

            if (targets.Count > 0 && realinputs.Count > 0)
            {
                var rawTrans = m_Client.CreateRawTransaction(realinputs, targets);
                var signedRawTrans = m_Client.SignRawTransaction(rawTrans);
                var sentRawTrans = m_Client.SendRawTransaction(signedRawTrans.Hex);
            }
            m_Log.Info("Finished Processing of a Draw.");
        }

        #region IDisposable Implementation

        /// <summary> Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources. </summary>
        /// <remarks> superreeen, 08.11.2013. </remarks>
        /// <seealso cref="M:System.IDisposable.Dispose()"/>
        public void Dispose()
        {
            Dispose(true);
            m_Disposed = true;
            GC.SuppressFinalize(this);
        }

        /// <summary> Finalizes an instance of the ServiceBase class. </summary>
        /// <remarks> superreeen, 08.11.2013. </remarks>
        ~ServiceBase()
        {
            Dispose(false);
        }

        /// <summary> Releases the unmanaged resources used by the ServiceBase and optionally releases the managed resources. </summary>
        /// <remarks> superreeen, 08.11.2013. </remarks>
        /// <param name="disposing"> true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (m_Client != null)
                {
                    m_Client.Dispose();
                    m_Client = null;
                }
                m_Log = null;
            }
        }
        #endregion
    }
}