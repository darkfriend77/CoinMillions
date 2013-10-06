using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMillions.Model
{
    class Account
    {
        public string Name { get; set; }
        public string Adress { get; set; }
        public List<Transaction> transactions { get; set; }
    }

    public class Transaction
    {
        public float Amount { get; set; }
        public int Confirmations { get; set; }
        public string Blockhash { get; set; }
        public int BlockIndex { get; set; }
        public int BlockTime { get; set; }
        public string TxId { get; set; }
        public int Time { get; set; }
        public int TimeReceived { get; set; }
        public double? fee { get; set; }
        public List<Details> Details { get; set; }
        public RawTransaction rawTransaction { get; set; }
    }

    public class Details
    {
        public string Account { get; set; }
        public string Address { get; set; }
        public string Category { get; set; }
        public float Amount { get; set; }
    }

    public class RawTransaction
    {
        public string TxId { get; set; }
        public int Version { get; set; }
        public int LockTime { get; set; }
        public List<Vin> Vins { get; set; }
        public List<Vout> Vouts { get; set; }
    }

    public class Vout
    {
        public double Value { get; set; }
        public int N { get; set; }
        public ScriptPubKey ScriptPubKey { get; set; }
    }

    public class Vin
    {
        public string TxId { get; set; }
        public int Vout { get; set; }
        public ScriptSig ScriptSig { get; set; }
        public object Sequence { get; set; }
    }

    public class ScriptSig
    {
        public string Asm { get; set; }
        public string Hex { get; set; }
    }

    public class ScriptPubKey
    {
        public string Asm { get; set; }
        public string Hex { get; set; }
        public int ReqSigs { get; set; }
        public string Type { get; set; }
        public List<string> Addresses { get; set; }
    }

    public class Block
    {
        public string Hash { get; set; }
        public int Confirmations { get; set; }
        public int Size { get; set; }
        public int Height { get; set; }
        public int Version { get; set; }
        public string MerkleRoot { get; set; }
        public List<string> Tx { get; set; }
        public int Time { get; set; }
        public string Nonce { get; set; }
        public string Bits { get; set; }
        public float Difficulty { get; set; }
        public string PreviousBlockHash { get; set; }
        public string NextBlockHash { get; set; }
    }
}
