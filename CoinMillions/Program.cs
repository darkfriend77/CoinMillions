﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitcoinWrapper.Data;
using BitcoinWrapper.Wrapper;
using Newtonsoft.Json.Linq;

namespace CoinMillions
{
    class Program
    {
        static void Main(string[] args)
        {


            //using (var db = new DatabaseEntities())
            //{
            //}

            BitcoinQtConnector btc = new BitcoinQtConnector();

            //Console.WriteLine("GetInfo: {0}", btc.GetInfo());
            //Console.WriteLine("ListReceivedByAddress: {0}", btc.ListReceivedByAddress());

            //Transaction transaction = btc.ListTransactions().Last();

            //BitcoinWrapper.Data.Transaction transaction = btc.DecodeRawTransaction(btc.GetRawTransaction(inWalletTransaction.TxId));

            //Console.WriteLine("Transaction: {0}", transaction);
            //Console.WriteLine("Transaction: {0}", transaction.TxId);
            //Console.WriteLine("Transaction: {0}", transaction.Version);

            //foreach (vin vin1 in transaction.vin) {
            //    Console.WriteLine("CoinBase: {0}", vin1.CoinBase);
            //    Console.WriteLine("Sequence: {0}", vin1.Sequence);
            //}
            //Console.WriteLine("Transaction: {0}", transaction.vin);
            //Console.WriteLine("Transaction: {0}", transaction.vout);

            //JObject rawTransaction = btc.GetDecodedRawTransaction("c878e369de3d5021c6cc6ca5de9b13831b59876f9781f1320b6cbd5b00c970e0");


            //Console.WriteLine(btc.GetTxOut("a5513c35a2ae3ec4ee44822bc75840749834707ebde3ae3cef3dfcfb7c0de6b9", 1));







            //JObject list = btc.ListAccounts1();
            //foreach(JToken token in list.GetValue("result"))
            //{
            //    string account = token.ToString().Split(':')[0].Replace("\"", "");
            //    List<JToken> tokens = btc.GetAddressesByAccount(account);
            //    string adress = string.Empty;
            //    if (tokens.Count > 0)
            //        adress = tokens[0].ToString();

            //    Console.WriteLine("'{0}', '{1}'", account, adress);
            //}
















            //List<Transaction> listTransactions = btc.ListTransactionsByCategory();
            //foreach (Transaction trans in listTransactions)
            //{
            //    Console.WriteLine("- Transaction[{0}]", listTransactions.IndexOf(trans));
            //    Console.WriteLine("  + Transaction(TxId): {0}", "");
            //    Console.WriteLine("  + Transaction(Amount): {0}", trans.Amount);
            //    Console.WriteLine("  + Transaction(TimeReceived): {0}", trans.TimeReceived);
            //    Console.WriteLine("  + Transaction(Time): {0}", trans.Time);
            //    Console.WriteLine("  + Transaction(fee): {0}", trans.Fee);
            //    foreach (Detail detail in trans.Details)
            //    {
            //        Console.WriteLine("  + Details[{0}]", trans.Details.IndexOf(detail));
            //        Console.WriteLine("    + Account: {0}", detail.Account);
            //        Console.WriteLine("    + Address: {0}", detail.Address);
            //        Console.WriteLine("    + Amount: {0}", detail.Amount);
            //        Console.WriteLine("    + Category: {0}", detail.Category);
            //    }
            //    Console.WriteLine("  + RawTransaction: {0}", trans.RawTransaction);
            //}






            //string rawTrans = btc.CreateZeroConfirmationTransaction("61de00b44ccfda8f9775618f901a490104c2761f33864120d7f053235b6f103b", 0.01, 0.0001);


            //Console.WriteLine(btc.DecodeRawTransactionString(rawTrans));

            //SignedRawTransaction signrawtransaction = btc.SignRawTransaction(rawTrans);

            //Console.WriteLine("signrawtransaction.Complete: {0}", signrawtransaction.Complete);
            //Console.WriteLine("signrawtransaction.Hex: {0}", signrawtransaction.Hex);

            //string result = btc.SendRawTransaction(signrawtransaction.Hex);

            //Console.WriteLine("result: {0}", result);















            //Console.WriteLine("GetAddressesByAccount: {0}", btc.GetAddressesByAccount(""));

            //Console.WriteLine("Raw Transaction: {0}", rawTransaction.ToString());

            //JToken token;
            //if (rawTransaction.TryGetValue("result", out token))
            //    Console.WriteLine("token: {0}", token.ToString());

            //Console.WriteLine("rawTransaction: {0}", rawTransaction["result"].ToString());


//Yeah it works
//But its safer to go back an extra transaction
//Take the vout that was sent to you
//Get the raw transaction associated with it
//And find the vout in it that was used as input to the transaction you received
//And use the address in there

//What stops anybody from creating a transaction with no change ?
//I could perfectly create a transaction with two vouts that are not change and no change at all
//Then with your technique you wouldnt be sending me back the funds ?
//(no question mark typo sry)

            //'[{"txid":"7649b33b6d80f7b5c866fbdb413419e04223974b0a5d6a3ca54944f30474d2bf","vout":0}]' '{"mirQLRn6ciqa3WwJSSe7RSJNVfAE9zLkS5":50}'
            //Console.WriteLine("CreateRawTransaction: {0}", btc.CreateRawTransaction("7649b33b6d80f7b5c866fbdb413419e04223974b0a5d6a3ca54944f30474d2bf", 0, "mirQLRn6ciqa3WwJSSe7RSJNVfAE9zLkS5", (long)50.0));

            Console.ReadKey();
        }

    }
}