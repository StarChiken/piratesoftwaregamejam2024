using System;
using System.Collections.Generic;


    /// <summary>
    /// Generic provider for random items from a list, removing them as they are used.
    /// </summary>
    /// <typeparam name="T">The type of items to provide.</typeparam>
    public class NameProvider<T> where T : class
    {
        private readonly List<T> m_items;
        private readonly Func<T> m_fallbackProvider;

        /// <summary>
        /// Initializes a new NameProvider with a list of items.
        /// </summary>
        /// <param name="items">The list of items to provide from.</param>
        /// <param name="fallbackProvider">Optional fallback provider when items are exhausted.</param>
        public NameProvider(List<T> items, Func<T> fallbackProvider = null)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items), "Items list cannot be null.");
            m_items = new List<T>(items);
            m_fallbackProvider = fallbackProvider;
        }

        /// <summary>
        /// Takes a random item from the list and removes it.
        /// </summary>
        /// <returns>A randomly selected item.</returns>
        public T TakeRandom()
        {
            if (m_items.Count == 0)
            {
                return m_fallbackProvider?.Invoke() ?? throw new InvalidOperationException("No items left to provide.");
            }
            return RandomUtil.TakeRandom(m_items);
        }

        /// <summary>
        /// Gets the number of remaining items.
        /// </summary>
        public int Count => m_items.Count;
    }
