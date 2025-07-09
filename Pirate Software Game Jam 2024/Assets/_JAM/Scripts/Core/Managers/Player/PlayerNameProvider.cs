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
            {
                // Return a fallback name instead of throwing an exception
                return "Player" + UnityEngine.Random.Range(1000, 9999);
            }
            int index = UnityEngine.Random.Range(0, _names.Count);
            string value = _names[index];
            _names.RemoveAt(index);
            return value;
        }
        public int Count => _names.Count;
    }
} 