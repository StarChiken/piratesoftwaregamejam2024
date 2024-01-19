using System;
using UnityEngine;

namespace Base.Core.Managers
{
    public class RandomEvents : BaseManager
    {
        public RandomEvents(Action<BaseManager> onComplete) : base(onComplete)
        {
            OnInitComplete();
        }
        
        public string DoEventGiveDevotionPoints()
        {
            Debug.Log($"<color=red>A Random Event Happened!</color>");

            GameManager.Player.Devotion.ChangeDevotionAmount(5);
            
            return "The People Are Weirded Out By Our Practices. But Our Lord Is Merciful. He Bestowed us With More Power!";
        }
    }
}