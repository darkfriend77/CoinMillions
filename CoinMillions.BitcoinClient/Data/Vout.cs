// <copyright file="Vout.cs" company="CoinMillions">
// Copyright (c) 2013 CoinMillions. All rights reserved.
// </copyright>
// <author>superreeen</author>
// <date>08.11.2013</date>
// <summary>Implements the vout class</summary>
namespace CoinMillions.BitcoinClient.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary> A vout. </summary>
    /// <remarks> superreeen, 08.11.2013. </remarks>
    public class Vout
    {
        /// <summary> Gets or sets the value. </summary>
        /// <value> The value. </value>
        public decimal Value { get; set; }

        /// <summary> Gets or sets the n. </summary>
        /// <value> The n. </value>
        public int N { get; set; }

        /// <summary> Gets or sets the script pub key. </summary>
        /// <value> The script pub key. </value>
        public ScriptPubKey ScriptPubKey { get; set; }
    }
}
