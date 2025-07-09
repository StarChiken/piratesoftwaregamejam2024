using System;
using System.Collections.Generic;

namespace Base.Core.Managers
{
    /// <summary>
    /// Configuration data for initializing a City.
    /// </summary>
    [Serializable]
    public class CityConfig
    {
        public int StartingDistrictsAmount = 9;
        public int StartingCitizenAmountPerDistrict = 10;
        public int StartingFactionGiveAmount = 2;
        public List<string> DistrictNames = new()
        {
            "Tea Garden District", "Geisha Flower-Town", "Bonsai Terrace",
            "Lotus Market", "Silk Trade District", "Harmony Haven",
            "Zen Retreat Area", "Eternal Sakura Gardens", "Golden Pavilion Quarter",
            "Rice Fields District", "Bamboo Grove District", "Sake Streets",
            "Samurai Quarter", "Shogun Plaza", "Pagoda Heights",
            "Koi Pond District", "Maple Grove District", "Cherry Blossom Alley"
        };
        public List<string> FactionNames = new()
        {
            "Blossom Syndicate", "Zen Brotherhood", "Traders Guild", "Rice Consortium",
            "Shogun Authority", "Lotus Cartel", "Sake Association", "Golden Coalition", "Tea Garden Society"
        };
        public List<string> CityNames = new()
        {
            "Edojima of Cherry Blossom", "Sakuragawa the Zen Retreat", "Hinodecho Heights", "Yamatomachi Bonsai Terrace",
            "Hanamachi Twilight Haven", "Nagareyama Woods Sanctuary", "Kyotopia of Cherry Blossom", "Osakamura the Zen Retreat",
            "Edojima Heights", "Sakuragawa Bonsai Terrace", "Hinodecho Twilight Haven", "Yamatomachi Woods Sanctuary",
            "Hanamachi of Cherry Blossom", "Nagareyama the Zen Retreat", "Kyotopia Heights"
        };
    }
} 