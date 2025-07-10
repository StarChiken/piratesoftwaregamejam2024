using System;
using System.Collections.Generic;


    /// <summary>
    /// Provides random player names from a list, removing them as they are used.
    /// </summary>
    public class PlayerNameProvider
    {
        private readonly List<string> m_names;
        public PlayerNameProvider(List<string> names)
        {
            m_names = new List<string>(names);
        }
        public string TakeRandom()
        {
            if (m_names.Count == 0)
            {
                // Return a fallback name instead of throwing an exception
                return "Player" + UnityEngine.Random.Range(1000, 9999);
            }
            int index = UnityEngine.Random.Range(0, m_names.Count);
            string value = m_names[index];
            m_names.RemoveAt(index);
            return value;
        }
        public int Count => m_names.Count;
    }
