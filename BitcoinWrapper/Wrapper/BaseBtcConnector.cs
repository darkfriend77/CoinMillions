using System;
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
    public class BitcoinQtConnector
    {
        

        public BaseConnector BaseConnector;

        /// <summary>
        /// Starts connecting to the Bitcoin-qt server
        /// https://en.bitcoin.it/wiki/Original_Bitcoin_client/API_calls_list
        /// </summary>
        public BitcoinQtConnector()
        {
            this.BaseConnector = new BaseConnector();
        }

        public string GetBalance()
        {
            return BaseConnector.RequestServer(MethodName.getbalance)["result"].ToString();
        }

        public Block GetBlock(string hash)
        {
            return BaseConnector.RequestServer(MethodName.getblock, hash)["result"].ToObject<Block>();
        }

        public string GetRawTransaction(string txid)
        {
            return BaseConnector.RequestServer(MethodName.getrawtransaction, txid)["result"].ToString();
        }

        public RawTransaction GetRawTransactionObject(string txid)
        {
            string rawTransactionString = BaseConnector.RequestServer(MethodName.getrawtransaction, txid)["result"].ToString();
            return BaseConnector.RequestServer(MethodName.decoderawtransaction, rawTransactionString)["result"].ToObject<RawTransaction>();
        }

        public string GetRawTransactionObjectString(string txid)
        {
            string rawTransactionString = BaseConnector.RequestServer(MethodName.getrawtransaction, txid)["result"].ToString();
            return BaseConnector.RequestServer(MethodName.decoderawtransaction, rawTransactionString)["result"].ToString();
        }

        public RawTransaction DecodeRawTransaction(string rawTransaction)
        {
            return BaseConnector.RequestServer(MethodName.decoderawtransaction, rawTransaction)["result"].ToObject<RawTransaction>();
        }

        public string DecodeRawTransactionString(string rawTransaction)
        {
            return BaseConnector.RequestServer(MethodName.decoderawtransaction, rawTransaction)["result"].ToString();
        }

        public string GetAccount(string bitcoinAddress)
        {
            return BaseConnector.RequestServer(MethodName.getaccount, bitcoinAddress)["result"].ToString();
        }

        public string GetBlockCount()
        {
            return BaseConnector.RequestServer(MethodName.getblockcount)["result"].ToString();
        }

        public string GetBlockhash(int index)
        {
            return BaseConnector.RequestServer(MethodName.getblockhash, index)["result"].ToString();
        }

        public string GetDifficulty()
        {
            return BaseConnector.RequestServer(MethodName.getdifficulty)["result"].ToString();
        }

        public string GetGenerate()
        {
            return BaseConnector.RequestServer(MethodName.getgenerate)["result"].ToString();
        }

        public string GetHashesPerSec()
        {
            return BaseConnector.RequestServer(MethodName.gethashespersec)["result"].ToString();
        }

        public Info GetInfo()
        {
            return BaseConnector.RequestServer(MethodName.getinfo)["result"].ToObject<Info>();
        }

        public string GetMiningInfo()
        {
            return BaseConnector.RequestServer(MethodName.getmininginfo)["result"].ToString();
        }

        public List<PeerInfo> GetPeerInfo()
        {
            return BaseConnector.RequestServer(MethodName.getpeerinfo)["result"].ToObject<List<PeerInfo>>();
        }

        public string GetRawMemPool()
        {
            return BaseConnector.RequestServer(MethodName.getrawmempool)["result"].ToString();
        }

        public string BackupWallet(string destination)
        {
            return BaseConnector.RequestServer(MethodName.backupwallet, destination)["result"].ToString();
        }

        public string DumpPrivKey(string bitcoinAddress)
        {
            return BaseConnector.RequestServer(MethodName.dumpprivkey, bitcoinAddress)["result"].ToString();
        }

        public string EncryptWallet(string passphrame)
        {
            return BaseConnector.RequestServer(MethodName.encryptwallet, passphrame)["result"].ToString();
        }

        public string GetAccountAddress(string account)
        {
            return BaseConnector.RequestServer(MethodName.getaccountaddress, account)["result"].ToString();
        }

        //public string GetAddressesByAccount(string account)
        //{
        //    return BaseConnector.RequestServer(MethodName.getaddressesbyaccount,account)["result"].ToString();
        //}

        public List<JToken> GetAddressesByAccount(string account)
        {
            JArray array = BaseConnector.RequestServer(MethodName.getaddressesbyaccount, account).GetValue("result").ToObject<JArray>();
            if (array.Count == 0)
                return new List<JToken>();
            return array.ToList();
            //return BaseConnector.RequestServer(MethodName.getaddressesbyaccount, account).ToString();
        }

        public string GetBlockTemplate()
        {
            return BaseConnector.RequestServer(MethodName.getblocktemplate)["result"].ToString();
        }

        public string GetConnectionCount()
        {
            return BaseConnector.RequestServer(MethodName.getconnectioncount)["result"].ToString();
        }

        public string GetNewAddress(string account)
        {
            return BaseConnector.RequestServer(MethodName.getnewaddress, account)["result"].ToString();
        }

        public string GetReceivedByAccount(string account)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.getreceivedbyaccount, account)["result"].ToString();
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
        public Transaction GetTransaction(string txid)
        {
            Transaction transaction = BaseConnector.RequestServer(MethodName.gettransaction, txid)["result"].ToObject<Transaction>();
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

        public JObject ListAccounts1()
        {
            return BaseConnector.RequestServer(MethodName.listaccounts);
        }

        public string ListAddressGroupings()
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.listaddressgroupings)["result"].ToString();
            return rawTransaction;
        }

        public string ListReceivedByAccount()
        {
            return BaseConnector.RequestServer(MethodName.listreceivedbyaccount)["result"].ToString();
        }

        public string ListReceivedByAddress()
        {
            return BaseConnector.RequestServer(MethodName.listreceivedbyaddress)["result"].ToString();
        }

        public string ListSinceBlock()
        {
            return BaseConnector.RequestServer(MethodName.listsinceblock)["result"].ToString();
        }

        //public List<Transaction> ListTransactions()
        //{
        //    List<Transaction> transactions = BaseConnector.RequestServer(MethodName.listtransactions)["result"].ToObject <List<Transaction>>();

        //    foreach (Transaction transaction in transactions)
        //    {
        //        transaction.Details = GetTransaction(transaction.TxId).Details;
        //        transaction.RawTransaction = GetRawTransactionObject(transaction.TxId);
        //    }
        //    return transactions;
        //}

        public string ListTransactions()
        {
            return BaseConnector.RequestServer(MethodName.listtransactions)["result"].ToString();
        }

        public string ListLockUnspent()
        {
            return BaseConnector.RequestServer(MethodName.listlockunspent)["result"].ToString();
        }

        public string SetTxFee(float amount)
        {
            return BaseConnector.RequestServer(MethodName.settxfee, amount)["result"].ToString();
        }

        public string WalletLock()
        {
            return BaseConnector.RequestServer(MethodName.walletlock)["result"].ToString();
        }

        public Adress ValidateAddress(string bitcoinAddress)
        {
            return BaseConnector.RequestServer(MethodName.validateaddress, bitcoinAddress)["result"].ToObject<Adress>();
        }

        public string Stop()
        {
            return BaseConnector.RequestServer(MethodName.stop)["result"].ToString();
        }

        public string SetGenerate(bool variable)
        {
            return BaseConnector.RequestServer(MethodName.setgenerate, variable)["result"].ToString();
        }

        /// <summary>
        /// not tested
        /// </summary>
        /// <param name="bitcoinAddress"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public string SendToAddress(string bitcoinAddress, float amount)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.sendtoaddress, new List<object>() { bitcoinAddress, amount })["result"].ToString();
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
            var rawTransaction = BaseConnector.RequestServer(MethodName.gettxout, new List<object>() { txId, n })["result"].ToString();
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
            var rawTransaction = BaseConnector.RequestServer(MethodName.sendfrom, new List<object>() { fromAccount, toBitcoinAddress, amount })["result"].ToString();
            return rawTransaction;
        }

        public string SignMessage(string bitcoinAddress, string message)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.signmessage, new List<object>() { bitcoinAddress, message })["result"].ToString();
            return rawTransaction;
        }

        public string SubmitBlock(string hexData)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.submitblock, hexData)["result"].ToString();
            return rawTransaction;
        }

        public string VerifyMessage(string bitcoinAddress, string signature, string message)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.verifymessage, new List<object>() { bitcoinAddress, signature, message })["result"].ToString();
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
            var rawTransaction = BaseConnector.RequestServer(MethodName.walletpassphrase, new List<object>() { passphrase, timeout })["result"].ToString();
            return rawTransaction;
        }

        public string WalletPassphraseChange(string oldpassphrase, string newpassphrase)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.walletpassphrasechange, new List<object>() { oldpassphrase, newpassphrase })["result"].ToString();
            return rawTransaction;
        }

        public string Move(string fromAccount, string toAccount, float amount)
        {
            var rawTransaction = BaseConnector.RequestServer(MethodName.move, new List<object>() { fromAccount, toAccount, amount })["result"].ToString();
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
        public string CreateRawTransaction(string txid, int vout, string adress1, double amount1, string adress2, double amount2)
        {
            JArray jArray = new JArray();

            JObject jObjectTx = new JObject();

            jObjectTx.Add("txid", txid);
            jObjectTx.Add("vout", vout);

            jArray.Add(jObjectTx);

            JObject jObjectAdress = new JObject();
            jObjectAdress.Add(adress1, amount1);
            jObjectAdress.Add(adress2, amount2);

            Console.WriteLine(jObjectTx.ToString());

            Console.WriteLine(jObjectAdress.ToString());

            JObject result = BaseConnector.RequestServer(MethodName.createrawtransaction, new List<object>() { jArray, jObjectAdress });

            Console.WriteLine(result.ToString());

            return result["result"].ToString();
        }

        /// <summary>
        /// Sign as many inputs as possible for raw transaction (serialized, hex-encoded). The first argument may be several variations of the
        /// same transaction concatenated together; signatures from all of them will be combined together, along with signatures for keys in
        /// the local wallet. The optional second argument is an array of parent transaction outputs, so you can create a chain of raw transactions
        /// that depend on each other before sending them to the network. Third optional argument is an array of base58-encoded private keys that,
        /// if given, will be the only keys used to sign the transaction. The fourth optional argument is a string that specifies how the signature
        /// hash is computed, and can be "ALL", "NONE", "SINGLE", "ALL|ANYONECANPAY", "NONE|ANYONECANPAY", or "SINGLE|ANYONECANPAY".
        /// Returns json object with keys:
        /// - hex : raw transaction with signature(s) (hex-encoded string)
        /// - complete : 1 if rawtx is completely signed, 0 if signatures are missing.
        /// 
        /// If no private keys are given and the wallet is locked, requires that the wallet be unlocked with walletpassphrase first.
        /// 
        /// example. signrawtransaction <hex string> [{"txid":txid,"vout":n,"scriptPubKey":hex},...] [<privatekey1>,...] [sighash="ALL"]
        /// </summary>
        /// <param name="txid"></param>
        /// <param name="vout"></param>
        /// <param name="adress1"></param>
        /// <param name="amount1"></param>
        /// <param name="adress2"></param>
        /// <param name="amount2"></param>
        /// <returns></returns>
        public SignedRawTransaction SignRawTransaction(string rawtransaction)
        {

            JObject result = BaseConnector.RequestServer(MethodName.signrawtransaction, rawtransaction);

            return result["result"].ToObject<SignedRawTransaction>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public List<Transaction> ListTransactionsByCategory(string category = null)
        {
            List<Transaction> transactions = BaseConnector.RequestServer(MethodName.listtransactions)["result"].ToObject<List<Transaction>>();

            foreach (Transaction transaction in transactions)
            {
                transaction.Details = GetTransaction(transaction.TxId).Details;
                transaction.RawTransaction = GetRawTransactionObject(transaction.TxId);
            }

            return transactions.Where(t => t.Details.First().Category == category || category == string.Empty || category == null).ToList();
        }

        public bool CreateZeroConfirmationTransaction(string txid, double amountToKeep, double fee, out string rawTransaction)
        {
            rawTransaction = string.Empty;

            string changeVoutAdress, payeeVoutAdress;
            int payeeVoutN;
            double payeeVoutValue;
            if (!DeepTransactionInfo(txid, out changeVoutAdress, out payeeVoutAdress, out payeeVoutN, out payeeVoutValue))
             return false;

            rawTransaction = CreateRawTransaction(txid, payeeVoutN, changeVoutAdress, payeeVoutValue - amountToKeep, payeeVoutAdress, amountToKeep - fee);

            return true;
        }

        public string CreateZeroConfirmationTransaction(string txid, double amountToKeep, double fee)
        {

            //Console.WriteLine(GetRawTransactionObjectString(txid));

            RawTransaction rawTransaction = GetRawTransactionObject(txid);

            Console.WriteLine("rawTransaction {0}", rawTransaction.TxId);
            Console.WriteLine("Version {0}", rawTransaction.Version);

            List<Vout> vouts = rawTransaction.Vout;
            List<Vout> changeVout = new List<Vout>();
            List<Vout> payeeVout = new List<Vout>();
            foreach (Vout vout in vouts)
            {
                Console.WriteLine("- VOUT[{0}]", vouts.IndexOf(vout));
                Console.WriteLine("  + N:     {0}", vout.N);
                Console.WriteLine("  + Value: {0}", vout.Value);
                Console.WriteLine("  + ScriptPubKey");
                Console.WriteLine("    + Addresses");

                if (vout.ScriptPubKey.Addresses.Count != 1)
                    return string.Empty;

                Adress validatedAdress = ValidateAddress(vout.ScriptPubKey.Addresses[0]);

                if (!validatedAdress.IsMine)
                    changeVout.Add(vout);
                else
                    payeeVout.Add(vout);

                Console.WriteLine("      + Address:      {0}", validatedAdress.Address);
                Console.WriteLine("      + Account:      {0}", validatedAdress.Account);
                Console.WriteLine("      + IsCompressed: {0}", validatedAdress.IsCompressed);
                Console.WriteLine("      + IsMine:       {0}", validatedAdress.IsMine);
                Console.WriteLine("      + IsScript:     {0}", validatedAdress.IsScript);
                Console.WriteLine("      + IsValid:      {0}", validatedAdress.IsValid);

            }

            if (changeVout.Count != 1 || payeeVout.Count != 1)
                return string.Empty;

            Console.WriteLine("CHANGEVOUT");
            Console.WriteLine(" N:     {0}", changeVout[0].N);
            Console.WriteLine(" Value: {0}", changeVout[0].Value);
            Console.WriteLine(" Address: {0}", changeVout[0].ScriptPubKey.Addresses[0].ToString());
            Console.WriteLine(" rawTransaction {0}", txid);

            string createRawTransaction = CreateRawTransaction(txid, payeeVout[0].N, changeVout[0].ScriptPubKey.Addresses[0].ToString(), payeeVout[0].Value - amountToKeep, payeeVout[0].ScriptPubKey.Addresses[0].ToString(), amountToKeep - fee);

            Console.WriteLine("CreateRawTransaction: {0}", createRawTransaction);

            return createRawTransaction;
        }

        public bool DeepTransactionInfo(string txid, out string changeVoutAdress, out string payeeVoutAdress, out int payeeVoutN, out double payeeVoutValue)
        {
            changeVoutAdress = string.Empty;
            payeeVoutAdress = string.Empty;
            payeeVoutN = 0;
            payeeVoutValue = 0;
            
            RawTransaction rawTransaction = GetRawTransactionObject(txid);
            List<Vout> vouts = rawTransaction.Vout;
            List<Vout> changeVout = new List<Vout>();
            List<Vout> payeeVout = new List<Vout>();

            foreach (Vout vout in vouts)
            {
                if (vout.ScriptPubKey.Addresses.Count != 1)
                    return false;

                Adress validatedAdress = ValidateAddress(vout.ScriptPubKey.Addresses[0]);

                if (!validatedAdress.IsMine)
                    changeVout.Add(vout);
                else
                    payeeVout.Add(vout);
            }

            if (changeVout.Count != 1 || payeeVout.Count != 1)
                return false;

            payeeVoutN = payeeVout[0].N;
            payeeVoutValue = payeeVout[0].Value;
            changeVoutAdress = changeVout[0].ScriptPubKey.Addresses[0];
            payeeVoutAdress = payeeVout[0].ScriptPubKey.Addresses[0];

            return true;
        }

    }
}
