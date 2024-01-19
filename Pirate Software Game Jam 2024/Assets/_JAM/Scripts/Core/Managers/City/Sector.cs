using System;
using System.Collections.Generic;

namespace Base.Core.Managers
{
    [Serializable]
    public class Sector
    {
        public string SectorName;
        public int SectorStat;
        public List<Citizen> SectorPopulace = new();
    }
}