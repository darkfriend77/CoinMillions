// <copyright file="Ticket.cs" company="CoinMillions">
// Copyright (c) 2013 CoinMillions. All rights reserved.
// </copyright>
// <author>superreeen</author>
// <date>09.11.2013</date>
// <summary>Implements the ticket class</summary>
namespace CoinMillions.Service.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CoinMillions.BitcoinClient;
    using CoinMillions.BitcoinClient.Data;
    using MathNet.Numerics;
using log4net;

    /// <summary> A ticket. </summary>
    /// <remarks> superreeen, 09.11.2013. </remarks>
    public class Ticket
    {
        private const int Numbers = 5;
        private const int MaxNumbers = 21;
        private const int Stars = 2;
        private const int MaxStars = 9;

        private static ILog m_Log = LogManager.GetLogger(typeof(Ticket));

        private readonly static Dictionary<ulong, int[]> m_TicketPool;
        private readonly static List<PossibleLot> m_PossibleLots;

        /// <summary> Initializes static members of the Ticket class. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        static Ticket()
        {
            m_TicketPool = GetTicketPool();
            m_PossibleLots = GetPossibleLots();
        }

        /// <summary> Gets ticket pool. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <returns> The ticket pool. </returns>
        private static Dictionary<ulong, int[]> GetTicketPool()
        {
            Dictionary<ulong, int[]> ticketPool = new Dictionary<ulong, int[]>();
            ulong count = 0;

            // numbers
            for (int a = 1; a < MaxNumbers - Numbers + 2; a++)
                for (int b = a + 1; b < MaxNumbers - Numbers + 3; b++)
                    for (int c = b + 1; c < MaxNumbers - Numbers + 4; c++)
                        for (int d = c + 1; d < MaxNumbers - Numbers + 5; d++)
                            for (int e = d + 1; e < MaxNumbers - Numbers + 6; e++)

                                // stars
                                for (int s = 1; s < MaxStars - Stars + 4; s++)
                                    for (int t = s + 1; t < MaxStars - Stars + 3; t++)
                                        ticketPool.Add(count++, new int[] { a, b, c, d, e, s, t });

            return ticketPool;
        }

        /// <summary> Ticket from amount. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <exception cref="ArgumentOutOfRangeException"> Thrown when one or more arguments are outside the required range. </exception>
        /// <param name="amount"> The amount. </param>
        /// <returns> An int[]. </returns>
        private static int[] TicketFromAmount(decimal amount)
        {
            ulong key = Convert.ToUInt64((decimal)Math.Pow(10, 6) - ((amount - 0.01M) * (decimal)Math.Pow(10, 8)));
            if (!m_TicketPool.ContainsKey(key))
                throw new ArgumentOutOfRangeException("amount", "No valid Ticket specified by the passed amount.");
            return m_TicketPool[key];
        }

        /// <summary> Gets split hash lenght. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <param name="hash"> The hash to split. </param>
        /// <returns> The split hash lenght. </returns>
        private static int GetSplitHashLenght(string hash)
        {
            return int.Parse(hash.Substring(0, 4), System.Globalization.NumberStyles.HexNumber) % 5;
        }

        /// <summary> Ticket from hash. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <param name="hash"> The hash source hash. </param>
        /// <returns> An int[]. </returns>
        internal static int[] TicketFromHash(string hash)
        {
            ulong nummer = 0;
            string splitHash = "";

            int splitHashLenght = GetSplitHashLenght(hash);

            foreach (char t in hash)
            {
                splitHash += t;
                if (splitHash.Length > (11 + splitHashLenght))
                {
                    nummer += ulong.Parse(splitHash, System.Globalization.NumberStyles.HexNumber);
                    splitHash = "";
                }
            }

            if (splitHash.Length > 0)
                nummer += ulong.Parse(splitHash, System.Globalization.NumberStyles.HexNumber);

            ulong key = nummer % (ulong)m_TicketPool.Count;

            return m_TicketPool[key];
        }

        /// <summary> Gets the bill numbers. </summary>
        /// <value> The bill numbers. </value>
        public int[] BillNumbers
        {
            get
            {
                if (Amount != 0.0102M)
                    return TicketFromAmount(Amount);
                else
                    return TicketFromHash(Transaction);
            }
        }

        /// <summary> Gets the amount. </summary>
        /// <value> The amount. </value>
        public decimal Amount { get; private set; }

        /// <summary> Gets the source for the. </summary>
        /// <value> The source. </value>
        public string Source { get; private set; }

        /// <summary> Gets the transaction. </summary>
        /// <value> The transaction. </value>
        public string Transaction { get; private set; }

        /// <summary> Gets the lot. </summary>
        /// <value> The lot. </value>
        public PossibleLot Lot { get; private set; }

        /// <summary> Gets found numbers. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <param name="winningTicket"> The winning ticket. </param>
        /// <returns> The found numbers. </returns>
        public void UpdateLot(int[] winningTicket)
        {
            int[] currentNumbers = new int[Numbers];
            int[] winningNumbers = new int[Numbers];

            int[] currentStars = new int[Stars];
            int[] winningStars = new int[Stars];
            
            Array.Copy(winningTicket, 0, winningNumbers, 0, Numbers);
            Array.Copy(BillNumbers, 0, currentNumbers, 0, Numbers);
            Array.Copy(winningTicket, 5, winningStars, 0, Stars);
            Array.Copy(BillNumbers, 5, currentStars, 0, Stars);
            m_Log.InfoFormat("Updateing lot for {0}:{1} with Numbers {2} against {3}.", Source, Transaction, String.Join(",", new List<int>(BillNumbers).ConvertAll(i => i.ToString()).ToArray()), new List<int>(winningTicket).ConvertAll(i => i.ToString()).ToArray());
            Lot = m_PossibleLots.Where(l => l.Numbers == GetRightNumbersCount(winningNumbers, currentNumbers) && l.Stars == GetRightNumbersCount(winningStars, currentStars)).First();
            m_Log.InfoFormat("The lot for {0}:{1} is {2}.", Source, Transaction, Lot);
        }

        /// <summary> Gets right numbers count. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <param name="winningNumbers"> The winning numbers. </param>
        /// <param name="currentNumbers"> The current numbers. </param>
        /// <returns> The right numbers count. </returns>
        private static int GetRightNumbersCount(int[] winningNumbers, int[] currentNumbers)
        {
            int count = 0;
            foreach (var winningNumber in winningNumbers)
                foreach (var currentNumber in currentNumbers)
                    if (winningNumber == currentNumber)
                        count++;
            return count;
        }

        /// <summary> Gets possible lots. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <returns> The possible lots. </returns>
        private static List<PossibleLot> GetPossibleLots()
        {
            List<PossibleLot> findings = new List<PossibleLot>();

            decimal gain = 1;
            decimal raiser = 1;
            decimal totalGain = 0;
            decimal gainStepAllocate = 0;
            decimal gainAllocate = 0;

            for (int n = 5; n >= 0; n--)
            {
                gainAllocate = (gain * (n)) / (n + raiser);
                gain -= gainAllocate;

                for (int s = 2; s >= 0; s--)
                {
                    gainStepAllocate = (gainAllocate * (s + 2)) / (s + raiser + 2);
                    gainAllocate -= gainStepAllocate;
                    totalGain += gainStepAllocate;

                    raiser += 2;

                    findings.Add(new PossibleLot()
                    {
                        Numbers = n,
                        Stars = s,
                        Probability = (decimal)(SpecialFunctions.Binomial(Numbers, n)
                                    * SpecialFunctions.Binomial(MaxNumbers - Numbers, Numbers - n)
                                    / SpecialFunctions.Binomial(MaxNumbers, Numbers)
                                    * SpecialFunctions.Binomial(Stars, s)
                                    * SpecialFunctions.Binomial(MaxStars - Stars, Stars - s)
                                    / SpecialFunctions.Binomial(MaxStars, Stars)),
                        Gain = gainStepAllocate
                    });
                }
            }
            return findings;
        }

        /// <summary> From inputs. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <param name="client"> The client. </param>
        /// <param name="betAddress"> The bet address. </param>
        /// <param name="inputs"> The inputs. </param>
        /// <returns> A List&lt;Ticket&gt; </returns>
        public static List<Ticket> FromInputs(BitcoinClient client, string betAddress, List<UnspentInput> inputs)
        {
            var tickets = inputs.SelectMany(input =>
            {
                var raw = client.QueryRawTransaction(input.TxId);

                var transin = raw.Vin.Select(o => client.QueryRawTransaction(o.TxId)).ToList();
                var tick = transin.Select(o =>
                {
                    var amount = o.Vout.Where(v => v.ScriptPubKey.Addresses.Contains(betAddress)).Select(v => v.Value).First();
                    var originputs = o.Vin.SelectMany(v => client.QueryRawTransaction(v.TxId).Vout.Where(output => output.N == v.Vout));
                    string source = originputs.OrderByDescending(v => v.Value).SelectMany(v => v.ScriptPubKey.Addresses).First();
                    return new Ticket() { Amount = amount, Source = source, Transaction = o.TxId };
                });
                return tick;
            }).ToList();
            return tickets;
        }
    }
}