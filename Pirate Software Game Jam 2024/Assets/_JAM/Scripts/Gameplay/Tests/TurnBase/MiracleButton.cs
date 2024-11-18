using System;
using System.Collections.Generic;
using Base.Core.Components;
using Base.Core.Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Base.Gameplay
{
    public class MiracleButton : MyMonoBehaviour, IPointerEnterHandler
    {
        public Button button;
        public MiracleType MiracleType;
        public SectorScript sector;
        
        // data
        public List<Citizen> sectorPop = new ();
        private Gameplay gameplayManager;
        
        // Sound
        //private AudioComponent audioSource;
        [SerializeField] private AudioClip idleSound;
        [SerializeField] private AudioClip castSound;
        [SerializeField] private List<AudioClip> audioClips; // 0 is idle, 1 is cast

        private void Awake()
        {
            gameplayManager = GameObject.Find("Gameplay").GetComponent<Gameplay>();
//            audioSource = GameObject.Find("AudioManager").GetComponent<AudioComponent>();
        }

        public void DoMiracle()
        {
            sectorPop = sector.sector.SectorPopulace;
            gameplayManager.DoMiracleOnCitizens(MiracleType, sectorPop);
            //audioSource.PlayFXSound(castSound);
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            // if(idleSound == null) return;
            // audioSource.PlayFXSound(idleSound);
        }
    }
}