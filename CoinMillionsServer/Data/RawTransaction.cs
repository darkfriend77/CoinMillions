using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMillionsServer.Data
{
    public class RawTransaction
    {
        public string TxId { get; set; }
        public int Version { get; set; }
        public int LockTime { get; set; }
        public List<Vin> Vin { get; set; }
        public List<Vout> Vout { get; set; }
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

    public class SignedRawTransaction
    {
        public string Hex { get; set; }
        public bool Complete { get; set; }
    }
}
