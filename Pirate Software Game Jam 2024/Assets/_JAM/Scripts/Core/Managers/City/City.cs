using System;
using System.Collections.Generic;

namespace Base.Core.Managers
{
    /// <summary>
    /// Interface for creating Citizen instances.
    /// </summary>
    public interface ICitizenFactory
    {
        Citizen CreateCitizen();
    }

    /// <summary>
    /// Default implementation of ICitizenFactory.
    /// </summary>
    public class CitizenFactory : ICitizenFactory
    {
        public Citizen CreateCitizen()
        {
            return new Citizen();
        }
    }

    /// <summary>
    /// Manages the city, its districts, factions, and initialization logic.
    /// </summary>
    [Serializable]
    public class City : BaseManager
    {
        private readonly CityConfig _config;
        private readonly NameProvider<string> _districtNameProvider;
        private readonly NameProvider<string> _factionNameProvider;
        private readonly NameProvider<string> _cityNameProvider;
        private readonly ICitizenFactory _citizenFactory;

        /// <summary>
        /// The name of the city.
        /// </summary>
        public string CityName { get; private set; }
        /// <summary>
        /// The list of districts in the city.
        /// </summary>
        public IReadOnlyList<District> Districts => _districts;
        private readonly List<District> _districts = new();

        /// <summary>
        /// Initializes a new City and its districts/factions using the provided configuration.
        /// </summary>
        /// <param name="config">Configuration for the city.</param>
        /// <param name="citizenFactory">Factory for creating citizens.</param>
        /// <param name="onComplete">Callback when initialization is complete.</param>
        public City(CityConfig config, ICitizenFactory citizenFactory, Action<BaseManager> onComplete) : base(onComplete)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _citizenFactory = citizenFactory ?? throw new ArgumentNullException(nameof(citizenFactory));
            _districtNameProvider = new NameProvider<string>(_config.DistrictNames, () => "District" + UnityEngine.Random.Range(1000, 9999));
            _factionNameProvider = new NameProvider<string>(_config.FactionNames, () => "Faction" + UnityEngine.Random.Range(1000, 9999));
            _cityNameProvider = new NameProvider<string>(_config.CityNames, () => "City" + UnityEngine.Random.Range(1000, 9999));
            InitializeCity();
            OnInitComplete();
        }

        /// <summary>
        /// Initializes the city districts and populates them.
        /// </summary>
        private void InitializeCity()
        {
            for (int i = 0; i < _config.StartingDistrictsAmount; i++)
            {
                District district = new();
                district.DistrictName = _districtNameProvider.TakeRandom();
                district.DistrictType = DistrictTypeMapper.GetDistrictType(district.DistrictName);
                PopulateDistrict(district);
                _districts.Add(district);
            }
            InitFactions();
            CityName = _cityNameProvider.TakeRandom();
        }

        /// <summary>
        /// Initializes factions for each district.
        /// </summary>
        private void InitFactions()
        {
            foreach (var district in _districts)
            {
                district.DistrictFaction = new Faction(_config.StartingFactionGiveAmount);
                district.DistrictFaction.FactionName = _factionNameProvider.TakeRandom();
            }
        }

        /// <summary>
        /// Populates a district with citizens using the citizen factory.
        /// </summary>
        /// <param name="district">The district to populate.</param>
        private void PopulateDistrict(District district)
        {
            for (int i = 0; i < _config.StartingCitizenAmountPerDistrict; i++)
            {
                Citizen citizen = _citizenFactory.CreateCitizen();
                district.DistrictPopulace.Add(citizen);
            }
        }
    }
}