using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace Base.Core.Managers
{
    [Serializable]
    public class Sector
    {
        public SectorType sectorType;
        public string SectorName;
        public int SectorStat;
        public List<Citizen> SectorPopulace = new();
        public MainBuilding ActionBuilding;

        public Sector()
        {
            sectorType = ReturnSectorType();
            ActionBuilding = new MainBuilding(sectorType);
        }
        
        public SectorType ReturnSectorType()
        {
            switch (SectorName)
            {
                case "Tea Garden District":
                case "Bamboo Grove District":
                case "Geisha Entertainment Zone":
                case "Sake Streets":
                case "Golden Pavilion Quarter":
                    return SectorType.Entertainment;
                case "Rice Fields District":
                case "Maple Grove District":
                case "Shogun Plaza":
                case "Pagoda Heights":
                case "Lotus Market":
                case "Koi Pond District":
                case "Eternal Sakura Gardens":
                case "Bonsai Terrace":
                case "Harmony Haven":
                    return SectorType.Business;
                case "Samurai Quarter":
                case "Cherry Blossom Alley":
                case "Zen Retreat Area":
                case "Silk Trade District":
                    return SectorType.Park;
                default:
                    return SectorType.Default;
            }
        }
    }
    
    public class MainBuilding
    {
        public SectorType type;
        public ActionOptions options;
        public Sector sectorOfOrigion;

        public MainBuilding(SectorType sectorType)
        {
            type = sectorType;
        }

        public void DoAction()
        {
            switch (type)
            {
                //Get Happiness for Followers
                case SectorType.Entertainment:
                case SectorType.Park:
                    foreach (var citizen in sectorOfOrigion.SectorPopulace)
                    {
                        citizen.Happiness += 1;
                    }
                    break;
                // Get a bit of Resources
                case SectorType.Business:
                    GameManager.Instance.Player.Resources.AddResources(5);
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
        Business
    }
    
    public enum ActionOptions
    {
        DoAction,
        AskFavor,
        DoMiracle
    }
}