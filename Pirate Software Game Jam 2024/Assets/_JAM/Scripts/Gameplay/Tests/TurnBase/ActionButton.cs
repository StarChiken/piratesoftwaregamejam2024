using Base.Core.Components;
using Base.Core.Managers;
using UnityEngine;

namespace Base.Gameplay
{
    /// <summary>
    /// Handles UI button for triggering a faction action on a district.
    /// </summary>
    public class ActionButton : MyMonoBehaviour
    {
        [SerializeField] private FactionAction factionActions;
        [SerializeField] private DistrictScript district;
        [SerializeField] private Gameplay gameplayManager;

        private void Awake()
        {
            // Allow assignment via inspector, fallback to Find for legacy scenes
            if (gameplayManager == null)
                gameplayManager = GameObject.Find("Gameplay").GetComponent<Gameplay>();
        }

        /// <summary>
        /// Triggers the faction action on the district's faction.
        /// </summary>
        public void DoAction()
        {
            gameplayManager.DoFactionAction(factionActions, district.district.DistrictFaction);
        }
    }
}