using System;
using UnityEngine;

    /// <summary>
    /// Coordinates core game managers and provides global access.
    /// </summary>
    public class GameManager : IGameManager
    {
        #region Fields
        private readonly Action m_onCompleteAction;
        [SerializeField, Tooltip("Reference to the ConfigManager for game setup")]
        private ConfigManager m_configManager;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the current player instance.
        /// </summary>
        public Player Player { get; private set; }

        /// <summary>
        /// Gets the current city instance.
        /// </summary>
        public City City { get; private set; }

        /// <summary>
        /// Gets the current game events instance.
        /// </summary>
        public RandomEvents GameEvents { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the GameManager class.
        /// </summary>
        /// <param name="onComplete">Callback to invoke when initialization is complete.</param>
        /// <param name="player">Optional player instance to use.</param>
        /// <param name="city">Optional city instance to use.</param>
        /// <param name="gameEvents">Optional game events instance to use.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when onComplete is null.</exception>
        public GameManager(Action onComplete, Player player = null, City city = null, RandomEvents gameEvents = null)
        {
            if (onComplete == null)
            {
                throw new System.ArgumentNullException(nameof(onComplete), "Completion callback cannot be null.");
            }

            try
            {
                m_onCompleteAction = onComplete;
                InitManagers(player, city, gameEvents);
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error initializing GameManager: {ex.Message}");
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Initializes the game managers with provided instances or loads from config.
        /// </summary>
        /// <param name="player">Optional player instance.</param>
        /// <param name="city">Optional city instance.</param>
        /// <param name="gameEvents">Optional game events instance.</param>
        private void InitManagers(Player player, City city, RandomEvents gameEvents)
        {
            try
            {
                if (player != null && city != null && gameEvents != null)
                {
                    Player = player;
                    City = city;
                    GameEvents = gameEvents;
                    m_onCompleteAction?.Invoke();
                    return;
                }

                // Load configs from ScriptableObjects
                var playerConfig = m_configManager?.GetConfig<PlayerConfig>();
                var cityConfig = m_configManager?.GetConfig<CityConfig>();
                var randomEventsConfig = m_configManager?.GetConfig<RandomEventsConfig>();

                if (playerConfig == null || cityConfig == null || randomEventsConfig == null)
                {
                    Debug.LogError("Failed to load required configs for GameManager initialization.");
                    return;
                }

                // Default initialization chain with loaded configs
                new Player(playerConfig, new CitizenFactory(), result =>
                {
                    try
                    {
                        Player = (Player)result;
                        new City(cityConfig, new CitizenFactory(), result =>
                        {
                            try
                            {
                                City = (City)result;
                                new RandomEvents(randomEventsConfig, result =>
                                {
                                    try
                                    {
                                        GameEvents = (RandomEvents)result;
                                        m_onCompleteAction?.Invoke();
                                    }
                                    catch (System.Exception ex)
                                    {
                                        Debug.LogError($"Error initializing RandomEvents: {ex.Message}");
                                    }
                                });
                            }
                            catch (System.Exception ex)
                            {
                                Debug.LogError($"Error initializing City: {ex.Message}");
                            }
                        });
                    }
                    catch (System.Exception ex)
                    {
                        Debug.LogError($"Error initializing Player: {ex.Message}");
                    }
                });
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error in InitManagers: {ex.Message}");
            }
        }
        #endregion
    }