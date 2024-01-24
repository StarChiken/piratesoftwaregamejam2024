using System;
using UnityEngine;

namespace Base.Core.Managers
{
    public class RandomEvents : BaseManager
    {
        private GameEventType Events;
        private int happinessThreshold;
        
        public RandomEvents(Action<BaseManager> onComplete) : base(onComplete)
        {
            OnInitComplete();
        }
        
        public string DoEventGiveDevotionPoints()
        {
            GameManager.Player.Devotion.ChangeDevotionAmount(5);
            return "The People Are Weirded Out By Our Practices. But Our Lord Is Merciful. He Bestowed us With More Power!";
        }

        public bool CheckEvents()
        {
            switch (Events)
            {
                case GameEventType.GiveHappiness: // check with aan Event MonoBhaviour component, if to initiate the happiness event
                    
                    Debug.Log($"<color=red>A Random Event Happened!</color>");
                    
                    return CheckHappinessEvent();
                
                case GameEventType.GiveDevotionPoints: // silly example how you can still do something by type without returning the bool
                    
                    Debug.Log($"<color=red>A Random Event Happened!</color>");
                    
                    DoEventGiveDevotionPoints();
                    
                    return true;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private bool CheckHappinessEvent()
        {
            int totalHappiness = CalculateTotalHappiness();
            
            if (totalHappiness <= happinessThreshold)
            {
                // Trigger a give happiness event
                return true;
            }

            return false;
        }

        private int CalculateTotalHappiness()
        {
            int totalHappiness = 0;

            foreach (Citizen citizen in GameManager.City.CityPopulace)
            {
                totalHappiness += citizen.Happiness;
            }

            return totalHappiness;
        }
    }
    public enum GameEventType
    {
        GiveDevotionPoints,
        GiveHappiness,
    }
}