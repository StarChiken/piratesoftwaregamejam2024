using System.Collections;
using Base.Core.Components;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace Base.Gameplay
{
    public class MyGameLoader : MyMonoBehaviour
    {
        [SerializeField] private TMP_Text TMPText;
        [SerializeField] private string sceneName;
        
        // loader bar stuff
        [SerializeField] private Slider sliderFillImage;
        [SerializeField] float fillAmountDuration = 0.1f;
        [SerializeField] private bool isLoading = false;
        [SerializeField] private int DOValue = 10;
        
        // Transition stuff
        [SerializeField] private Animator crossFade;
        
        // Music stuff
        private AudioComponent audio;
        public AudioClip Intro;
        public AudioClip OpeningLoop;
        public AudioClip GameplayLoop;
        public AudioClip LoseSound;
        public AudioClip WinSound;
        private void Awake()
        {
            DontDestroyOnLoad(this);

            audio = GameObject.Find("AudioManager").GetComponent<AudioComponent>();
        }
        
        private void Update()
        {
            if (isLoading)
            {
                DoLoadBar();
            }
        }

        public void DoIntro()
        {
            StartCoroutine(LoadScene("Intro"));
        }
        
        private IEnumerator LoadScene(string scene)
        {
            crossFade.SetTrigger("TriggerName");
            
            switch (scene)
            {
                case "Intro":
                    audio.PlayBackgroundSound(Intro);
                    // game logo come in
                    audio.PlayBackgroundSound(OpeningLoop);
                    // manu coming in with tween
                    break;
                case "Start":
                    // OpeningLoop still looping
                    // wait for player choosing god
                    break;
                case "Gameplay":
                    audio.PlayBackgroundSound(GameplayLoop);
                    break;
                case "Credits":
                    audio.PlayBackgroundSound(OpeningLoop);
                    // do particles
                    break;
            }
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(scene);
        }
        
        private void DoLoadBar()
        {
            sliderFillImage.DOValue(DOValue, fillAmountDuration).SetEase(Ease.InBounce);

            TMPText.text = sceneName == null
                ? "Loading . . ."
                : $"Loading {sceneName}";
        }
    }
}