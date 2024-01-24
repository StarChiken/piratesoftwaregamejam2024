using Base.Core.Components;
using UnityEngine;

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
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
            PlayBackgroundMusic(BackgroundMusic, MusicAudioSource);
        }

        private void PlayBackgroundMusic(AudioClip clip, AudioSource musicSource)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
    }
}