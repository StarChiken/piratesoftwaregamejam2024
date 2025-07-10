using System;
using UnityEngine;
using System.Collections.Generic;

    /// <summary>
    /// Manages random game events and their execution.
    /// </summary>
    public class RandomEvents : BaseManager
    {
        private readonly RandomEventsConfig m_config;
        private GameEventType m_currentEvent;
        
        public RandomEvents(RandomEventsConfig config, Action<BaseManager> onComplete) : base(onComplete)
        {
            m_config = config ?? throw new ArgumentNullException(nameof(config));
            OnInitComplete();
        }
        
        public string DoEventGiveDevotionPoints()
        {
            GameManager.Player.Devotion.ChangeDevotionAmount(5);
            return m_config.EventMessages[GameEventType.GiveDevotionPoints];
        }

        public bool CheckEvents()
        {
            switch (m_currentEvent)
            {
                case GameEventType.GiveHappiness:
                    Debug.Log("<color=red>A Random Event Happened!</color>");
                    return CheckHappinessEvent();
                case GameEventType.GiveDevotionPoints:
                    Debug.Log("<color=red>A Random Event Happened!</color>");
                    DoEventGiveDevotionPoints();
                    return true;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private bool CheckHappinessEvent()
        {
            int totalHappiness = CalculateTotalHappiness();
            if (totalHappiness <= m_config.HappinessThreshold)
            {
                // Trigger a give happiness event
                return true;
            }
            return false;
        }

        private int CalculateTotalHappiness()
        {
            int totalHappiness = 0;
            var districts = GameManager.City.Districts;
            foreach (var district in districts)
            {
                var districtPop = district.DistrictPopulace;
                foreach (Citizen citizen in districtPop)
                {
                    totalHappiness += citizen.Happiness;
                }
            }
            return totalHappiness;
        }
    }