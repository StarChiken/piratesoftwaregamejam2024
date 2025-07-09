using System;
using System.Collections.Generic;

namespace Base.Core.Managers
{
    /// <summary>
    /// Configuration data for initializing a Player.
    /// </summary>
    [Serializable]
    public class PlayerConfig
    {
        public int StartingFollowerAmount = 2;
        public int StartingDevotionAmount = 2;
        public List<string> PlayerNames = new()
        {
            "John", "Jane", "Alex", "Emily", "Michael", "Olivia", "David", "Sophia"
        };
    }
} 