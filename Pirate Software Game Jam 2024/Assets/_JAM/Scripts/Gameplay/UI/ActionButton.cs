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

        /// <summary>
        /// Event triggered when the action button is pressed.
        /// </summary>
        public event System.Action<FactionAction, Faction> OnActionTriggered;

        /// <summary>
        /// Triggers the faction action on the district's faction.
        /// </summary>
        public void DoAction()
        {
            OnActionTriggered?.Invoke(factionActions, district.district.DistrictFaction);
        }
    }
}