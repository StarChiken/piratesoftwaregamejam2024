using UnityEngine;

namespace Base.Core.Config
{
    /// <summary>
    /// Base class for all configuration ScriptableObjects that can be loaded from Resources.
    /// </summary>
    public abstract class BaseConfig : ScriptableObject
    {
        /// <summary>
        /// The name of the config file in the Resources folder.
        /// </summary>
        protected abstract string ConfigFileName { get; }
        
        /// <summary>
        /// Loads the config from the Resources folder.
        /// </summary>
        /// <typeparam name="T">The type of config to load.</typeparam>
        /// <returns>The loaded config, or null if not found.</returns>
        public static T LoadConfig<T>() where T : BaseConfig
        {
            var config = Resources.Load<T>(typeof(T).Name);
            if (config == null)
            {
                Debug.LogError($"Failed to load config: {typeof(T).Name}");
            }
            return config;
        }
        
        /// <summary>
        /// Loads the config from the Resources folder with a specific name.
        /// </summary>
        /// <typeparam name="T">The type of config to load.</typeparam>
        /// <param name="configName">The name of the config file.</param>
        /// <returns>The loaded config, or null if not found.</returns>
        public static T LoadConfig<T>(string configName) where T : BaseConfig
        {
            var config = Resources.Load<T>(configName);
            if (config == null)
            {
                Debug.LogError($"Failed to load config: {configName}");
            }
            return config;
        }
    }
} 