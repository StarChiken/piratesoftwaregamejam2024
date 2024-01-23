using System;
using System.Collections.Generic;

namespace Base.Core.Managers
{
    [Serializable]
    public class City : BaseManager
    {   public List<Citizen> CityPopulace = new();
        public int StartingPopulaceAmount = 15;
        public string CityName;
        
        public City(Action<BaseManager> onComplete) : base(onComplete)
        {
            CityName = GenerateRandomCity();
            PopulateCity();
            
            OnInitComplete();
        }
        
        private void PopulateCity()
        {
            for (int i = 0; i <= StartingPopulaceAmount; i++)
            {
                Citizen citizen = new Citizen();
                CityPopulace.Add(citizen);
            }
        }
        
        private string GenerateRandomCity()
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