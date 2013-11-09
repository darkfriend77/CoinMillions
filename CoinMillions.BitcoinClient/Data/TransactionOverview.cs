// <copyright file="TransactionOverview.cs" company="CoinMillions">
// Copyright (c) 2013 CoinMillions. All rights reserved.
// </copyright>
// <author>superreeen</author>
// <date>09.11.2013</date>
// <summary>Implements the transaction overview class</summary>
namespace CoinMillions.BitcoinClient.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary> A transaction overview. </summary>
    /// <remarks> superreeen, 09.11.2013. </remarks>
    public class TransactionOverview
    {
        /// <summary> Gets or sets the account. </summary>
        /// <value> The account. </value>
        public string Account { get; set; }

        /// <summary> Gets or sets the address. </summary>
        /// <value> The address. </value>
        public string Address { get; set; }

        /// <summary> Gets or sets the category. </summary>
        /// <value> The category. </value>
        public string Category { get; set; }

        /// <summary> Gets or sets the amount. </summary>
        /// <value> The amount. </value>
        public decimal Amount { get; set; }

        /// <summary> Gets or sets the confirmations. </summary>
        /// <value> The confirmations. </value>
        public int Confirmations { get; set; }

        /// <summary> Gets or sets the block hash. </summary>
        /// <value> The block hash. </value>
        public string BlockHash { get; set; }

        /// <summary> Gets or sets the zero-based index of the block. </summary>
        /// <value> The block index. </value>
        public int BlockIndex { get; set; }

        /// <summary> Gets or sets the block time. </summary>
        /// <value> The block time. </value>
        public int BlockTime { get; set; }

        /// <summary> Gets or sets the identifier of the transmit. </summary>
        /// <value> The identifier of the transmit. </value>
        public string TxId { get; set; }

        /// <summary> Gets or sets the time. </summary>
        /// <value> The time. </value>
        public int Time { get; set; }

        /// <summary> Gets or sets the time received. </summary>
        /// <value> The time received. </value>
        public int TimeReceived { get; set; }

        /// <summary> Gets or sets the fee. </summary>
        /// <value> The fee. </value>
        public decimal? Fee { get; set; }
    }
}
