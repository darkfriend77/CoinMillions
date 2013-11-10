using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoinMillions.Service.Base;

namespace CoinMillions.Tests
{
    [TestClass]
    public class ServiceBaseTests
    {
        private ServiceBase m_Service;

        [TestInitialize]
        public void Initialize()
        {
            m_Service = new ServiceBase(new Uri("http://127.0.0.1:18332/"), "testnet", "key");
        }

        [TestCleanup]
        public void DisposeService()
        {
            m_Service.Dispose();
            m_Service = null;
        }

        [TestMethod]
        public void ProcessBetsTest()
        {
            m_Service.ProcessBets();
        }

        [TestMethod]
        public void GetJackpotTest()
        {
            var value = m_Service.Jackpot;
        }

        [TestMethod]
        public void ProcessDrawTest()
        {
            m_Service.ProcessDraw();
        }
    }
}
