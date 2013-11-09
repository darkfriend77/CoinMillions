// <copyright file="UnspentInput.cs" company="CoinMillions">
// Copyright (c) 2013 CoinMillions. All rights reserved.
// </copyright>
// <author>superreeen</author>
// <date>09.11.2013</date>
// <summary>Implements the unspent input class</summary>
namespace CoinMillions.BitcoinClient.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary> An unspent input. </summary>
    /// <remarks> superreeen, 09.11.2013. </remarks>
    public class UnspentInput
    {
        /// <summary> Gets or sets the identifier of the transmit. </summary>
        /// <value> The identifier of the transmit. </value>
        public string TxId { get; set; }

        /// <summary> Gets or sets the out. </summary>
        /// <value> The v out. </value>
        public int VOut { get; set; }

        /// <summary> Gets or sets the address. </summary>
        /// <value> The address. </value>
        public string Address { get; set; }

        /// <summary> Gets or sets the account. </summary>
        /// <value> The account. </value>
        public string Account { get; set; }

        /// <summary> Gets or sets the script pub key. </summary>
        /// <value> The script pub key. </value>
        public string ScriptPubKey { get; set; }

        /// <summary> Gets or sets the amount. </summary>
        /// <value> The amount. </value>
        public decimal Amount { get; set; }

        /// <summary> Gets or sets the confirmations. </summary>
        /// <value> The confirmations. </value>
        public ulong Confirmations { get; set; }

        /// <summary> Returns a string that represents the current object. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <returns> A string that represents the current object. </returns>
        /// <seealso cref="M:System.Object.ToString()"/>
        public override string ToString()
        {
            return string.Format("txid: {0}, vout: {1}, address: {2}, account: {3}, scriptPubKey: {4}, amount: {5}, confirmations: {6}", TxId, VOut, Address, Account, ScriptPubKey, Amount, Confirmations);
        }
    }
}
