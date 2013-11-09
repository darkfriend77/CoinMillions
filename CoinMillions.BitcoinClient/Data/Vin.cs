// <copyright file="Vin.cs" company="CoinMillions">
// Copyright (c) 2013 CoinMillions. All rights reserved.
// </copyright>
// <author>superreeen</author>
// <date>08.11.2013</date>
// <summary>Implements the vin class</summary>
namespace CoinMillions.BitcoinClient.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary> A vin. </summary>
    /// <remarks> superreeen, 08.11.2013. </remarks>
    public class Vin
    {
        /// <summary> Gets or sets the identifier of the transmit. </summary>
        /// <value> The identifier of the transmit. </value>
        public string TxId { get; set; }

        /// <summary> Gets or sets the vout. </summary>
        /// <value> The vout. </value>
        public int Vout { get; set; }

        /// <summary> Gets or sets the script signal. </summary>
        /// <value> The script signal. </value>
        public ScriptSig ScriptSig { get; set; }

        /// <summary> Gets or sets the sequence. </summary>
        /// <value> The sequence. </value>
        public object Sequence { get; set; }
    }
}
