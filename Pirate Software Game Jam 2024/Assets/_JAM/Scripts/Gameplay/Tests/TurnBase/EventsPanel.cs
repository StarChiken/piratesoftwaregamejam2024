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
        
        public void OpenPanel(panelType type)
        {
            switch (type)
            {
                case panelType.RandomEvent:
                    panelEvent.SetActive(true);
                    panelCommandment.SetActive(false);
                    break;
                case panelType.ChooseCommandment:
                    panelEvent.SetActive(false);
                    panelCommandment.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }

    public enum panelType
    {
        RandomEvent,
        ChooseCommandment
    }
}
