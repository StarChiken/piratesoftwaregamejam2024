using System.Collections.Generic;
using Base.Core.Components;
using Base.Core.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Base.Gameplay
{
    public class ButtonBase : MyMonoBehaviour
    {
        // data
        public List<Citizen> sectorPop = new ();
        
        public GameObject panel;
        public GameObject[] otherPanels;
        private bool panelState;
        private float startTime;
        public Gameplay gameplayManager;
        private void Awake()
        {
            gameplayManager = GameObject.Find("Gameplay").GetComponent<Gameplay>();
            OpenClosePanel(false);
        }

        public void OpenClosePanel(bool isActive)
        {
            panel.SetActive(isActive);

            foreach (var obj in otherPanels)
            {
                if (obj.activeSelf)
                {
                    obj.SetActive(!isActive);
                }
            }

            panelState = isActive;
            // Reset open count and start timer when opening the panel
            if (isActive)
            {
                startTime = Time.time;
            }
        }

        private void Update()
        {
            // if (panelState)
            // {
            //     // Close the panel after 30 seconds
            //     if (Time.time - startTime > 10)
            //     {
            //         OpenClosePanel(false);
            //     }
            // }
        }
    }
}