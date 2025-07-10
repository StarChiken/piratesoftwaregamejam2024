using System;
using System.Collections.Generic;

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
        private readonly CityConfig m_config;
        private readonly NameProvider<string> m_districtNameProvider;
        private readonly NameProvider<string> m_factionNameProvider;
        private readonly NameProvider<string> m_cityNameProvider;
        private readonly ICitizenFactory m_citizenFactory;

        /// <summary>
        /// The name of the city.
        /// </summary>
        public string CityName { get; private set; }
        /// <summary>
        /// The list of districts in the city.
        /// </summary>
        public IReadOnlyList<District> Districts => m_districts;
        private readonly List<District> m_districts = new();

        /// <summary>
        /// Initializes a new City and its districts/factions using the provided configuration.
        /// </summary>
        /// <param name="config">Configuration for the city.</param>
        /// <param name="citizenFactory">Factory for creating citizens.</param>
        /// <param name="onComplete">Callback when initialization is complete.</param>
        public City(CityConfig config, ICitizenFactory citizenFactory, Action<BaseManager> onComplete) : base(onComplete)
        {
            if (onComplete == null)
                throw new ArgumentNullException(nameof(onComplete), "Completion callback cannot be null.");
            m_config = config ?? throw new ArgumentNullException(nameof(config));
            m_citizenFactory = citizenFactory ?? throw new ArgumentNullException(nameof(citizenFactory));
            m_districtNameProvider = new NameProvider<string>(m_config.DistrictNames, () => "District" + UnityEngine.Random.Range(1000, 9999));
            m_factionNameProvider = new NameProvider<string>(m_config.FactionNames, () => "Faction" + UnityEngine.Random.Range(1000, 9999));
            m_cityNameProvider = new NameProvider<string>(m_config.CityNames, () => "City" + UnityEngine.Random.Range(1000, 9999));
            InitializeCity();
            OnInitComplete();
        }

        /// <summary>
        /// Initializes the city districts and populates them.
        /// </summary>
        private void InitializeCity()
        {
            for (int i = 0; i < m_config.StartingDistrictsAmount; i++)
            {
                District district = new();
                district.DistrictName = m_districtNameProvider.TakeRandom();
                district.DistrictType = DistrictTypeMapper.GetDistrictType(district.DistrictName);
                PopulateDistrict(district);
                m_districts.Add(district);
            }
            InitFactions();
            CityName = m_cityNameProvider.TakeRandom();
        }

        /// <summary>
        /// Initializes factions for each district.
        /// </summary>
        private void InitFactions()
        {
            foreach (var district in m_districts)
            {
                district.DistrictFaction = new Faction(m_config.StartingFactionGiveAmount, m_gameManager);
                district.DistrictFaction.FactionName = m_factionNameProvider.TakeRandom();
            }
        }

        /// <summary>
        /// Populates a district with citizens using the citizen factory.
        /// </summary>
        /// <param name="district">The district to populate.</param>
        private void PopulateDistrict(District district)
        {
            if (district == null)
                throw new ArgumentNullException(nameof(district), "District cannot be null.");
            for (int i = 0; i < m_config.StartingCitizenAmountPerDistrict; i++)
            {
                Citizen citizen = m_citizenFactory.CreateCitizen();
                district.DistrictPopulace.Add(citizen);
            }
        }
    }