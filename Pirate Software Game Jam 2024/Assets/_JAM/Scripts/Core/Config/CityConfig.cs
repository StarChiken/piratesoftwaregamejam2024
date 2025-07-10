using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Configuration data for initializing a City.
/// </summary>
[CreateAssetMenu(fileName = "CityConfig", menuName = "Game/Config/City Config")]
public class CityConfig : BaseConfig
{
    [Header("Starting Values")]
    [SerializeField] private int startingDistrictsAmount = 9;
    [SerializeField] private int startingCitizenAmountPerDistrict = 10;
    [SerializeField] private int startingFactionGiveAmount = 2;
    
    [Header("Names")]
    [SerializeField] private List<string> districtNames = new()
    {
        "Tea Garden District", "Geisha Flower-Town", "Bonsai Terrace",
        "Lotus Market", "Silk Trade District", "Harmony Haven",
        "Zen Retreat Area", "Eternal Sakura Gardens", "Golden Pavilion Quarter",
        "Rice Fields District", "Bamboo Grove District", "Sake Streets",
        "Samurai Quarter", "Shogun Plaza", "Pagoda Heights",
        "Koi Pond District", "Maple Grove District", "Cherry Blossom Alley"
    };
    
    [SerializeField] private List<string> factionNames = new()
    {
        "Blossom Syndicate", "Zen Brotherhood", "Traders Guild", "Rice Consortium",
        "Shogun Authority", "Lotus Cartel", "Sake Association", "Golden Coalition", "Tea Garden Society"
    };
    
    [SerializeField] private List<string> cityNames = new()
    {
        "Edojima of Cherry Blossom", "Sakuragawa the Zen Retreat", "Hinodecho Heights", "Yamatomachi Bonsai Terrace",
        "Hanamachi Twilight Haven", "Nagareyama Woods Sanctuary", "Kyotopia of Cherry Blossom", "Osakamura the Zen Retreat",
        "Edojima Heights", "Sakuragawa Bonsai Terrace", "Hinodecho Twilight Haven", "Yamatomachi Woods Sanctuary",
        "Hanamachi of Cherry Blossom", "Nagareyama the Zen Retreat", "Kyotopia Heights"
    };

    // Public properties for backward compatibility
    public int StartingDistrictsAmount => startingDistrictsAmount;
    public int StartingCitizenAmountPerDistrict => startingCitizenAmountPerDistrict;
    public int StartingFactionGiveAmount => startingFactionGiveAmount;
    public List<string> DistrictNames => districtNames;
    public List<string> FactionNames => factionNames;
    public List<string> CityNames => cityNames;

    protected override string ConfigFileName => "CityConfig";
} 