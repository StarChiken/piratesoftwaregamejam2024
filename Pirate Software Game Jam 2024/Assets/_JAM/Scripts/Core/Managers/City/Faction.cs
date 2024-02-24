using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Base.Core.Managers
{
    public class Faction
    {
        private FactionAction actions;
        public string FactionName;
        public int FactionGiveAmount;
        public int FactionAlignment;
        public bool InFavor;
        public Faction(int factionGiveAmount)
        {
            FactionGiveAmount = factionGiveAmount;
            FactionAlignment = 10;
        }

        public void DoAction(FactionAction factionAction)
        {
            switch (actions)
            {
                case FactionAction.GetResource:
                    GameManager.Instance.Player.Resources += InFavor ? FactionGiveAmount * 2 : FactionGiveAmount;
                    break;
                
                case FactionAction.GetFavor:
                    GameManager.Instance.Player.Resources += FactionGiveAmount;
                    break;
                
                case FactionAction.GetInfluence:
                    var list = GameManager.Instance.City.Sectors;
                    
                    List<Faction> tempList = new List<Faction>();
                    
                    foreach (var sector in list)
                    {
                        if (sector.SectorFaction.FactionName == FactionName) continue;
                        tempList.Add(sector.SectorFaction);
                    }
                    
                    int index = UnityEngine.Random.Range(0, tempList.Count);
                    var selectedFaction = tempList[index];
                    selectedFaction.InFavor = true;
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }
    }
    
    public class Prophet
    {
        public string ProphetName;

        public void DoAction(int amount)
        {
            GameManager.Instance.Player.Resources += amount;
        }
    }
    
    public enum FactionAction
    {
        GetResource,
        GetFavor,
        GetInfluence
    }
}