// <copyright file="ScriptPubKey.cs" company="CoinMillions">
// Copyright (c) 2013 CoinMillions. All rights reserved.
// </copyright>
// <author>superreeen</author>
// <date>08.11.2013</date>
// <summary>Implements the script pub key class</summary>
namespace CoinMillions.BitcoinClient.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary> A script pub key. </summary>
    /// <remarks> superreeen, 08.11.2013. </remarks>
    public class ScriptPubKey
    {
        /// <summary> Gets or sets the assembly. </summary>
        /// <value> The assembly. </value>
        public string Asm { get; set; }

        /// <summary> Gets or sets the hexadecimal. </summary>
        /// <value> The hexadecimal. </value>
        public string Hex { get; set; }

        /// <summary> Gets or sets the request signals. </summary>
        /// <value> The request signals. </value>
        public int ReqSigs { get; set; }

        /// <summary> Gets or sets the type. </summary>
        /// <value> The type. </value>
        public string Type { get; set; }

        /// <summary> Gets or sets the addresses. </summary>
        /// <value> The addresses. </value>
        public List<string> Addresses { get; set; }
    }
}
