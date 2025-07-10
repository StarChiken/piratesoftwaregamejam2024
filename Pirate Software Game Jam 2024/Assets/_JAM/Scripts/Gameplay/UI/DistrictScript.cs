using TMPro;
using UnityEngine;

    /// <summary>
    /// Handles UI logic for displaying a district's name and data.
    /// </summary>
    public class DistrictScript : MyMonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_name;
        public District district;
        public TextMeshProUGUI nameText;
    }