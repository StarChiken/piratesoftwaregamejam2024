using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Base.Core.Config
{
    /// <summary>
    /// Manages loading and caching of all configuration ScriptableObjects.
    /// </summary>
    public class ConfigManager : MonoBehaviour
    {
        #region Fields
        [SerializeField, Tooltip("Reference to the ConfigManager instance for dependency injection")]
        private static ConfigManager s_instance;
        
        private readonly Dictionary<Type, BaseConfig> m_configCache = new();
        #endregion

        #region Properties
        /// <summary>
        /// Gets the ConfigManager instance. Must be set via dependency injection.
        /// </summary>
        public static ConfigManager Instance
        {
            get
            {
                if (s_instance == null)
                {
                    Debug.LogError("ConfigManager instance is null! Ensure it's properly initialized via dependency injection.");
                }
                return s_instance;
            }
            set
            {
                if (s_instance != null && s_instance != value)
                {
                    Debug.LogWarning("ConfigManager instance is being overwritten. This may indicate a setup issue.");
                }
                s_instance = value;
            }
        }
        #endregion

        #region Unity Lifecycle
        private void Awake()
        {
            if (s_instance == null)
            {
                s_instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (s_instance != this)
            {
                Debug.LogWarning("Multiple ConfigManager instances detected. Destroying duplicate.");
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            if (s_instance == this)
            {
                s_instance = null;
            }
        }
        #endregion

        #region Public API
        /// <summary>
        /// Gets a cached config or loads it from Resources if not cached.
        /// </summary>
        /// <typeparam name="T">The type of config to get.</typeparam>
        /// <returns>The config instance, or null if loading failed.</returns>
        /// <exception cref="InvalidOperationException">Thrown when ConfigManager is not initialized.</exception>
        public T GetConfig<T>() where T : BaseConfig
        {
            if (s_instance == null)
            {
                throw new InvalidOperationException("ConfigManager is not initialized. Ensure it's properly set up.");
            }

            var type = typeof(T);
            
            if (m_configCache.TryGetValue(type, out var cachedConfig))
            {
                return cachedConfig as T;
            }

            try
            {
                var config = BaseConfig.LoadConfig<T>();
                if (config != null)
                {
                    m_configCache[type] = config;
                }
                else
                {
                    Debug.LogError($"Failed to load config: {type.Name}");
                }
                
                return config;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error loading config {type.Name}: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Gets a cached config or loads it from Resources if not cached.
        /// </summary>
        /// <typeparam name="T">The type of config to get.</typeparam>
        /// <param name="configName">The name of the config file.</param>
        /// <returns>The config instance, or null if loading failed.</returns>
        /// <exception cref="ArgumentNullException">Thrown when configName is null or empty.</exception>
        /// <exception cref="InvalidOperationException">Thrown when ConfigManager is not initialized.</exception>
        public T GetConfig<T>(string configName) where T : BaseConfig
        {
            if (string.IsNullOrEmpty(configName))
            {
                throw new ArgumentNullException(nameof(configName), "Config name cannot be null or empty.");
            }

            if (s_instance == null)
            {
                throw new InvalidOperationException("ConfigManager is not initialized. Ensure it's properly set up.");
            }

            var type = typeof(T);
            
            if (m_configCache.TryGetValue(type, out var cachedConfig))
            {
                return cachedConfig as T;
            }

            try
            {
                var config = BaseConfig.LoadConfig<T>(configName);
                if (config != null)
                {
                    m_configCache[type] = config;
                }
                else
                {
                    Debug.LogError($"Failed to load config: {configName}");
                }
                
                return config;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error loading config {configName}: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Preloads all configs to cache them.
        /// </summary>
        public void PreloadAllConfigs()
        {
            try
            {
                GetConfig<CityConfig>();
                GetConfig<PlayerConfig>();
                GetConfig<DevotionConfig>();
                GetConfig<SaveLoadManagerConfig>();
                GetConfig<RandomEventsConfig>();
                GetConfig<GameplayConfig>();
                GetConfig<BuildingConfig>();
                GetConfig<AudioConfig>();
                GetConfig<CameraConfig>();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error during config preloading: {ex.Message}");
            }
        }

        /// <summary>
        /// Clears the config cache.
        /// </summary>
        public void ClearCache()
        {
            m_configCache.Clear();
            Debug.Log("Config cache cleared.");
        }

        /// <summary>
        /// Gets all loaded configs as a list.
        /// </summary>
        /// <returns>A list of all loaded configs.</returns>
        /// <exception cref="InvalidOperationException">Thrown when ConfigManager is not initialized.</exception>
        public static List<BaseConfig> GetAllConfigs()
        {
            if (s_instance == null)
            {
                throw new InvalidOperationException("ConfigManager is not initialized. Ensure it's properly set up.");
            }

            var configs = new List<BaseConfig>();
            
            try
            {
                // Load all known config types
                var instance = Instance;
                configs.Add(instance.GetConfig<CityConfig>());
                configs.Add(instance.GetConfig<PlayerConfig>());
                configs.Add(instance.GetConfig<DevotionConfig>());
                configs.Add(instance.GetConfig<SaveLoadManagerConfig>());
                configs.Add(instance.GetConfig<RandomEventsConfig>());
                configs.Add(instance.GetConfig<GameplayConfig>());
                configs.Add(instance.GetConfig<BuildingConfig>());
                configs.Add(instance.GetConfig<AudioConfig>());
                configs.Add(instance.GetConfig<CameraConfig>());
                
                // Filter out null configs
                return configs.Where(c => c != null).ToList();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error getting all configs: {ex.Message}");
                return new List<BaseConfig>();
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Validates that the ConfigManager is properly initialized.
        /// </summary>
        /// <returns>True if properly initialized, false otherwise.</returns>
        private bool IsInitialized()
        {
            return s_instance != null;
        }
        #endregion
    }
} 