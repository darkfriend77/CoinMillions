// <copyright file="Lot.cs" company="CoinMillions">
// Copyright (c) 2013 CoinMillions. All rights reserved.
// </copyright>
// <author>superreeen</author>
// <date>09.11.2013</date>
// <summary>Implements the found numbers class</summary>
namespace CoinMillions.Service.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary> A lot. </summary>
    /// <remarks> superreeen, 09.11.2013. </remarks>
    public class Lot
    {
        /// <summary> Gets or sets the numbers. </summary>
        /// <value> The total number of s. </value>
        public int Numbers { get; set; }

        /// <summary> Gets or sets the stars. </summary>
        /// <value> The stars. </value>
        public int Stars { get; set; }

        /// <summary> Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <param name="obj"> The object to compare with the current object. </param>
        /// <returns> true if the specified object  is equal to the current object; otherwise, false. </returns>
        /// <seealso cref="M:System.Object.Equals(object)"/>
        public override bool Equals(object obj)
        {
            Lot other = obj as Lot;
            if (other != null)
                return other.Numbers.Equals(Numbers) && other.Stars.Equals(Stars);
            return base.Equals(obj);
        }

        /// <summary> Serves as a hash function for a particular type. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <returns> A hash code for the current <see cref="T:System.Object" />. </returns>
        /// <seealso cref="M:System.Object.GetHashCode()"/>
        public override int GetHashCode()
        {
            return Numbers.GetHashCode() ^ Stars.GetHashCode();
        }
    }
}
