// <copyright file="ValidatedAddress.cs" company="CoinMillions">
// Copyright (c) 2013 CoinMillions. All rights reserved.
// </copyright>
// <author>superreeen</author>
// <date>09.11.2013</date>
// <summary>Implements the validated address class</summary>
namespace CoinMillions.BitcoinClient.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary> A validated address. </summary>
    /// <remarks> superreeen, 09.11.2013. </remarks>
    public class ValidatedAddress
    {
        /// <summary> Gets or sets a value indicating whether this instance is valid. </summary>
        /// <value> true if this instance is valid, false if not. </value>
        public bool IsValid { get; set; }

        /// <summary> Gets or sets the address. </summary>
        /// <value> The address. </value>
        public string Address { get; set; }

        /// <summary> Gets or sets a value indicating whether this instance is mine. </summary>
        /// <value> true if this instance is mine, false if not. </value>
        public bool IsMine { get; set; }

        /// <summary> Gets or sets a value indicating whether this instance is script. </summary>
        /// <value> true if this instance is script, false if not. </value>
        public bool IsScript { get; set; }

        /// <summary> Gets or sets the pub key. </summary>
        /// <value> The pub key. </value>
        public string PubKey { get; set; }

        /// <summary> Gets or sets a value indicating whether this instance is compressed. </summary>
        /// <value> true if this instance is compressed, false if not. </value>
        public bool IsCompressed { get; set; }

        /// <summary> Gets or sets the account. </summary>
        /// <value> The account. </value>
        public string Account { get; set; }
    }
}
