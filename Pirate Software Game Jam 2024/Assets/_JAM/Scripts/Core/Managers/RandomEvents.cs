using System;
using UnityEngine;
using System.Collections.Generic;

    /// <summary>
    /// Manages random game events and their execution.
    /// </summary>
    public class RandomEvents : BaseManager
    {
        private readonly RandomEventsConfig _config;
        private GameEventType _currentEvent;
        
        public RandomEvents(RandomEventsConfig config, Action<BaseManager> onComplete) : base(onComplete)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            OnInitComplete();
        }
        
        public string DoEventGiveDevotionPoints()
        {
            GameManager.Player.Devotion.ChangeDevotionAmount(5);
            return _config.EventMessages[GameEventType.GiveDevotionPoints];
        }

        public bool CheckEvents()
        {
            switch (_currentEvent)
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
            if (totalHappiness <= _config.HappinessThreshold)
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