using System;
using System.Collections.Generic;
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
        
        // Player Curses & Miracles system
        public Devotion Devotion;
        private int _startingDevotionAmount = 2;
        
        // Player Factions System
        public int Resources;
        
        public Player(Action<BaseManager> onComplete) : base(onComplete)
        {
            for (int i = 0; i < _startingFollowerAmount; i++)
            {
                Citizen follower = new Citizen();
                follower.ChangeAttractionAmount(3);
                FollowerCount.Add(follower);
            }
            
            Devotion = new(_startingDevotionAmount);
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
}