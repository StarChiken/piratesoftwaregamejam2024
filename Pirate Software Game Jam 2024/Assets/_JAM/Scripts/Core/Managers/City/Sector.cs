using System;
using System.Collections.Generic;

namespace Base.Core.Managers
{
    [Serializable]
    public class Sector
    {
        public SectorType sectorType;
        public string SectorName;
        public int SectorStat;
        public List<Citizen> SectorPopulace = new();
        public Faction SectorFaction;
        
        public void DoAction()
        {
            switch (sectorType)
            {
                //Get Happiness for Followers
                case SectorType.Entertainment:
                case SectorType.Park:
                    foreach (var citizen in SectorPopulace)
                    {
                        citizen.Health += 1;
                    }
                    break;
                // Get a bit of Resources
                case SectorType.Market:
                   // GameManager.Instance.Player.Resources.AddResources(5);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
    public enum SectorType
    {
        Default,
        Entertainment,
        Park,
        Market,
        Labor,
        Samurai
    }
}