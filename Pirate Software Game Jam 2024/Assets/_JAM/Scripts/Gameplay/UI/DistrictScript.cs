using UnityEngine;

namespace Base.Gameplay
{
    /// <summary>
    /// Handles UI logic for displaying a district's name and data.
    /// </summary>
    public class DistrictScript : MyMonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI name;
        public District district;
        public TextMeshProUGUI nameText;
    }
}