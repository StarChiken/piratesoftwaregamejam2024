using System;
using System.Collections.Generic;

    /// <summary>
    /// Represents a faction in the city, with actions and alignment.
    /// </summary>
    public class Faction
    {
        public string FactionName { get; set; }
        public int FactionGiveAmount { get; set; }
        public int FactionAlignment { get; set; }
        public bool InFavor { get; set; }
        private readonly IGameManager _gameManager;

        public Faction(int factionGiveAmount, IGameManager gameManager = null)
        {
            FactionGiveAmount = factionGiveAmount;
            FactionAlignment = 10;
            _gameManager = gameManager ?? GameManager.Instance;
        }

        /// <summary>
        /// Performs a faction action using a strategy helper.
        /// </summary>
        public void DoAction(FactionAction factionAction)
        {
            FactionActionStrategy.Execute(this, factionAction, _gameManager);
        }
    }

    /// <summary>
    /// Strategy helper for faction actions.
    /// </summary>
    public static class FactionActionStrategy
    {
        public static void Execute(Faction faction, FactionAction action, IGameManager gameManager)
        {
            if (gameManager?.Player == null) return;
            
            switch (action)
            {
                case FactionAction.GetResource:
                    gameManager.Player.Resources += faction.InFavor ? faction.FactionGiveAmount * 2 : faction.FactionGiveAmount;
                    break;
                case FactionAction.GetFavor:
                    gameManager.Player.Resources += faction.FactionGiveAmount;
                    break;
                case FactionAction.GetInfluence:
                    if (gameManager.City?.Districts == null) return;
                    var list = gameManager.City.Districts;
                    var tempList = new List<Faction>();
                    foreach (var district in list)
                    {
                        if (district.DistrictFaction?.FactionName == faction.FactionName) continue;
                        tempList.Add(district.DistrictFaction);
                    }
                    if (tempList.Count > 0)
                    {
                        var selectedFaction = RandomUtil.GetRandom(tempList);
                        selectedFaction.InFavor = true;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public interface IGameManager
    {
        Player Player { get; }
        City City { get; }
    }

    public class Prophet
    {
        public string ProphetName { get; set; }
        public void DoAction(int amount, IGameManager gameManager)
        {
            gameManager.Player.Resources += amount;
        }
    }

