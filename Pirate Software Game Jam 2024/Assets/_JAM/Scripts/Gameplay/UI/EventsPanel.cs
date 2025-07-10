using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// Handles UI logic for switching between event and commandment panels.
    /// </summary>
    public class EventsPanel : MonoBehaviour
    {
        [SerializeField, Tooltip("Panel for commandment selection")] private GameObject m_panelCommandment;
        [SerializeField, Tooltip("Panel for random events")] private GameObject m_panelEvent;
        
        /// <summary>
        /// Opens the specified panel type.
        /// </summary>
        public void OpenPanel(PanelType type)
        {
            switch (type)
            {
                case PanelType.RandomEvent:
                    m_panelEvent.SetActive(true);
                    m_panelCommandment.SetActive(false);
                    break;
                case PanelType.ChooseCommandment:
                    m_panelEvent.SetActive(false);
                    m_panelCommandment.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
