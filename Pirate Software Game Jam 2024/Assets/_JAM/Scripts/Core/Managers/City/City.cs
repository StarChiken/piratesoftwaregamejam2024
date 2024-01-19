using System;
using System.Collections.Generic;

namespace Base.Core.Managers
{
    [Serializable]
    public class City : BaseManager
    {
        public List<Sector> SectorsCount = new();
        public int StartingSectorsAmount = 5;
        public string CityName;

        List<String> districtNames = new List<string> { "Tea Garden District", "Samurai Quarter", "Cherry Blossom Alley", "Zen Retreat Area",
            "Silk Trade District", "Bamboo Grove District", "Geisha Entertainment Zone", "Rice Fields District","Maple Grove District", "Shogun Plaza",
            "Pagoda Heights", "Lotus Market", "Sake Streets", "Golden Pavilion Quarter", "Koi Pond District", "Eternal Sakura Gardens",
            "Bonsai Terrace", "Harmony Haven" };
        
        public City(Action<BaseManager> onComplete) : base(onComplete)
        {
            for (int i = 0; i < StartingSectorsAmount; i++)
            {
                Sector sector = new();
                sector.SectorName = GenerateRandomDistrict();
                SectorsCount.Add(sector);
            }

            CityName = GenerateRandomCity();
            PopulateCity();
            
            OnInitComplete();
        }
        
        private void PopulateCity()
        {
            foreach (var sector in SectorsCount)
            {
                for (int i = 0; i < 10; i++)
                {
                    Citizen citizen = new Citizen();
                    sector.SectorPopulace.Add(citizen);
                }
            }
        }

        
        private string GenerateRandomCity()
        {
            string[] cityNames = { "Edojima", "Sakuragawa", "Hinodecho", "Yamatomachi", "Hanamachi", "Nagareyama", "Kyotopia", "Osakamura" };
            int index = UnityEngine.Random.Range(0, cityNames.Length);
            return cityNames[index];
        }

        private string GenerateRandomDistrict()
        {
            int index = UnityEngine.Random.Range(0, districtNames.Count);
            string selectedDistrict = districtNames[index];
            districtNames.RemoveAt(index);
            
            return selectedDistrict;
        }
    }
}