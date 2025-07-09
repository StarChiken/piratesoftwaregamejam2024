using System;
using Base.Core.Components;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

namespace Base.Gameplay
{
    public class AudioComponent : MyMonoBehaviour
    {
        [SerializeField] public AudioSource MusicAudioSource;
        [SerializeField] public AudioSource SFXAudioSource;
        
        public AudioClip BackgroundMusic;
        public AudioClip UIClick;
        public AudioClip LoseSound;
        public AudioClip WinSound;

        public string sceneName;
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void PlayBackgroundSound(AudioClip clip)
        {
            MusicAudioSource.Stop();
            MusicAudioSource.clip = clip;
            MusicAudioSource.Play();
        }
        
        public void PlayFXSound(AudioClip clip)
        {
            MusicAudioSource.clip = clip;
            SFXAudioSource.Play();
        }
        
        private void StopMusic()
        {
            MusicAudioSource.Stop();
        }
    }
}