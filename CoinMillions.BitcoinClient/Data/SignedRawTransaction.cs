// <copyright file="SignedRawTransaction.cs" company="CoinMillions">
// Copyright (c) 2013 CoinMillions. All rights reserved.
// </copyright>
// <author>superreeen</author>
// <date>08.11.2013</date>
// <summary>Implements the signed raw transaction class</summary>
namespace CoinMillions.BitcoinClient.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary> A signed raw transaction. </summary>
    /// <remarks> superreeen, 08.11.2013. </remarks>
    public class SignedRawTransaction
    {
        /// <summary> Gets or sets the hexadecimal. </summary>
        /// <value> The hexadecimal. </value>
        public string Hex { get; set; }

        /// <summary> Gets or sets a value indicating whether the complete. </summary>
        /// <value> true if complete, false if not. </value>
        public bool Complete { get; set; }
    }
}
