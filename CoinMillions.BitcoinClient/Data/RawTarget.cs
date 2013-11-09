// <copyright file="RawTarget.cs" company="CoinMillions">
// Copyright (c) 2013 CoinMillions. All rights reserved.
// </copyright>
// <author>superreeen</author>
// <date>09.11.2013</date>
// <summary>Implements the raw target class</summary>
namespace CoinMillions.BitcoinClient.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary> A raw target. </summary>
    /// <remarks> superreeen, 09.11.2013. </remarks>
    public class RawTarget
    {
        /// <summary> Gets or sets the address. </summary>
        /// <value> The address. </value>
        public string Address { get; set; }

        /// <summary> Gets or sets the amount. </summary>
        /// <value> The amount. </value>
        public decimal Amount { get; set; }
    }
}
