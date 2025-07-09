using System;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Core.Config
{
    /// <summary>
    /// Configuration data for initializing a Player.
    /// </summary>
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Game/Config/Player Config")]
    public class PlayerConfig : BaseConfig
    {
        [Header("Starting Values")]
        [SerializeField] private int startingFollowerAmount = 2;
        [SerializeField] private int startingDevotionAmount = 2;
        
        [Header("Names")]
        [SerializeField] private List<string> playerNames = new()
        {
            "John", "Jane", "Alex", "Emily", "Michael", "Olivia", "David", "Sophia"
        };

        // Public properties for backward compatibility
        public int StartingFollowerAmount => startingFollowerAmount;
        public int StartingDevotionAmount => startingDevotionAmount;
        public List<string> PlayerNames => playerNames;

        protected override string ConfigFileName => "PlayerConfig";
    }
} 