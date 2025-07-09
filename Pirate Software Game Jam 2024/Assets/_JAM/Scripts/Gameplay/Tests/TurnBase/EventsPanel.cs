using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Gameplay
{
    /// <summary>
    /// Handles UI logic for switching between event and commandment panels.
    /// </summary>
    public class EventsPanel : MonoBehaviour
    {
        [SerializeField] private GameObject panelCommandment;
        [SerializeField] private GameObject panelEvent;
        
        /// <summary>
        /// Opens the specified panel type.
        /// </summary>
        public void OpenPanel(PanelType type)
        {
            switch (type)
            {
                case PanelType.RandomEvent:
                    panelEvent.SetActive(true);
                    panelCommandment.SetActive(false);
                    break;
                case PanelType.ChooseCommandment:
                    panelEvent.SetActive(false);
                    panelCommandment.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }

    public enum PanelType
    {
        RandomEvent,
        ChooseCommandment
    }
}
