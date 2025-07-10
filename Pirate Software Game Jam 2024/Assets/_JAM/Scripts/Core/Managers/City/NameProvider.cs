using System;
using System.Collections.Generic;


    /// <summary>
    /// Generic provider for random items from a list, removing them as they are used.
    /// </summary>
    /// <typeparam name="T">The type of items to provide.</typeparam>
    public class NameProvider<T> where T : class
    {
        private readonly List<T> _items;
        private readonly Func<T> _fallbackProvider;

        /// <summary>
        /// Initializes a new NameProvider with a list of items.
        /// </summary>
        /// <param name="items">The list of items to provide from.</param>
        /// <param name="fallbackProvider">Optional fallback provider when items are exhausted.</param>
        public NameProvider(List<T> items, Func<T> fallbackProvider = null)
        {
            _items = new List<T>(items);
            _fallbackProvider = fallbackProvider;
        }

        /// <summary>
        /// Takes a random item from the list and removes it.
        /// </summary>
        /// <returns>A randomly selected item.</returns>
        public T TakeRandom()
        {
            if (_items.Count == 0)
            {
                return _fallbackProvider?.Invoke() ?? throw new InvalidOperationException("No items left to provide.");
            }
            return RandomUtil.TakeRandom(_items);
        }

        /// <summary>
        /// Gets the number of remaining items.
        /// </summary>
        public int Count => _items.Count;
    }
