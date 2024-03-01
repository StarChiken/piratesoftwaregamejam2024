using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Gameplay
{
    public class EventsPanel : MonoBehaviour
    {
        public GameObject panelCommandment;
        public GameObject panelEvent;
        public GameObject scrim;

        public void OpenPanel(PanelType type)
        {
            scrim.SetActive(true);

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
