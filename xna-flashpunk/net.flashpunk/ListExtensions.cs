using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace net.flashpunk
{
    public static class ListExtensions
    {
        /// <summary>
        /// Gets the value for the specified key or return the default value if the key is not in the array.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dict">The dict.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static TValue Get<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
        {
            return Get(dict, key, default(TValue));
        }

        /// <summary>
        /// Gets the value for the specified key or return the default value if the key is not in the array.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dict">The dict.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static TValue Get<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue defaultValue)
        {
            TValue value;
            if (dict.TryGetValue(key, out value))
                return value;
            return defaultValue;
        }
    }
}
