// <copyright file="Extensions.cs" company="CoinMillions">
// Copyright (c) 2013 CoinMillions. All rights reserved.
// </copyright>
// <author>superreeen</author>
// <date>09.11.2013</date>
// <summary>Implements the extensions class</summary>
namespace CoinMillions.Service.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary> An extensions. </summary>
    /// <remarks> superreeen, 09.11.2013. </remarks>
    public static class Extensions
    {
        /// <summary> A Dictionary&lt;TKey,TValue&gt; extension method that adds an or update. </summary>
        /// <remarks> superreeen, 09.11.2013. </remarks>
        /// <typeparam name="TKey"> Type of the key. </typeparam>
        /// <typeparam name="TValue"> Type of the value. </typeparam>
        /// <param name="outputs"> The outputs to act on. </param>
        /// <param name="key"> The key. </param>
        /// <param name="newValue"> The new value. </param>
        public static void AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> outputs, TKey key, Func<TValue, TValue> newValue)
        {
            if (outputs.ContainsKey(key))
            {
                outputs[key] = newValue(outputs[key]);
            }
            else
            {
                outputs.Add(key, newValue(default(TValue)));
            }
        }
    }
}
