using System;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Core.Config
{
    /// <summary>
    /// Manages loading and caching of all configuration ScriptableObjects.
    /// </summary>
    public class ConfigManager : MonoBehaviour
    {
        private static ConfigManager _instance;
        public static ConfigManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("ConfigManager");
                    _instance = go.AddComponent<ConfigManager>();
                    DontDestroyOnLoad(go);
                }
                return _instance;
            }
        }

        private readonly Dictionary<Type, BaseConfig> _configCache = new();

        /// <summary>
        /// Gets a cached config or loads it from Resources if not cached.
        /// </summary>
        /// <typeparam name="T">The type of config to get.</typeparam>
        /// <returns>The config instance.</returns>
        public T GetConfig<T>() where T : BaseConfig
        {
            var type = typeof(T);
            
            if (_configCache.TryGetValue(type, out var cachedConfig))
            {
                return cachedConfig as T;
            }

            var config = BaseConfig.LoadConfig<T>();
            if (config != null)
            {
                _configCache[type] = config;
            }
            
            return config;
        }

        /// <summary>
        /// Gets a cached config or loads it from Resources if not cached.
        /// </summary>
        /// <typeparam name="T">The type of config to get.</typeparam>
        /// <param name="configName">The name of the config file.</param>
        /// <returns>The config instance.</returns>
        public T GetConfig<T>(string configName) where T : BaseConfig
        {
            var type = typeof(T);
            
            if (_configCache.TryGetValue(type, out var cachedConfig))
            {
                return cachedConfig as T;
            }

            var config = BaseConfig.LoadConfig<T>(configName);
            if (config != null)
            {
                _configCache[type] = config;
            }
            
            return config;
        }

        /// <summary>
        /// Preloads all configs to cache them.
        /// </summary>
        public void PreloadAllConfigs()
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

        /// <summary>
        /// Clears the config cache.
        /// </summary>
        public void ClearCache()
        {
            _configCache.Clear();
        }
    }
} 