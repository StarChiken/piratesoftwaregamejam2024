using System.Collections.Generic;
using Base.Core.Components;
using Base.Core.Managers;
using UnityEngine;

namespace Base.Gameplay
{
    /// <summary>
    /// Handles UI button for triggering a miracle on a district's citizens.
    /// </summary>
    public class MiracleButton : MyMonoBehaviour
    {
        [SerializeField] private MiracleType miracleType;
        [SerializeField] private DistrictScript district;
        private List<Citizen> districtPop = new();

        /// <summary>
        /// Event triggered when the miracle button is pressed.
        /// </summary>
        public event System.Action<MiracleType, List<Citizen>> OnMiracleTriggered;

        /// <summary>
        /// Triggers the miracle on the district's citizens.
        /// </summary>
        public void DoMiracle()
        {
            districtPop = district.district.DistrictPopulace;
            OnMiracleTriggered?.Invoke(miracleType, districtPop);
        }
    }
}