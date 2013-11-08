using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinMillionsServer.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CoinMillionsServer.Wrapper
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

        /// <summary>
        /// Returns information about the block with the given hash.
        /// 
        /// example. getblock <hash>
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        public Block GetBlock(string hash)
        {
            return BaseConnector.RequestServer(MethodName.getblock, hash)["result"].ToObject<Block>();
        }

        //public string GetRawTransaction(string txid)
        //{
        //    return BaseConnector.RequestServer(MethodName.getrawtransaction, txid)["result"].ToString();
        //}

        /// <summary>
        /// Get a raw transaction representation for given transaction id and Produces a human-readable JSON object for this raw transaction.
        /// 
        /// example. getrawtransaction <txid> [verbose=0]
        /// </summary>
        /// <param name="txid"></param>
        /// <returns></returns>
        public RawTransaction GetRawTransactionObject(string txid)
        {
            string rawTransactionString = BaseConnector.RequestServer(MethodName.getrawtransaction, txid)["result"].ToString();
            return BaseConnector.RequestServer(MethodName.decoderawtransaction, rawTransactionString)["result"].ToObject<RawTransaction>();
        }

        //public string GetRawTransactionObjectString(string txid)
        //{
        //    string rawTransactionString = BaseConnector.RequestServer(MethodName.getrawtransaction, txid)["result"].ToString();
        //    return BaseConnector.RequestServer(MethodName.decoderawtransaction, rawTransactionString)["result"].ToString();
        //}

        //public RawTransaction DecodeRawTransaction(string rawTransaction)
        //{
        //    return BaseConnector.RequestServer(MethodName.decoderawtransaction, rawTransaction)["result"].ToObject<RawTransaction>();
        //}

        //public string DecodeRawTransactionString(string rawTransaction)
        //{
        //    return BaseConnector.RequestServer(MethodName.decoderawtransaction, rawTransaction)["result"].ToString();
        //}

        //public string GetAccount(string bitcoinAddress)
        //{
        //    return BaseConnector.RequestServer(MethodName.getaccount, bitcoinAddress)["result"].ToString();
        //}

        /// <summary>
        /// Returns the number of blocks in the longest block chain.
        /// </summary>
        /// <returns></returns>
        public string GetBlockCount()
        {
            return BaseConnector.RequestServer(MethodName.getblockcount)["result"].ToString();
        }

        /// <summary>
        /// Returns hash of block in best-block-chain at <index>; index 0 is the genesis block
        /// 
        /// example. getblockhash <index>
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetBlockhash(int index)
        {
            return BaseConnector.RequestServer(MethodName.getblockhash, index)["result"].ToString();
        }

        //public string GetDifficulty()
        //{
        //    return BaseConnector.RequestServer(MethodName.getdifficulty)["result"].ToString();
        //}

        //public string GetGenerate()
        //{
        //    return BaseConnector.RequestServer(MethodName.getgenerate)["result"].ToString();
        //}

        //public string GetHashesPerSec()
        //{
        //    return BaseConnector.RequestServer(MethodName.gethashespersec)["result"].ToString();
        //}

        /// <summary>
        /// Returns an object containing various state info.
        /// </summary>
        /// <returns></returns>
        public Info GetInfo()
        {
            return BaseConnector.RequestServer(MethodName.getinfo)["result"].ToObject<Info>();
        }

        //public string GetMiningInfo()
        //{
        //    return BaseConnector.RequestServer(MethodName.getmininginfo)["result"].ToString();
        //}

        //public List<PeerInfo> GetPeerInfo()
        //{
        //    return BaseConnector.RequestServer(MethodName.getpeerinfo)["result"].ToObject<List<PeerInfo>>();
        //}

        //public string GetRawMemPool()
        //{
        //    return BaseConnector.RequestServer(MethodName.getrawmempool)["result"].ToString();
        //}

        //public string BackupWallet(string destination)
        //{
        //    return BaseConnector.RequestServer(MethodName.backupwallet, destination)["result"].ToString();
        //}

        //public string DumpPrivKey(string bitcoinAddress)
        //{
        //    return BaseConnector.RequestServer(MethodName.dumpprivkey, bitcoinAddress)["result"].ToString();
        //}

        //public string EncryptWallet(string passphrame)
        //{
        //    return BaseConnector.RequestServer(MethodName.encryptwallet, passphrame)["result"].ToString();
        //}

        //public string GetAccountAddress(string account)
        //{
        //    return BaseConnector.RequestServer(MethodName.getaccountaddress, account)["result"].ToString();
        //}

        public string GetAddressesByAccount(string account)
        {
            return BaseConnector.RequestServer(MethodName.getaddressesbyaccount,account)["result"][0].ToString();
        }

        //public string GetBlockTemplate()
        //{
        //    return BaseConnector.RequestServer(MethodName.getblocktemplate)["result"].ToString();
        //}

        //public string GetConnectionCount()
        //{
        //    return BaseConnector.RequestServer(MethodName.getconnectioncount)["result"].ToString();
        //}

        //public string GetNewAddress(string account)
        //{
        //    return BaseConnector.RequestServer(MethodName.getnewaddress, account)["result"].ToString();
        //}

        public string GetReceivedByAccount(string account)
        {
            return BaseConnector.RequestServer(MethodName.getreceivedbyaccount, account)["result"].ToString();
        }

        //public string GetReceivedByAddress(string bitcoinAddress)
        //{
        //    var rawTransaction = BaseConnector.RequestServer(MethodName.getreceivedbyaddress, bitcoinAddress)["result"].ToString();
        //    return rawTransaction;
        //}

        /// <summary>
        /// Returns an object about the given transaction containing:
        /// - "amount" : total amount of the transaction
        /// - "confirmations" : number of confirmations of the transaction
        /// - "txid" : the transaction ID
        /// - "time" : time associated with the transaction[1].
        /// - "details" - An array of objects containing:
        ///   - "account"
        ///   - "address"
        ///   - "category"
        ///   - "amount"
        ///   - "fee"
        /// </summary>
        /// <param name="txid"></param>
        /// <returns></returns>
        public Transaction GetTransaction(string txid)
        {
            return BaseConnector.RequestServer(MethodName.gettransaction, txid)["result"].ToObject<Transaction>();
        }

        public string GetTransactionString(string txid)
        {
            return BaseConnector.RequestServer(MethodName.gettransaction, txid)["result"].ToString();
        }

        //public string GetWork()
        //{
        //    return BaseConnector.RequestServer(MethodName.getwork)["result"].ToString();
        //}

        //public string Help()
        //{
        //    return BaseConnector.RequestServer(MethodName.help)["result"].ToString();
        //}

        //public string ListAccounts()
        //{
        //    return BaseConnector.RequestServer(MethodName.listaccounts)["result"].ToString();
        //}

        //public string ListAddressGroupings()
        //{
        //    return BaseConnector.RequestServer(MethodName.listaddressgroupings)["result"].ToString();
        //}

        //public string ListReceivedByAccount()
        //{
        //    return BaseConnector.RequestServer(MethodName.listreceivedbyaccount)["result"].ToString();
        //}

        //public string ListReceivedByAddress()
        //{
        //    return BaseConnector.RequestServer(MethodName.listreceivedbyaddress)["result"].ToString();
        //}

        //public string ListSinceBlock()
        //{
        //    return BaseConnector.RequestServer(MethodName.listsinceblock)["result"].ToString();
        //}

        /// <summary>
        /// Returns up to [count] most recent transactions skipping the first [from] transactions for account [account].
        /// If [account] not provided will return recent transaction from all accounts.
        /// 
        /// example. listtransactions [account] [count=10] [from=0]
        /// </summary>
        /// <param name="account"></param>
        /// <param name="count"></param>
        /// <param name="from"></param>
        /// <returns></returns>
        public List<TransactionDetail> ListTransactions(string account = "", int count = 999999, int from = 0)
        {
            return BaseConnector.RequestServer(MethodName.listtransactions, new List<object>() { account, count, from })["result"].ToObject<List<TransactionDetail>>();
        }
        
        public string ListTransactionsString(string account = "", int count = 999999, int from = 0)
        {
            return BaseConnector.RequestServer(MethodName.listtransactions, new List<object>() {account, count, from})["result"].ToString();
        }

        public string ListUnspentString()
        {
            return BaseConnector.RequestServer(MethodName.listunspent)["result"].ToString();
        }

        //public string ListLockUnspent()
        //{
        //    return BaseConnector.RequestServer(MethodName.listlockunspent)["result"].ToString();
        //}

        //public string SetTxFee(float amount)
        //{
        //    return BaseConnector.RequestServer(MethodName.settxfee, amount)["result"].ToString();
        //}

        //public string WalletLock()
        //{
        //    return BaseConnector.RequestServer(MethodName.walletlock)["result"].ToString();
        //}

        /// <summary>
        /// Return information about <bitcoinaddress>.
        /// 
        /// example. validateaddress <bitcoinaddress>
        /// </summary>
        /// <param name="bitcoinAddress"></param>
        /// <returns></returns>
        public Adress ValidateAddress(string bitcoinAddress)
        {
            return BaseConnector.RequestServer(MethodName.validateaddress, bitcoinAddress)["result"].ToObject<Adress>();
        }

        //public string Stop()
        //{
        //    return BaseConnector.RequestServer(MethodName.stop)["result"].ToString();
        //}

        //public string SetGenerate(bool variable)
        //{
        //    return BaseConnector.RequestServer(MethodName.setgenerate, variable)["result"].ToString();
        //}

        /// <summary>
        /// <amount> is a real and is rounded to 8 decimal places. Returns the transaction ID <txid> if successful.
        /// 
        /// example. sendtoaddress <bitcoinaddress> <amount> [comment] [comment-to]
        /// </summary>
        /// <param name="bitcoinAddress"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public string SendToAddress(string bitcoinAddress, float amount)
        {
            return BaseConnector.RequestServer(MethodName.sendtoaddress, new List<object>() { bitcoinAddress, amount })["result"].ToString();
        }

        public string SendRawTransaction(string rawTrans)
        {
            return BaseConnector.RequestServer(MethodName.sendrawtransaction, rawTrans)["result"].ToString();
        }

        //public string AddNode(string node, NodeAction action)
        //{
        //    return BaseConnector.RequestServer(MethodName.addnode, new List<object>() { node, action.ToString() })["result"].ToString();
        //}

        //public string GetAddedNodeInfo(string dns)
        //{
        //    return BaseConnector.RequestServer(MethodName.getaddednodeinfo, dns)["result"].ToString();
        //}

        //public string GetTxOut(string txId, int n)
        //{
        //    return BaseConnector.RequestServer(MethodName.gettxout, new List<object>() { txId, n })["result"].ToString();
        //}

        //public string GetTxOutSetInfo()
        //{
        //    return BaseConnector.RequestServer(MethodName.gettxoutsetinfo)["result"].ToString();
        //}

        //public string KeyPoolRefill()
        //{
        //    return BaseConnector.RequestServer(MethodName.keypoolrefill)["result"].ToString();
        //}

        //public string SendFrom(string fromAccount, string toBitcoinAddress, decimal amount)
        //{
        //    return BaseConnector.RequestServer(MethodName.sendfrom, new List<object>() { fromAccount, toBitcoinAddress, amount })["result"].ToString();
        //}

        //public string SignMessage(string bitcoinAddress, string message)
        //{
        //    return BaseConnector.RequestServer(MethodName.signmessage, new List<object>() { bitcoinAddress, message })["result"].ToString();
        //}

        //public string SubmitBlock(string hexData)
        //{
        //    return BaseConnector.RequestServer(MethodName.submitblock, hexData)["result"].ToString();
        //}

        //public string VerifyMessage(string bitcoinAddress, string signature, string message)
        //{
        //    return BaseConnector.RequestServer(MethodName.verifymessage, new List<object>() { bitcoinAddress, signature, message })["result"].ToString();
        //}

        //public string WalletPassphrase(string passphrase, int timeout)
        //{
        //    return BaseConnector.RequestServer(MethodName.walletpassphrase, new List<object>() { passphrase, timeout })["result"].ToString();
        //}

        //public string WalletPassphraseChange(string oldpassphrase, string newpassphrase)
        //{
        //    return BaseConnector.RequestServer(MethodName.walletpassphrasechange, new List<object>() { oldpassphrase, newpassphrase })["result"].ToString();
        //}

        //public string Move(string fromAccount, string toAccount, float amount)
        //{
        //    return BaseConnector.RequestServer(MethodName.move, new List<object>() { fromAccount, toAccount, amount })["result"].ToString();
        //}

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

            JObject result = BaseConnector.RequestServer(MethodName.createrawtransaction, new List<object>() { jArray, jObjectAdress });

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
            return BaseConnector.RequestServer(MethodName.signrawtransaction, rawtransaction)["result"].ToObject<SignedRawTransaction>();
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
        /// <param name="amountToKeep"></param>
        /// <param name="fee"></param>
        /// <param name="rawTransaction"></param>
        /// <returns></returns>
        public bool CreateZeroConfirmationTransaction(string txid, double amountToKeep, double fee, string adress, out string rawTransaction)
        {
            rawTransaction = string.Empty;

            string changeVoutAdress, payeeVoutAdress;
            int payeeVoutN;
            double payeeVoutValue;
            if (!DeepTransactionInfo(txid, out changeVoutAdress, out payeeVoutAdress, out payeeVoutN, out payeeVoutValue))
             return false;

            if (adress != null && ValidateAddress(adress).IsValid)
                payeeVoutAdress = adress;

            rawTransaction = CreateRawTransaction(txid, payeeVoutN, changeVoutAdress, payeeVoutValue - (amountToKeep + fee), payeeVoutAdress, amountToKeep);

            return true;
        }

        //public string CreateZeroConfirmationTransaction(string txid, double amountToKeep, double fee)
        //{

        //    //Console.WriteLine(GetRawTransactionObjectString(txid));

        //    RawTransaction rawTransaction = GetRawTransactionObject(txid);

        //    Console.WriteLine("rawTransaction {0}", rawTransaction.TxId);
        //    Console.WriteLine("Version {0}", rawTransaction.Version);

        //    List<Vout> vouts = rawTransaction.Vout;
        //    List<Vout> changeVout = new List<Vout>();
        //    List<Vout> payeeVout = new List<Vout>();
        //    foreach (Vout vout in vouts)
        //    {
        //        Console.WriteLine("- VOUT[{0}]", vouts.IndexOf(vout));
        //        Console.WriteLine("  + N:     {0}", vout.N);
        //        Console.WriteLine("  + Value: {0}", vout.Value);
        //        Console.WriteLine("  + ScriptPubKey");
        //        Console.WriteLine("    + Addresses");

        //        if (vout.ScriptPubKey.Addresses.Count != 1)
        //            return string.Empty;

        //        Adress validatedAdress = ValidateAddress(vout.ScriptPubKey.Addresses[0]);

        //        if (!validatedAdress.IsMine)
        //            changeVout.Add(vout);
        //        else
        //            payeeVout.Add(vout);

        //        Console.WriteLine("      + Address:      {0}", validatedAdress.Address);
        //        Console.WriteLine("      + Account:      {0}", validatedAdress.Account);
        //        Console.WriteLine("      + IsCompressed: {0}", validatedAdress.IsCompressed);
        //        Console.WriteLine("      + IsMine:       {0}", validatedAdress.IsMine);
        //        Console.WriteLine("      + IsScript:     {0}", validatedAdress.IsScript);
        //        Console.WriteLine("      + IsValid:      {0}", validatedAdress.IsValid);

        //    }

        //    if (changeVout.Count != 1 || payeeVout.Count != 1)
        //        return string.Empty;

        //    Console.WriteLine("CHANGEVOUT");
        //    Console.WriteLine(" N:     {0}", changeVout[0].N);
        //    Console.WriteLine(" Value: {0}", changeVout[0].Value);
        //    Console.WriteLine(" Address: {0}", changeVout[0].ScriptPubKey.Addresses[0].ToString());
        //    Console.WriteLine(" rawTransaction {0}", txid);

        //    string createRawTransaction = CreateRawTransaction(txid, payeeVout[0].N, changeVout[0].ScriptPubKey.Addresses[0].ToString(), payeeVout[0].Value - amountToKeep, payeeVout[0].ScriptPubKey.Addresses[0].ToString(), amountToKeep - fee);

        //    Console.WriteLine("CreateRawTransaction: {0}", createRawTransaction);

        //    return createRawTransaction;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="txid"></param>
        /// <param name="changeVoutAdress"></param>
        /// <param name="payeeVoutAdress"></param>
        /// <param name="payeeVoutN"></param>
        /// <param name="payeeVoutValue"></param>
        /// <returns></returns>
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


        public void getNextDifficultyChangeBlock()
        {

        }
    }
}
