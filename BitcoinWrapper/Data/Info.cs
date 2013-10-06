using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinWrapper.Data
{
    public class Info
    {
        public int Version { get; set; }
        public int ProtocolVersion { get; set; }
        public int WalletVersion { get; set; }
        public double Balance { get; set; }
        public int Blocks { get; set; }
        public int TimeOffset { get; set; }
        public int Connections { get; set; }
        public string Proxy { get; set; }
        public double Difficulty { get; set; }
        public bool TestNet { get; set; }
        public int KeyPoolOlDest { get; set; }
        public int KeyPoolSize { get; set; }
        public double PayTxFee { get; set; }
        public string Errors { get; set; }
    }

    public class PeerInfo
    {
        public string Addr { get; set; }
        public string Services { get; set; }
        public int LastSend { get; set; }
        public int LastRecv { get; set; }
        public int BytesSent { get; set; }
        public int BytesRecv { get; set; }
        public int ConnTime { get; set; }
        public int Version { get; set; }
        public string Subver { get; set; }
        public bool Inbound { get; set; }
        public int StartingHeight { get; set; }
        public int BanScore { get; set; }
        public bool SyncNode { get; set; }
    }
}
