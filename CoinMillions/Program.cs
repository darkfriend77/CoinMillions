using System;
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

            BaseBtcConnector btc = new BaseBtcConnector();

            Console.WriteLine("GetInfo: {0}", btc.GetInfo());
            //Console.WriteLine("ListReceivedByAddress: {0}", btc.ListReceivedByAddress());

            InWalletTransaction inWalletTransaction = btc.GetAllInWalletTransactions().Last();

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

            JObject rawTransaction = btc.GetDecodedRawTransaction(inWalletTransaction.TxId);

            //Console.WriteLine("Raw Transaction: {0}", rawTransaction.ToString());

            //JToken token;
            //if (rawTransaction.TryGetValue("result", out token))
            //    Console.WriteLine("token: {0}", token.ToString());

            //Console.WriteLine("result: {0}", rawTransaction["result"]["vout"]["value"].ToString());
            Console.WriteLine("vout: {0}", rawTransaction["result"]["vout"].ToString());


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
