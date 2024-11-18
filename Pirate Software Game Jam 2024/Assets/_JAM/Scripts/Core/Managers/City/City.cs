using System;
using System.Collections.Generic;

namespace Base.Core.Managers
{
    [Serializable]
    public class City : BaseManager
    {   
        // public List<Citizen> CityPopulace = new();
        // public int StartingPopulaceAmount = 15;
        // public string CityName;
        
        
        public int StartingSectorsAmount = 9;
        public int StartingCitizenAmountPerDistrict = 10;
        public int StartingFactionGiveAmount = 2;
        
        public string CityName;
        public List<Sector> Sectors = new();
        
        private List<String> districtNames = new() { 
            "Tea Garden District", "Geisha Flower-Town", "Bonsai Terrace" ,
            "Lotus Market", "Silk Trade District", "Harmony Haven" ,
            "Zen Retreat Area", "Eternal Sakura Gardens", "Golden Pavilion Quarter" ,
            "Rice Fields District", "Bamboo Grove District" , "Sake Streets" ,
            "Samurai Quarter", "Shogun Plaza" , "Pagoda Heights" ,
            "Koi Pond District", "Maple Grove District" , "Cherry Blossom Alley" ,
        };
        
        private List<String> factionNames = new() {             
            "Blossom Syndicate",
            "Zen Brotherhood",
            "Traders Guild",
            "Rice Consortium",
            "Shogun Authority",
            "Lotus Cartel",
            "Sake Association",
            "Golden Coalition",
            "Tea Garden Society" };
        
        public City(Action<BaseManager> onComplete) : base(onComplete)
        {
            InitializeCity();
            
            OnInitComplete();
        }

        private string RandomFactionName()
        {
            int index = UnityEngine.Random.Range(0, factionNames.Count);
            string selectedFactionName = factionNames[index];
            factionNames.RemoveAt(index);
            
            return selectedFactionName;
        }

        private void InitializeCity()
        {
            for (int i = 0; i < StartingSectorsAmount; i++)
            {
                Sector sector = new();
                
                sector.SectorName = RandomDistrictName();
                sector.sectorType = SetSectorType(sector.SectorName);
                PopulateDistrict(sector);
                Sectors.Add(sector);
            }

            InitFactions();
            
            CityName = RandomCityName();
        }

        private void InitFactions()
        {
            foreach (var sector in Sectors)
            {
                sector.SectorFaction = new Faction(StartingFactionGiveAmount);
                sector.SectorFaction.FactionName = RandomFactionName();
            }
        }

        private void PopulateDistrict(Sector sector)
        {
            for (int i = 0; i < StartingCitizenAmountPerDistrict; i++)
            {
                Citizen citizen = new Citizen();
                citizen.InitializeCitizen();
                sector.SectorPopulace.Add(citizen);
            }
        }

        private SectorType SetSectorType(String name)
        {
            return name switch
            {
                "Tea Garden District" or "Geisha Flower-Town" or "Bonsai Terrace" => SectorType.Entertainment,
                "Lotus Market" or "Silk Trade District" or "Harmony Haven" => SectorType.Market,
                "Zen Retreat Area" or "Eternal Sakura Gardens" or "Golden Pavilion Quarter" => SectorType.Park,
                "Rice Fields District" or "Bamboo Grove District" or "Sake Streets" => SectorType.Labor,
                "Samurai Quarter" or "Shogun Plaza" or "Pagoda Heights" => SectorType.Samurai,
                "Koi Pond District" or "Maple Grove District" or "Cherry Blossom Alley" => SectorType.Default,
                _ => SectorType.Default
            };
        }

        private string RandomDistrictName()
        {
            int index = UnityEngine.Random.Range(0, districtNames.Count);
            string selectedDistrict = districtNames[index];
            districtNames.RemoveAt(index);
            
            return selectedDistrict;
        }
        
        private string RandomCityName()
        {
            string[] cityNames = {
                "Edojima of Cherry Blossom", "Sakuragawa the Zen Retreat", "Hinodecho Heights","Yamatomachi Bonsai Terrace",
                "Hanamachi Twilight Haven", "Nagareyama Woods Sanctuary", "Kyotopia of Cherry Blossom", "Osakamura the Zen Retreat",
                "Edojima Heights", "Sakuragawa Bonsai Terrace","Hinodecho Twilight Haven","Yamatomachi Woods Sanctuary","Hanamachi of Cherry Blossom",
                "Nagareyama the Zen Retreat","Kyotopia Heights"
            };
            
            int index = UnityEngine.Random.Range(0, cityNames.Length);
            return cityNames[index];
        }
    }
}