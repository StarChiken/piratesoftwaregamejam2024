using System;
using System.Collections.Generic;

    /// <summary>
    /// Maps district names to district types.
    /// </summary>
    public static class DistrictTypeMapper
    {
        private static readonly Dictionary<string, DistrictType> _mapping = new()
        {
            // Entertainment
            { "Tea Garden District", DistrictType.Entertainment },
            { "Geisha Flower-Town", DistrictType.Entertainment },
            { "Bonsai Terrace", DistrictType.Entertainment },
            // Market
            { "Lotus Market", DistrictType.Market },
            { "Silk Trade District", DistrictType.Market },
            { "Harmony Haven", DistrictType.Market },
            // Park
            { "Zen Retreat Area", DistrictType.Park },
            { "Eternal Sakura Gardens", DistrictType.Park },
            { "Golden Pavilion Quarter", DistrictType.Park },
            // Labor
            { "Rice Fields District", DistrictType.Labor },
            { "Bamboo Grove District", DistrictType.Labor },
            { "Sake Streets", DistrictType.Labor },
            // Samurai
            { "Samurai Quarter", DistrictType.Samurai },
            { "Shogun Plaza", DistrictType.Samurai },
            { "Pagoda Heights", DistrictType.Samurai },
            // Default
            { "Koi Pond District", DistrictType.Default },
            { "Maple Grove District", DistrictType.Default },
            { "Cherry Blossom Alley", DistrictType.Default },
        };
        public static DistrictType GetDistrictType(string districtName)
        {
            return _mapping.TryGetValue(districtName, out var type) ? type : DistrictType.Default;
        }
    }