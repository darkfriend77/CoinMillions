// <copyright file="BitcoinClient.cs" company="CoinMillions">
// Copyright (c) 2013 CoinMillions. All rights reserved.
// </copyright>
// <author>superreeen</author>
// <date>08.11.2013</date>
// <summary>Implements the bitcoin client class</summary>
namespace CoinMillions.BitcoinClient
{
    using CoinMillions.BitcoinClient.Data;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    /// <summary> A bitcoin client. </summary>
    /// <remarks> superreeen, 08.11.2013. </remarks>
    /// <seealso cref="T:System.IDisposable"/>
    public class BitcoinClient : IDisposable
    {
        #region Fields
        /// <summary> The client. </summary>
        private WebClient m_Client = new WebClient();
        /// <summary> true if disposed. </summary>
        private bool m_Disposed = false;
        /// <summary> The host. </summary>
        private Uri m_Host;
        /// <summary> The user. </summary>
        private string m_User;
        /// <summary> The password. </summary>
        private string m_Password;
        #endregion

        #region Constructors

        /// <summary> Prevents a default instance of the BitcoinClient class from being created. </summary>
        /// <remarks> superreeen, 08.11.2013. </remarks>
        private BitcoinClient() { }

        /// <summary> Initializes a new instance of the BitcoinClient class. </summary>
        /// <remarks> superreeen, 08.11.2013. </remarks>
        /// <exception cref="ArgumentOutOfRangeException"> Thrown when one or more arguments are outside the required range. </exception>
        /// <exception cref="ArgumentNullException"> Thrown when one or more required arguments are null. </exception>
        /// <param name="host"> The host. </param>
        /// <param name="user"> The user. </param>
        /// <param name="password"> The password. </param>
        public BitcoinClient(Uri host, string user, string password)
        {
            if (string.IsNullOrWhiteSpace(user))
                throw new ArgumentOutOfRangeException("user", "Please specify a user.");
            if (string.IsNullOrEmpty(password))
                throw new ArgumentOutOfRangeException("password", "Please specify a password.");
            if (host == null)
                throw new ArgumentNullException("host", "Please specify a valid host");

            m_Host = host;
            m_User = user;
            m_Password = password;
            m_Client.Credentials = new NetworkCredential(user, password);
            m_Client.Headers.Add(HttpRequestHeader.ContentType, "application/json-rpc");
        }
        #endregion

        #region Public Methods
        /// <summary> Sign raw transaction. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <param name="rawtransaction"> The rawtransaction. </param>
        /// <returns> A SignedRawTransaction. </returns>
        public SignedRawTransaction SignRawTransaction(string rawtransaction)
        {
            return QueryServer(MethodName.SignRawTransaction, rawtransaction)["result"].ToObject<SignedRawTransaction>();
        }

        /// <summary> Creates raw transaction. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <param name="inputs"> The inputs. </param>
        /// <param name="targets"> The targets. </param>
        /// <returns> The new raw transaction. </returns>
        public string CreateRawTransaction(List<UnspentInput> inputs, List<RawTarget> targets)
        {
            JArray jArray = new JArray();

            foreach (UnspentInput input in inputs)
            {
                JObject jObjectTx = new JObject();

                jObjectTx.Add("txid", input.TxId);
                jObjectTx.Add("vout", input.VOut);

                jArray.Add(jObjectTx);
            }

            JObject jObjectAdress = new JObject();
            foreach (RawTarget target in targets)
            {
                jObjectAdress.Add(target.Address, target.Amount);    
            }

            return QueryServer(MethodName.CreateRawTransaction, jArray, jObjectAdress)["result"].ToString();
        }

        /// <summary> Sends a raw transaction. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <param name="rawTrans"> The raw transaction. </param>
        /// <returns> A string. </returns>
        public string SendRawTransaction(string rawTrans)
        {
            return QueryServer(MethodName.SendRawTransaction, rawTrans)["result"].ToString();
        }

        /// <summary> Sends to address. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <param name="bitcoinAddress"> The bitcoin address. </param>
        /// <param name="amount"> The amount. </param>
        /// <returns> A string. </returns>
        public string SendToAddress(string bitcoinAddress, decimal amount)
        {
            return QueryServer(MethodName.SendToAddress, bitcoinAddress, amount.ToBitcoinValue())["result"].ToString();
        }

        /// <summary> Validates the address described by bitcoinAddress. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <param name="bitcoinAddress"> The bitcoin address. </param>
        /// <returns> The ValidatedAddress. </returns>
        public ValidatedAddress ValidateAddress(string bitcoinAddress)
        {
            return QueryServer(MethodName.ValidateAddress, bitcoinAddress)["result"].ToObject<ValidatedAddress>();
        }

        /// <summary> List unspent. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <returns> A List&lt;UnspentInput&gt; </returns>
        public List<UnspentInput> ListUnspent()
        {
            return QueryServer(MethodName.ListUnspent)["result"].ToObject<List<UnspentInput>>();
        }

        /// <summary> List transactions. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <param name="account"> (Optional) The account. </param>
        /// <param name="count"> (Optional) number of. </param>
        /// <param name="from"> (Optional) source for the. </param>
        /// <returns> A List&lt;TransactionOverview&gt; </returns>
        public List<TransactionOverview> ListTransactions(string account = "", int count = 999999, int from = 0)
        {
            return QueryServer(MethodName.ListTransactions, account, count, from)["result"].ToObject<List<TransactionOverview>>();
        }

