// <copyright file="PossibleLot.cs" company="CoinMillions">
// Copyright (c) 2013 CoinMillions. All rights reserved.
// </copyright>
// <author>superreeen</author>
// <date>09.11.2013</date>
// <summary>Implements the possible lot class</summary>
namespace CoinMillions.Service.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary> A possible lot. </summary>
    /// <remarks> superreeen, 09.11.2013. </remarks>
    /// <seealso cref="T:CoinMillions.Service.Base.Lot"/>
    public class PossibleLot : Lot
    {
        /// <summary> Gets or sets the probability. </summary>
        /// <value> The probability. </value>
        public decimal Probability { get; set; }

        /// <summary> Gets or sets the gain. </summary>
        /// <value> The gain. </value>
        public decimal Gain { get; set; }

        /// <summary> Returns a string that represents the current object. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <returns> A string that represents the current object. </returns>
        /// <seealso cref="M:System.Object.ToString()"/>
        public override string ToString()
        {
            return string.Format("Numbers: {0}, Stars: {1}, Probability: {2}, Gain: {3}", Numbers, Stars, Probability, Gain);
        }
    }
}
