using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Base.Gameplay
{
    public class SectorButton : MonoBehaviour
    {
        // public SectorItem sectorItem;
        //
        // private void OnMouseDown()
        // {
        //     Debug.Log("Inside OnMouseDown");
        // }
        
        
        [SerializeField] public SectorScript Sector;

        private void OnMouseEnter()
        {
            if (GetComponent<MeshRenderer>().enabled)
            {
                return;
            }
            GetComponent<MeshRenderer>().enabled = true;
        }

        private void OnMouseExit()
        {
            GetComponent<MeshRenderer>().enabled = false;
        }

        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject() && Sector.gameObject.activeSelf)
            {
                return;
            }
            else
            {
                Sector.gameObject.SetActive(true);
            }
           
            
            // switch (Sector.gameObject.activeSelf)
            // {
            //     case true:
            //         Debug.Log("Inside case true");
            //         Sector.gameObject.SetActive(false);
            //         //Sector.DoDistrictWindow(false);
            //         break;
            //     case false:
            //         Debug.Log("Inside case false");
            //         Sector.gameObject.SetActive(true);
            //         //Sector.DoDistrictWindow(true);
            //         break;
            // }
        }
    }
}
