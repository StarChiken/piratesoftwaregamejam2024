using System;
using System.Collections.Generic;
using UnityEngine;
using Base.Core.Managers;

namespace Base.Core.Config
{
    /// <summary>
    /// Configuration for random events, including thresholds and messages.
    /// </summary>
    [CreateAssetMenu(fileName = "RandomEventsConfig", menuName = "Game/Config/Random Events Config")]
    public class RandomEventsConfig : BaseConfig
    {
        [Header("Event Settings")]
        [SerializeField] private int happinessThreshold = 10;
        
        [Header("Event Messages")]
        [SerializeField] private Dictionary<GameEventType, string> eventMessages = new()
        {
            { GameEventType.GiveDevotionPoints, "The People Are Weirded Out By Our Practices. But Our Lord Is Merciful. He Bestowed us With More Power!" },
            { GameEventType.GiveHappiness, "A Random Happiness Event Occurred!" }
        };

        // Public properties for backward compatibility
        public int HappinessThreshold => happinessThreshold;
        public Dictionary<GameEventType, string> EventMessages => eventMessages;

        protected override string ConfigFileName => "RandomEventsConfig";
    }
} 