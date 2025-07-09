using UnityEngine;

namespace Base.Core.Config
{
    /// <summary>
    /// Base class for all configuration ScriptableObjects that can be loaded from Resources.
    /// </summary>
    public abstract class BaseConfig : ScriptableObject
    {
        #region Properties
        /// <summary>
        /// The name of the config file in the Resources folder.
        /// </summary>
        protected abstract string ConfigFileName { get; }
        #endregion

        #region Public API
        /// <summary>
        /// Loads the config from the Resources folder.
        /// </summary>
        /// <typeparam name="T">The type of config to load.</typeparam>
        /// <returns>The loaded config, or null if not found.</returns>
        /// <exception cref="System.ArgumentException">Thrown when T is not a valid config type.</exception>
        public static T LoadConfig<T>() where T : BaseConfig
        {
            try
            {
                var config = Resources.Load<T>(typeof(T).Name);
                if (config == null)
                {
                    Debug.LogError($"Failed to load config: {typeof(T).Name}. Ensure the config file exists in the Resources folder.");
                }
                return config;
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error loading config {typeof(T).Name}: {ex.Message}");
                return null;
            }
        }
        
        /// <summary>
        /// Loads the config from the Resources folder with a specific name.
        /// </summary>
        /// <typeparam name="T">The type of config to load.</typeparam>
        /// <param name="configName">The name of the config file.</param>
        /// <returns>The loaded config, or null if not found.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when configName is null or empty.</exception>
        /// <exception cref="System.ArgumentException">Thrown when T is not a valid config type.</exception>
        public static T LoadConfig<T>(string configName) where T : BaseConfig
        {
            if (string.IsNullOrEmpty(configName))
            {
                throw new System.ArgumentNullException(nameof(configName), "Config name cannot be null or empty.");
            }

            try
            {
                var config = Resources.Load<T>(configName);
                if (config == null)
                {
                    Debug.LogError($"Failed to load config: {configName}. Ensure the config file exists in the Resources folder.");
                }
                return config;
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error loading config {configName}: {ex.Message}");
                return null;
            }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Validates that the config is properly initialized.
        /// </summary>
        /// <returns>True if the config is valid, false otherwise.</returns>
        protected virtual bool ValidateConfig()
        {
            return !string.IsNullOrEmpty(ConfigFileName);
        }
        #endregion
    }
} 