using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Base.Core.Managers
{
    [Serializable]
    public class Player : BaseManager
    {
        // Player Data
        public string CharacterName;
        
        // Player Followers Data
        public List<Citizen> FollowerCount = new();
        private int _startingFollowerAmount = 2;
        private int StartingProphetGiveAmount = 2;
        
        // Player Curses & Miracles system
        public Devotion Devotion;
        private int _startingDevotionAmount = 2;
        
        // Player Factions System
        public int Resources;
        public int Currency;
        
        // Prophet Stuff
        public Prophet prophet;
        
        public Player(Action<BaseManager> onComplete) : base(onComplete)
        {
            for (int i = 0; i < _startingFollowerAmount; i++)
            {
                Citizen follower = new Citizen();
                follower.ChangeAttractionAmount(3);
                follower.InitSanityHealthDuty();
                FollowerCount.Add(follower);
            }
            
            Devotion = new Devotion(_startingDevotionAmount);
            prophet = new Prophet(StartingProphetGiveAmount);
            
            Resources = 0;
            Currency = 10;
            
            CharacterName = GenerateName();
            OnInitComplete();
        }
        
        private string GenerateName()
        {
            string[] names = { "John", "Jane", "Alex", "Emily", "Michael", "Olivia", "David", "Sophia" };
            int index = UnityEngine.Random.Range(0, names.Length);
            return names[index];
        }
    }
    
    public class Prophet
    {
        public string ProphetName;
        public int ProphetGiveAmount;
        private ProphetAction actions;
        public bool active;
        public int ProphetStat;
        
        public Prophet(int prophetGiveAmount)
        {
            ProphetGiveAmount = prophetGiveAmount;
            active = true;
            ProphetStat = 5; // 0-50 or something, this is initial value
        }

        public void DoAction(ProphetAction action, List<Citizen> targetCitizens)
        {
            switch (action)
            {
                case ProphetAction.Pray:
                case ProphetAction.Preach:
                    
                    foreach (var citizen in targetCitizens)
                    {
                        citizen.ChangeAttractionAmount(ProphetGiveAmount);
                        Debug.Log($"A <color=red>{action}</color> is being preformed... {citizen.CitizenName}'s Player God Attraction is now {citizen.PlayerGodAttraction}");
                    }
                    break;

                case ProphetAction.Infiltrate:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }
        }
    }
    
    public enum ProphetAction
    {
        Pray,
        Preach,
        Infiltrate
    }
}