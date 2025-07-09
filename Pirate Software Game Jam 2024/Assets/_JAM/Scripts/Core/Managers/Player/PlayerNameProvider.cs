using System;
using System.Collections.Generic;

namespace Base.Core.Managers
{
    /// <summary>
    /// Provides random player names from a list, removing them as they are used.
    /// </summary>
    public class PlayerNameProvider
    {
        private readonly List<string> _names;
        public PlayerNameProvider(List<string> names)
        {
            _names = new List<string>(names);
        }
        public string TakeRandom()
        {
            if (_names.Count == 0)
                throw new InvalidOperationException("No player names left to provide.");
            int index = UnityEngine.Random.Range(0, _names.Count);
            string value = _names[index];
            _names.RemoveAt(index);
            return value;
        }
        public int Count => _names.Count;
    }
} 