using System;
using System.Collections.Generic;

namespace Base.Core.Managers
{
    /// <summary>
    /// Represents a city district, its type, name, stats, populace, and faction.
    /// </summary>
    [Serializable]
    public class District
    {
        public DistrictType DistrictType { get; set; }
        public string DistrictName { get; set; }
        public int DistrictStat { get; set; }
        public List<Citizen> DistrictPopulace { get; } = new();
        public Faction DistrictFaction { get; set; }

        /// <summary>
        /// Performs the district's main action using a strategy helper.
        /// </summary>
        public void DoAction()
        {
            DistrictActionStrategy.Execute(this);
        }
    }

    /// <summary>
    /// Strategy helper for district actions.
    /// </summary>
    public static class DistrictActionStrategy
    {
        public static void Execute(District district)
        {
            switch (district.DistrictType)
            {
                case DistrictType.Entertainment:
                case DistrictType.Park:
                    foreach (var citizen in district.DistrictPopulace)
                    {
                        citizen.Happiness += 1;
                    }
                    break;
                case DistrictType.Market:
                    // GameManager.Instance.Player.Resources.AddResources(5);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum DistrictType
    {
        Default,
        Entertainment,
        Park,
        Market,
        Labor,
        Samurai
    }
}