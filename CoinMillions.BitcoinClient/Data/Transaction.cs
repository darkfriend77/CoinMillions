// <copyright file="Transaction.cs" company="CoinMillions">
// Copyright (c) 2013 CoinMillions. All rights reserved.
// </copyright>
// <author>superreeen</author>
// <date>09.11.2013</date>
// <summary>Implements the transaction class</summary>


namespace CoinMillions.BitcoinClient.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary> A transaction. </summary>
    /// <remarks> superreeen, 09.11.2013. </remarks>
    public class Transaction
    {
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

        /// <summary> Gets or sets the details. </summary>
        /// <value> The details. </value>
        public List<TransactionDetail> Details { get; set; }
    }
}