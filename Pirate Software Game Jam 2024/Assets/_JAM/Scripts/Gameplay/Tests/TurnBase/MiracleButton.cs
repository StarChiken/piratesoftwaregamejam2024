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
        [SerializeField] private Gameplay gameplayManager;

        private void Awake()
        {
            // Allow assignment via inspector, fallback to Find for legacy scenes
            if (gameplayManager == null)
                gameplayManager = GameObject.Find("Gameplay").GetComponent<Gameplay>();
        }

        /// <summary>
        /// Triggers the miracle on the district's citizens.
        /// </summary>
        public void DoMiracle()
        {
            districtPop = district.district.DistrictPopulace;
            gameplayManager.DoMiracleOnCitizens(miracleType, districtPop);
        }
    }
}