        /// <summary> Gets a transaction. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <param name="txid"> The txid. </param>
        /// <returns> The transaction. </returns>
        public Transaction GetTransaction(string txid)
        {
            return QueryServer(MethodName.GetTransaction, txid)["result"].ToObject<Transaction>();
        }

        /// <summary> Gets addresses by account. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <param name="account"> The account. </param>
        /// <returns> The addresses by account. </returns>
        public List<string> GetAddressesByAccount(string account)
        {
            return QueryServer(MethodName.GetAddressesByAccount, account)["result"].ToObject<List<string>>();
        }

        /// <summary> Gets received by account. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <param name="account"> The account. </param>
        /// <returns> The received by account. </returns>
        public decimal GetReceivedByAccount(string account)
        {
            return QueryServer(MethodName.GetReceivedByAccount, account)["result"].ToObject<decimal>();
        }

        /// <summary> Gets received by address. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <param name="bitcoinAddress"> The bitcoin address. </param>
        /// <returns> The received by address. </returns>
        public decimal GetReceivedByAddress(string bitcoinAddress)
        {
            return QueryServer(MethodName.GetReceivedByAddress, bitcoinAddress)["result"].ToObject<decimal>();
        }

        /// <summary> Gets the information. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <returns> The information. </returns>
        public Info GetInfo()
        {
            return QueryServer(MethodName.GetInfo)["result"].ToObject<Info>();
        }

        /// <summary> Gets the difficulty. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <returns> The difficulty. </returns>
        public decimal GetDifficulty()
        {
            return QueryServer(MethodName.GetDifficulty)["result"].ToObject<decimal>();
        }

        /// <summary> Gets block count. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <returns> The block count. </returns>
        public ulong GetBlockCount()
        {
            return QueryServer(MethodName.GetBlockCount)["result"].ToObject<ulong>();
        }

        /// <summary> Gets block hash. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <param name="index"> Zero-based index of the. </param>
        /// <returns> The block hash. </returns>
        public string GetBlockHash(ulong index)
        {
            return QueryServer(MethodName.GetBlockHash, index)["result"].ToString();
        }

        /// <summary> Gets an account. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <param name="bitcoinAddress"> The bitcoin address. </param>
        /// <returns> The account. </returns>
        public string GetAccount(string bitcoinAddress)
        {
            return QueryServer(MethodName.GetAccount, bitcoinAddress)["result"].ToString();
        }

        /// <summary> Queries raw transaction. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <param name="txid"> The txid. </param>
        /// <returns> The raw transaction. </returns>
        public RawTransaction QueryRawTransaction(string txid)
        {
            var rawTransactionString = GetRawTransaction(txid);
            return DecodeRawTransaction(rawTransactionString);
        }

        /// <summary> Decode raw transaction. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <param name="rawTransaction"> The raw transaction. </param>
        /// <returns> A RawTransaction. </returns>
        public RawTransaction DecodeRawTransaction(string rawTransaction)
        {
            return QueryServer(MethodName.DecodeRawTransaction, rawTransaction)["result"].ToObject<RawTransaction>();
        }

        /// <summary> Gets the balance. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <returns> The balance. </returns>
        public string GetBalance()
        {
            return QueryServer(MethodName.GetBalance)["result"].ToString();
        }

        /// <summary> Gets raw transaction. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <param name="txid"> The txid. </param>
        /// <returns> The raw transaction. </returns>
        public string GetRawTransaction(string txid)
        {
            return QueryServer(MethodName.GetRawTransaction, txid)["result"].ToString();
        }

        /// <summary> Gets a block. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <param name="hash"> The hash. </param>
        /// <returns> The block. </returns>
        public Block GetBlock(string hash)
        {
            return QueryServer(MethodName.GetBlock, hash)["result"].ToObject<Block>();
        }
        #endregion

        #region Private Methods

        /// <summary> Queries a server. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <param name="methodName"> Name of the method. </param>
        /// <param name="args"> A variable-length parameters list containing arguments. </param>
        /// <returns> The server. </returns>
        private JObject QueryServer(MethodName methodName, params object[] args)
        {
            var request = BuildRequestJObject(methodName, args);
            var requestString = JsonConvert.SerializeObject(request);
            var responseString = m_Client.UploadString(m_Host, "POST", requestString);
            var response = (JObject)JsonConvert.DeserializeObject(responseString);
            return response;
        }

        /// <summary> Builds request j object. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <param name="methodName"> Name of the method. </param>
        /// <param name="args"> A variable-length parameters list containing arguments. </param>
        /// <returns> A JObject. </returns>
        private static JObject BuildRequestJObject(MethodName methodName, params object[] args)
        {
            JObject joe = new JObject();
            joe.Add(new JProperty("jsonrpc", "1.0"));
            joe.Add(new JProperty("id", "1"));
            joe.Add(new JProperty("method", methodName.ToString().ToLower()));

            // adds provided paramters
            joe.Add(new JProperty("params", args));

            return joe;
        }
        #endregion

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

        /// <summary> Finalizes an instance of the BitcoinClient class. </summary>
        /// <remarks> superreeen, 08.11.2013. </remarks>
        ~BitcoinClient()
        {
            Dispose(false);
        }

        /// <summary> Releases the unmanaged resources used by the BitcoinClient and optionally releases the managed resources. </summary>
        /// <remarks> superreeen, 08.11.2013. </remarks>
        /// <param name="disposing"> true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_Host = null;
                m_User = null;
                m_Password = null;
                if (m_Client != null)
                {
                    m_Client.Dispose();
                    m_Client = null;
                }
            }
        }
        #endregion
    }
}
