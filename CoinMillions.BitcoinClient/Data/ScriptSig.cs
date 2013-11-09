// <copyright file="ScriptSig.cs" company="CoinMillions">
// Copyright (c) 2013 CoinMillions. All rights reserved.
// </copyright>
// <author>superreeen</author>
// <date>08.11.2013</date>
// <summary>Implements the script signal class</summary>
namespace CoinMillions.BitcoinClient.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary> A script signal. </summary>
    /// <remarks> superreeen, 08.11.2013. </remarks>
    public class ScriptSig
    {
        /// <summary> Gets or sets the assembly. </summary>
        /// <value> The assembly. </value>
        public string Asm { get; set; }

        /// <summary> Gets or sets the hexadecimal. </summary>
        /// <value> The hexadecimal. </value>
        public string Hex { get; set; }
    }
}
