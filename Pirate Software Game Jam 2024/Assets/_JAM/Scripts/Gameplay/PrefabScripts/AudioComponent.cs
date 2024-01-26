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
            
            //PlayBackgroundSound(BackgroundMusic);
        }

        public void PlayBackgroundSound(AudioClip clip)
        {
            MusicAudioSource.clip = clip;
            MusicAudioSource.Play();
        }
        
        public void PlayFXSound(AudioClip clip)
        {
            MusicAudioSource.clip = clip;
            SFXAudioSource.Play();
        }
    }
}