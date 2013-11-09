// <copyright file="RawTransaction.cs" company="CoinMillions">
// Copyright (c) 2013 CoinMillions. All rights reserved.
// </copyright>
// <author>superreeen</author>
// <date>08.11.2013</date>
// <summary>Implements the raw transaction class</summary>
namespace CoinMillions.BitcoinClient.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary> A raw transaction. </summary>
    /// <remarks> superreeen, 08.11.2013. </remarks>
    public class RawTransaction
    {
        /// <summary> Gets or sets the identifier of the transmit. </summary>
        /// <value> The identifier of the transmit. </value>
        public string TxId { get; set; }

        /// <summary> Gets or sets the version. </summary>
        /// <value> The version. </value>
        public int Version { get; set; }

        /// <summary> Gets or sets the lock time. </summary>
        /// <value> The lock time. </value>
        public int LockTime { get; set; }

        /// <summary> Gets or sets the vin. </summary>
        /// <value> The vin. </value>
        public List<Vin> Vin { get; set; }

        /// <summary> Gets or sets the vout. </summary>
        /// <value> The vout. </value>
        public List<Vout> Vout { get; set; }
    }
}
