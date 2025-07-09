using System;
using System.Collections.Generic;

namespace Base.Core.Managers
{
    /// <summary>
    /// Provides random names from a list, removing them as they are used.
    /// </summary>
    public class NameProvider
    {
        private readonly List<string> _names;
        public NameProvider(List<string> names)
        {
            _names = new List<string>(names);
        }
        public string TakeRandom()
        {
            if (_names.Count == 0)
                throw new InvalidOperationException("No names left to provide.");
            int index = UnityEngine.Random.Range(0, _names.Count);
            string value = _names[index];
            _names.RemoveAt(index);
            return value;
        }
        public int Count => _names.Count;
    }
} 