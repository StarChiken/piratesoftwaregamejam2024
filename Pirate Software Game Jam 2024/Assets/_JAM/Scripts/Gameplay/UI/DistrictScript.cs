using TMPro;
using UnityEngine;

    /// <summary>
    /// Handles UI logic for displaying a district's name and data.
    /// </summary>
    public class DistrictScript : MyMonoBehaviour
    {
      
        #region Fields
        [SerializeField, Tooltip("Text component for district name display")]
        private TextMeshProUGUI m_nameText;
        /// <summary>
        /// The district data for this UI element.
        /// </summary>
        public District district;
        private ConfigManager m_configManager;
        #endregion

        #region Unity Methods
        // Unity methods here (if any)
        #endregion

        #region Public Methods
        // Public methods here (if any)
        public static void SetConfigManager(ConfigManager configManager)
        {
            m_configManager = configManager;
        }
        #endregion
    }