using UnityEngine;

namespace Base.Core.Config
{
    /// <summary>
    /// Initializes the configuration system and ensures all configs are loaded.
    /// </summary>
    public class ConfigInitializer : MonoBehaviour
    {
        [Header("Auto Initialization")]
        [SerializeField] private bool autoInitializeOnStart = true;
        [SerializeField] private bool preloadAllConfigs = true;

        private void Start()
        {
            if (autoInitializeOnStart)
            {
                InitializeConfigSystem();
            }
        }

        /// <summary>
        /// Initializes the configuration system.
        /// </summary>
        public void InitializeConfigSystem()
        {
            // Ensure ConfigManager instance exists
            var configManager = ConfigManager.Instance;
            
            if (preloadAllConfigs)
            {
                configManager.PreloadAllConfigs();
                Debug.Log("Configuration system initialized and all configs preloaded.");
            }
            else
            {
                Debug.Log("Configuration system initialized.");
            }
        }

        /// <summary>
        /// Manually preload all configs.
        /// </summary>
        [ContextMenu("Preload All Configs")]
        public void PreloadAllConfigs()
        {
            ConfigManager.Instance.PreloadAllConfigs();
            Debug.Log("All configs preloaded manually.");
        }

        /// <summary>
        /// Clear the config cache.
        /// </summary>
        [ContextMenu("Clear Config Cache")]
        public void ClearConfigCache()
        {
            ConfigManager.Instance.ClearCache();
            Debug.Log("Config cache cleared.");
        }
    }
} 