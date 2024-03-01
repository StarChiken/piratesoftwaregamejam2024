using System.Collections.Generic;
using Base.Core.Components;
using Base.Core.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Base.Gameplay
{
    public class ActionButton : MyMonoBehaviour
    {
        public Button button;

        public FactionAction factionActions;
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
        public void DoAction()
        {
            gameplayManager.DoFactionAction(factionActions, sector.sector.SectorFaction);
            audioSource.PlayFXSound(castSound);

        }
    }
}