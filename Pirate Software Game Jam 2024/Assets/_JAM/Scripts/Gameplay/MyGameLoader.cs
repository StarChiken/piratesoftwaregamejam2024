using System;
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
        [SerializeField] private Image fade;
        [SerializeField] private GameObject JamLogo;
        [SerializeField] private GameObject GameLogo;
        [SerializeField] private GameObject Menu;

        public float FadeInDuration = 0.5f;
        public float FadeOutDuration = 1.5f;
        
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

        void Start () 
        {
            StartCoroutine(TweenJamLogo());
        }

        private IEnumerator TweenJamLogo()
        {
            JamLogo.GetComponent<Image>().DOFade(1.0f, FadeInDuration);
            yield return new WaitForSeconds(3f);
            JamLogo.GetComponent<Image>().DOFade(0.0f, FadeOutDuration).OnComplete(TweenComplete);
        }
        
        private void TweenComplete()
        {
            StartCoroutine(TweenGameLogo());
        }
        
        private IEnumerator TweenGameLogo()
        {
            yield return new WaitForSeconds(0.1f);
            GameLogo.GetComponent<Image>().DOFade(1.0f, FadeInDuration).OnComplete(TweenMenu);
           
            //GameLogo.GetComponent<Image>().DOFade(0.0f, FadeOutDuration).OnComplete(TweenMenu);
        }

        private void TweenMenu()
        {
            Menu.GetComponent<Animator>().SetTrigger("Menu");
            GameLogo.GetComponent<Animator>().SetTrigger("Move");
        }
        
        

       
        
        private void Update()
        {
            if (isLoading)
            {
                DoLoadBar();
            }
        }
        
        // private IEnumerator LoadScene(string scene)
        // {
        //     crossFade.SetTrigger("TriggerName");
        //     
        //     switch (scene)
        //     {
        //         case "Intro":
        //             audio.PlayBackgroundSound(Intro);
        //             // game logo come in
        //             audio.PlayBackgroundSound(OpeningLoop);
        //             // manu coming in with tween
        //             break;
        //         case "Start":
        //             // OpeningLoop still looping
        //             // wait for player choosing god
        //             break;
        //         case "Gameplay":
        //             audio.PlayBackgroundSound(GameplayLoop);
        //             break;
        //         case "Credits":
        //             audio.PlayBackgroundSound(OpeningLoop);
        //             // do particles
        //             break;
        //     }
        //     yield return new WaitForSeconds(1f);
        //     SceneManager.LoadScene(scene);
        // }
        
        private void DoLoadBar()
        {
            sliderFillImage.DOValue(DOValue, fillAmountDuration).SetEase(Ease.InBounce);

            TMPText.text = sceneName == null
                ? "Loading . . ."
                : $"Loading {sceneName}";
        }
    }
}