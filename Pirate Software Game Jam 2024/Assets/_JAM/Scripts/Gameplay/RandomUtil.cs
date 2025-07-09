using System;
using System.Collections.Generic;

namespace Base.Gameplay
{
    /// <summary>
    /// Utility methods for random operations on collections.
    /// </summary>
    public static class RandomUtil
    {
        /// <summary>
        /// Selects a random element from a list and removes it.
        /// </summary>
        /// <typeparam name="T">Type of list element.</typeparam>
        /// <param name="list">The list to select from.</param>
        /// <returns>The randomly selected element.</returns>
        public static T TakeRandom<T>(List<T> list)
        {
            if (list == null || list.Count == 0)
                throw new ArgumentException("List is null or empty");
            int index = UnityEngine.Random.Range(0, list.Count);
            T value = list[index];
            list.RemoveAt(index);
            return value;
        }
    }
} 