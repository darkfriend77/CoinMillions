using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMillionsServer.Data
{
    public class Adress
    {
        public bool IsValid { get; set; }
        public string Address { get; set; }
        public bool IsMine { get; set; }
        public bool IsScript { get; set; }
        public string PubKey { get; set; }
        public bool IsCompressed { get; set; }
        public string Account { get; set; }
    }
}
