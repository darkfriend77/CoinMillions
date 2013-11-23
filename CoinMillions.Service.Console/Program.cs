namespace CoinMillions.Service.Console
{
    using CoinMillions.Service.Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Console = System.Console;
    using System.Configuration;
    class Program
    {
        static void Main(string[] args)
        {
            string serviceUri = ConfigurationManager.AppSettings["ServiceUri"];
            string serviceUser = ConfigurationManager.AppSettings["ServiceUser"];
            string servicePass = ConfigurationManager.AppSettings["ServicePass"];
            using (ServiceBase service = new ServiceBase(new Uri(serviceUri), serviceUser, servicePass))
            {
                Console.ReadLine();
            }
        }
    }
}
