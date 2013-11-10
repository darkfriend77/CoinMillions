namespace CoinMillions.Service.Console
{
    using CoinMillions.Service.Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Console = System.Console;
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceBase service = new ServiceBase(new Uri("http://127.0.0.1:18332/"), "testnet", "key"))
            {
                Console.ReadLine();
            }
        }
    }
}
