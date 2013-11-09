// <copyright file="Info.cs" company="CoinMillions">
// Copyright (c) 2013 CoinMillions. All rights reserved.
// </copyright>
// <author>superreeen</author>
// <date>09.11.2013</date>
// <summary>Implements the information class</summary>
namespace CoinMillions.BitcoinClient.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary> An information. </summary>
    /// <remarks> superreeen, 09.11.2013. </remarks>
    public class Info
    {
        /// <summary> Gets or sets the version. </summary>
        /// <value> The version. </value>
        public int Version { get; set; }

        /// <summary> Gets or sets the protocol version. </summary>
        /// <value> The protocol version. </value>
        public int ProtocolVersion { get; set; }

        /// <summary> Gets or sets the wallet version. </summary>
        /// <value> The wallet version. </value>
        public int WalletVersion { get; set; }

        /// <summary> Gets or sets the balance. </summary>
        /// <value> The balance. </value>
        public double Balance { get; set; }

        /// <summary> Gets or sets the blocks. </summary>
        /// <value> The blocks. </value>
        public int Blocks { get; set; }

        /// <summary> Gets or sets the time offset. </summary>
        /// <value> The time offset. </value>
        public int TimeOffset { get; set; }

        /// <summary> Gets or sets the connections. </summary>
        /// <value> The connections. </value>
        public int Connections { get; set; }

        /// <summary> Gets or sets the proxy. </summary>
        /// <value> The proxy. </value>
        public string Proxy { get; set; }

        /// <summary> Gets or sets the difficulty. </summary>
        /// <value> The difficulty. </value>
        public double Difficulty { get; set; }

        /// <summary> Gets or sets a value indicating whether the test net. </summary>
        /// <value> true if test net, false if not. </value>
        public bool TestNet { get; set; }

        /// <summary> Gets or sets the key pool oldest. </summary>
        /// <value> The key pool oldest. </value>
        public int KeyPoolOldest { get; set; }

        /// <summary> Gets or sets the size of the key pool. </summary>
        /// <value> The size of the key pool. </value>
        public int KeyPoolSize { get; set; }

        /// <summary> Gets or sets the pay transmit fee. </summary>
        /// <value> The pay transmit fee. </value>
        public double PayTxFee { get; set; }

        /// <summary> Gets or sets the errors. </summary>
        /// <value> The errors. </value>
        public string Errors { get; set; }
    }
}
