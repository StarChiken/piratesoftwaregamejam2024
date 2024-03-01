using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Base.Core.Managers
{
    [Serializable]
    public class Faction
    {
        public string FactionName;
        public int FactionGiveAmount;
        public int FactionAlignment;
        public bool InFavor;
        
        private FactionAction actions;
        
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
                    Debug.Log($"A <color=red>{factionAction}</color> is being performed, Currency is now {GameManager.Instance.Player.Currency}, Resources are now {GameManager.Instance.Player.Resources}!");
                    InFavor = false;
                    break;
                
                case FactionAction.GetFavor:
                    InFavor = true;
                    Debug.Log($"A <color=red>{factionAction}</color> is being performed, you are now In Favor wit the {FactionName}!");
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
                    
                    Debug.Log($"A <color=red>{factionAction}</color> is being performed, you are now In Favor wit the {selectedFaction.FactionName}!");

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }
    }
    
    public enum FactionAction
    {
        GetResource,
        GetFavor,
        GetInfluence
    }
}