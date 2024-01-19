using System;
using System.Collections.Generic;
using Base.Core.Components;
using Base.Core.Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Base.Gameplay.UI
{
    public class MiracleButton : MyMonoBehaviour
    {
        public MiracleType miracleType;
        public Gameplay gameplayManager;
        
        public void DoMiracleButton()
        {
            var devotionPoints = GameManager.Player.Devotion.DevotionPoints;
            var citySectorsList = GameManager.City.SectorsCount;
            
            if (devotionPoints > 0)
            {
                GameManager.Player.Devotion.ChangeDevotionAmount(-1);
                
                var randomSector = RandomSector(citySectorsList);

                Debug.Log($"A <color=red>{miracleType}</color> is being cast on {randomSector.SectorName}");
                
                gameplayManager.DoMiracleOnSector(randomSector, miracleType);
                gameplayManager.DoMiracleOnFollowers(miracleType);
                gameplayManager.ChangeState(GameState.CalculateCityPhase);
            }
            else
            {
                Debug.Log($"Not enough Devotion Points, currently <color=red>{devotionPoints}</color");
                gameplayManager.ChangeState(GameState.CalculateCityPhase);
            }
        }
        
        private static Sector RandomSector(List<Sector> citySectors)
        {
            int index = UnityEngine.Random.Range(0, citySectors.Count);
            var randomSector = citySectors[index];
            return randomSector;
        }
    }
}