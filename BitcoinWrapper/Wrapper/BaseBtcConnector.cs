﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitcoinWrapper.Common;
using BitcoinWrapper.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BitcoinWrapper.Wrapper
{
    /// <summary>
    /// This connector implements all the original methods in the Bitcoin-qt API
    /// See more here: https://en.bitcoin.it/wiki/Original_Bitcoin_client/API_Calls_list
    /// </summary>
    public class BaseBtcConnector
    {
        public BaseConnector BaseConnector { get; set; }

        /// <summary>
        /// Starts connecting to the Bitcoin-qt server
        /// </summary>
        public BaseBtcConnector()
        {
            this.BaseConnector = new BaseConnector();
        }

        public float GetBalance()
        {
            var result = BaseConnector.RequestServer(MethodName.getbalance)["result"].ToString();
            float balance = 0.0f;
            float.TryParse(result, out balance);
            return balance;
        }

        public Block GetBlock(string hash)
        {
            Block block = BaseConnector.RequestServer(MethodName.getblock,hash)["result"].ToObject<Block>();
            return block;
        }

        public string GetRawTransaction(string txid)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.getrawtransaction,txid)["result"].ToString();
            return rawTransaction;
        }

        public JObject GetDecodedRawTransaction(string txid)
        {
            return BaseConnector.RequestServer(MethodName.decoderawtransaction, GetRawTransaction(txid));
        }

        public Transaction DecodeRawTransaction(string rawTransaction)
        {
            Transaction transaction = BaseConnector.RequestServer(MethodName.decoderawtransaction, rawTransaction)["result"].ToObject<Transaction>();
            return transaction;
        }

        public string GetAccount(string bitcoinAddress)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.getaccount, bitcoinAddress)["result"].ToString();
            return rawTransaction;
        }

        public int GetBlockCount()
        {
            var result = BaseConnector.RequestServer(MethodName.getblockcount)["result"].ToString();
            int balance = 0;
            int.TryParse(result, out balance);
            return balance;
        }

        public string GetBlockhash(int index)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.getblockhash,index)["result"].ToString();
            return rawTransaction;
        }

        public float GetDifficulty()
        {
            var result = BaseConnector.RequestServer(MethodName.getdifficulty)["result"].ToString();
            float balance = 0;
            float.TryParse(result, out balance);
            return balance;
        }

        public string GetGenerate()
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.getgenerate)["result"].ToString();
            return rawTransaction;
        }

        public int GetHashesPerSec()
        {
            var result = BaseConnector.RequestServer(MethodName.gethashespersec)["result"].ToString();
            int balance = 0;
            int.TryParse(result, out balance);
            return balance;
        }

        public string GetInfo()
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.getinfo)["result"].ToString();
            return rawTransaction;
        }

        public string GetMiningInfo()
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.getmininginfo)["result"].ToString();
            return rawTransaction;
        }

        public string GetPeerInfo()
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.getpeerinfo)["result"].ToString();
            return rawTransaction;
        }

        public string GetRawMemPool()
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.getrawmempool)["result"].ToString();
            return rawTransaction;
        }

        public string BackupWallet(string destination)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.backupwallet,destination)["result"].ToString();
            return rawTransaction;
        }

        public string DumpPrivKey(string bitcoinAddress)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.dumpprivkey, bitcoinAddress)["result"].ToString();
            return rawTransaction;
        }

        public string EncryptWallet(string passphrame)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.encryptwallet,passphrame)["result"].ToString();
            return rawTransaction;
        }

        public string GetAccountAddress(string account)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.getaccountaddress,account)["result"].ToString();
            return rawTransaction;
        }

        public string GetAddressesByAccount(string account)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.getaddressesbyaccount,account)["result"].ToString();
            return rawTransaction;
        }

        public string GetBlockTemplate()
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.getblocktemplate)["result"].ToString();
            return rawTransaction;
        }

        public int GetconnectionCount()
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.getconnectioncount)["result"].ToString();
            int count = 0;
            int.TryParse(rawTransaction, out count);
            return count;
        }

        public string GetNewAddress(string account)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.getnewaddress, account)["result"].ToString();
            return rawTransaction;
        }

        public string GetReceivedByAccount(string account)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.getreceivedbyaccount,account)["result"].ToString();
            return rawTransaction;
        }

        public string GetReceivedByAddress(string bitcoinAddress)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.getreceivedbyaddress, bitcoinAddress)["result"].ToString();
            return rawTransaction;
        }

        /// <summary>
        /// Only for in-wallet transactions. To find "all transactions", use GetRawTransaction and decude with DecodeRawTransaction
        /// </summary>
        /// <param name="txid"></param>
        /// <returns></returns>
        public InWalletTransaction GetTransaction(string txid)
        {
            InWalletTransaction transaction = BaseConnector.RequestServer(MethodName.gettransaction, txid)["result"].ToObject<InWalletTransaction>();
            return transaction;
        }

        public string GetWork()
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.getwork)["result"].ToString();
            return rawTransaction;
        }

        public string Help()
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.help)["result"].ToString();
            return rawTransaction;
        }

        public string ListAccounts()
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.listaccounts)["result"].ToString();
            return rawTransaction;
        }

        public string ListAddressGroupings()
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.listaddressgroupings)["result"].ToString();
            return rawTransaction;
        }

        public string ListReceivedByAccount()
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.listreceivedbyaccount)["result"].ToString();
            return rawTransaction;
        }

        public string ListReceivedByAddress()
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.listreceivedbyaddress)["result"].ToString();
            return rawTransaction;
        }

        public string ListSinceBlock()
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.listsinceblock)["result"].ToString();
            return rawTransaction;
        }

        public string ListTransactions()
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.listtransactions)["result"].ToString();
            return rawTransaction;
        }

        public List<InWalletTransaction> GetAllInWalletTransactions()
        {
            List<InWalletTransaction> listInWalletTransactions = BaseConnector.RequestServer(MethodName.listtransactions)["result"].ToObject<List<InWalletTransaction>>();
            return listInWalletTransactions;
        }

        public string ListLockUnspent()
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.listlockunspent)["result"].ToString();
            return rawTransaction;
        }

        public string SetTxFee(float amount)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.settxfee,amount)["result"].ToString();
            return rawTransaction;
        }

        public string WalletLock()
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.walletlock)["result"].ToString();
            return rawTransaction;
        }

        public string ValidateAddress(string bitcoinAddress)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.validateaddress,bitcoinAddress)["result"].ToString();
            return rawTransaction;
        }

        public string Stop()
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.stop)["result"].ToString();
            return rawTransaction;
        }

        public string SetGenerate(bool variable)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.setgenerate,variable)["result"].ToString();
            return rawTransaction;
        }
        
        /// <summary>
        /// not tested
        /// </summary>
        /// <param name="bitcoinAddress"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public string SendToAddress(string bitcoinAddress, float amount)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.sendtoaddress, new List<object>() {bitcoinAddress, amount })["result"].ToString();
            return rawTransaction;
        }

        public string SendRawTransaction(string rawTrans)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.sendrawtransaction, rawTrans)["result"].ToString();
            return rawTransaction;
        }

        public string AddNode(string node, NodeAction action)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.addnode, new List<object>() { node, action.ToString() })["result"].ToString();
            return rawTransaction;
        }

        public string GetAddedNodeInfo(string dns)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.getaddednodeinfo, dns)["result"].ToString();
            return rawTransaction;
        }

        public string GetTxOut(string txId, int n)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.gettxout, new List<object>() { txId,n})["result"].ToString();
            return rawTransaction;
        }

        public string GetTxOutSetInfo()
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.gettxoutsetinfo)["result"].ToString();
            return rawTransaction;
        }

        public string KeyPoolRefill()
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.keypoolrefill)["result"].ToString();
            return rawTransaction;
        }

        public string SendFrom(string fromAccount, string toBitcoinAddress, decimal amount)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.sendfrom, new List<object>() { fromAccount,toBitcoinAddress,amount})["result"].ToString();
            return rawTransaction;
        }

        public string SignMessage(string bitcoinAddress, string message)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.signmessage, new List<object>() { bitcoinAddress,message})["result"].ToString();
            return rawTransaction;
        }

        public string SubmitBlock(string hexData)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.submitblock, hexData)["result"].ToString();
            return rawTransaction;
        }

        public string VerifyMessage(string bitcoinAddress, string signature, string message)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.verifymessage, new List<object>() { bitcoinAddress,signature,message})["result"].ToString();
            return rawTransaction;
        }

        /// <summary>
        /// This opens the Bitcoin wallet to access methods which requires an open wallet
        /// </summary>
        /// <param name="passphrase">Password you've encrypted the wallet with</param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public string WalletPassphrase(string passphrase, int timeout)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.walletpassphrase, new List<object>() { passphrase,timeout})["result"].ToString();
            return rawTransaction;
        }

        public string WalletPassphraseChange(string oldpassphrase, string newpassphrase)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.walletpassphrasechange, new List<object>() { oldpassphrase, newpassphrase})["result"].ToString();
            return rawTransaction;
        }

        public string Move(string fromAccount, string toAccount, float amount)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.move, new List<object>() {fromAccount , toAccount, amount})["result"].ToString();
            return rawTransaction;
        }

        /// <summary>
        /// Create a transaction spending given inputs (array of objects containing transaction outputs to spend), sending to given address(es).
        /// Returns the hex-encoded transaction in a string. Note that the transaction's inputs are not signed, and it is not stored in the
        /// wallet or transmitted to the network.
        /// 
        /// Also note that NO transaction validity checks are done; it is easy to create invalid transactions or transactions that will not be
        /// relayed/mined by the network because they contain insufficient fees.
        /// 
        /// example. createrawtransaction [{"txid":txid,"vout":n},...] {address:amount,...}
        /// </summary>
        /// <param name="txid"></param>
        /// <param name="vout"></param>
        /// <param name="adress"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public string CreateRawTransaction(string txid, int vout, string adress, long amount)
        {
            JArray jArray = new JArray();

            JObject jObjectTx = new JObject();

            jObjectTx.Add("txid", txid);
            jObjectTx.Add("vout", vout);

            jArray.Add(jObjectTx);

            JObject jObjectAdress = new JObject();
            jObjectAdress.Add(adress, amount);

            return BaseConnector.RequestServer(MethodName.createrawtransaction, new List<object>() { jArray, jObjectAdress })["result"].ToString();
        }
    }
}
