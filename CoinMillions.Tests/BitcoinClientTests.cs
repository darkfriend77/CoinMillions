namespace CoinMillions.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using CoinMillions.BitcoinClient;

    [TestClass]
    public class BitcoinClientTests
    {
        private BitcoinClient m_Client;

        [TestInitialize]
        public void InitializeClient()
        {
            m_Client = new BitcoinClient(new Uri("http://127.0.0.1:18332/"), "testnet", "key");
        }

        [TestCleanup]
        public void DisposeClient()
        {
            m_Client.Dispose();
            m_Client = null;
        }

        [TestMethod]
        public void GetReceivedByAccountTest()
        {
            decimal x = m_Client.GetReceivedByAccount("");
            Assert.IsNotNull(x);
        }

        [TestMethod]
        public void GetBlockHashTest()
        {
            string hash = m_Client.GetBlockHash(0);
            Assert.AreEqual("000000000933ea01ad0ee984209779baaec3ced90fa3f408719526f8d77f4943", hash);
        }

        [TestMethod]
        public void ListUnspentTest()
        {
            var unspent = m_Client.ListUnspent();
            Assert.IsNotNull(unspent);
        }

        [TestMethod]
        public void GetInfoTest()
        {
            var info = m_Client.GetInfo();
            Assert.IsNotNull(info);
        }
    }
}
