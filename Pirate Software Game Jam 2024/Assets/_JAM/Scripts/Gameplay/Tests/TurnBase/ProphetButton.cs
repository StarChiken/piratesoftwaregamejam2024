using System.Collections.Generic;
using Base.Core.Components;
using Base.Core.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Base.Gameplay
{
    public class ProphetButton : MyMonoBehaviour
    {
        public Button button;

        public ProphetAction prophetActions;
        public SectorScript sector;
        private Gameplay gameplayManager;
        
        // Sound
        private AudioComponent audioSource;
        [SerializeField] private AudioClip idleSound;
        [SerializeField] private AudioClip castSound;
        [SerializeField] private List<AudioClip> audioClips; // 0 is idle, 1 is cast
        private void Awake()
        {
            gameplayManager = GameObject.Find("Gameplay").GetComponent<Gameplay>();
            audioSource = GameObject.Find("AudioManager").GetComponent<AudioComponent>();
        }
        
        public void DoPray()
        {
            gameplayManager.DoProphetAction(prophetActions, gameplayManager.player.FollowerCount);
            audioSource.PlayFXSound(castSound);

        }
        
        public void DoPreach()
        {
            gameplayManager.DoProphetAction(prophetActions, sector.sector.SectorPopulace);
            audioSource.PlayFXSound(castSound);

        }
        
        public void DoInfiltrate()
        {
            gameplayManager.DoProphetAction(prophetActions, sector.sector.SectorFaction);
            audioSource.PlayFXSound(castSound);

        }
    }
}