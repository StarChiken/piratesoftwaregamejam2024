using System;
using UnityEngine;

    /// <summary>
    /// Coordinates core game managers and provides global access.
    /// </summary>
    public class GameManager : IGameManager
    {
        #region Fields
        private static GameManager s_instance;
        private readonly Action m_onCompleteAction;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the GameManager instance. Must be properly initialized.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Thrown when GameManager is not initialized.</exception>
        public static GameManager Instance
        {
            get
            {
                if (s_instance == null)
                {
                    throw new System.InvalidOperationException("GameManager instance is null! Ensure it's properly initialized.");
                }
                return s_instance;
            }
            private set
            {
                if (s_instance != null && s_instance != value)
                {
                    Debug.LogWarning("GameManager instance is being overwritten. This may indicate a setup issue.");
                }
                s_instance = value;
            }
        }

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
                if (s_instance == null)
                {
                    Instance = this;
                }
                else
                {
                    Debug.LogError($"Two {typeof(GameManager)} instances exist, didn't create new one");
                    return;
                }

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
                var playerConfig = ConfigManager.Instance?.GetConfig<PlayerConfig>();
                var cityConfig = ConfigManager.Instance?.GetConfig<CityConfig>();
                var randomEventsConfig = ConfigManager.Instance?.GetConfig<RandomEventsConfig>();

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