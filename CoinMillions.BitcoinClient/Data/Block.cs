// <copyright file="Block.cs" company="CoinMillions">
// Copyright (c) CoinMillions. All rights reserved.
// </copyright>
// <author>superreeen</author>
// <date>08.11.2013</date>
// <summary>Implements the block class</summary>
namespace CoinMillions.BitcoinClient.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary> A block. </summary>
    /// <remarks> superreeen, 08.11.2013. </remarks>
    public class Block
    {
        /// <summary> Gets or sets the hash. </summary>
        /// <value> The hash. </value>
        public string Hash { get; set; }

        /// <summary> Gets or sets the confirmations. </summary>
        /// <value> The confirmations. </value>
        public int Confirmations { get; set; }

        /// <summary> Gets or sets the size. </summary>
        /// <value> The size. </value>
        public int Size { get; set; }

        /// <summary> Gets or sets the height. </summary>
        /// <value> The height. </value>
        public int Height { get; set; }

        /// <summary> Gets or sets the version. </summary>
        /// <value> The version. </value>
        public int Version { get; set; }

        /// <summary> Gets or sets the merkle root. </summary>
        /// <value> The merkle root. </value>
        public string MerkleRoot { get; set; }

        /// <summary> Gets or sets the transmit. </summary>
        /// <value> The transmit. </value>
        public List<string> Tx { get; set; }

        /// <summary> Gets or sets the time. </summary>
        /// <value> The time. </value>
        public int Time { get; set; }

        /// <summary> Gets or sets the nonce. </summary>
        /// <value> The nonce. </value>
        public string Nonce { get; set; }

        /// <summary> Gets or sets the bits. </summary>
        /// <value> The bits. </value>
        public string Bits { get; set; }

        /// <summary> Gets or sets the difficulty. </summary>
        /// <value> The difficulty. </value>
        public float Difficulty { get; set; }

        /// <summary> Gets or sets the previous block hash. </summary>
        /// <value> The previous block hash. </value>
        public string PreviousBlockHash { get; set; }

        /// <summary> Gets or sets the next block hash. </summary>
        /// <value> The next block hash. </value>
        public string NextBlockHash { get; set; }
    }
}
