// <copyright file="Extensions.cs" company="CoinMillions">
// Copyright (c) 2013 CoinMillions. All rights reserved.
// </copyright>
// <author>superreeen</author>
// <date>09.11.2013</date>
// <summary>Implements the extensions class</summary>
namespace CoinMillions.BitcoinClient
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    /// <summary> An extensions. </summary>
    /// <remarks> superreeen, 09.11.2013. </remarks>
    internal static class Extensions
    {
        /// <summary> A decimal extension method that converts a value to a bitcoin value. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <param name="value"> The value to act on. </param>
        /// <returns> value as a string. </returns>
        public static decimal ToBitcoinValue(this decimal value)
        {
            return decimal.Parse(Math.Round(value - 0.000000005M, 8, MidpointRounding.AwayFromZero).ToString("0.00000000", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
        }
    }
}
