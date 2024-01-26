using System;
using System.Collections;
using Base.Core.Components;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

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
        private Animator menuAnim;
        private Animator gameLogoAnim;
        public float ShortDuration = 0.5f;
        public float LongDuration = 1.5f;
        
        // Music stuff
        private AudioComponent audio;
        public AudioClip Intro;
        public AudioClip OpeningLoop;
        public AudioClip GameplayLoop;
        public AudioClip LoseSound;
        public AudioClip WinSound;
        private bool doOnce = true;

        public void QuitButton()
        {
            Application.Quit();
        }
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
            SceneManager.sceneLoaded += FadeOut;
            
            audio = GameObject.Find("AudioManager").GetComponent<AudioComponent>();
            menuAnim = Menu.GetComponent<Animator>();
            gameLogoAnim = GameLogo.GetComponent<Animator>();
        }
        
        private void FadeOut(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == sceneName)
            {
                fade.DOFade(0, 1f);
            }
        }
        
        private void Update()
        {
            if (isLoading)
            {
                DoLoadBar();
            }
            
            GamePlaySound();
        }

        private void GamePlaySound()
        {
            if (SceneManager.GetActiveScene().name == sceneName && doOnce)
            {
                audio.MusicAudioSource.loop = true;
                audio.MusicAudioSource.volume = 0;
                audio.PlayBackgroundSound(GameplayLoop);
                audio.MusicAudioSource.DOFade(1, 5f);
                doOnce = false;
            }
        }

        public void StartButton()
        {
            audio.MusicAudioSource.DOFade(0, 0.5f).OnComplete(() =>
            {
                fade.DOFade(1, 1f);
                SceneManager.LoadScene(sceneName);
            });
            
        }

        
        void Start () 
        {
            StartCoroutine(TweenJamLogo());
        }

        private IEnumerator TweenJamLogo()
        {
            yield return new WaitForSeconds(1f);
            audio.PlayBackgroundSound(Intro);
            JamLogo.GetComponent<Image>().DOFade(1.0f, ShortDuration);
            yield return new WaitForSeconds(1f);
            JamLogo.GetComponent<Image>().DOFade(0.0f, ShortDuration).OnComplete(TweenGameLogo);
        }
        
        private void TweenGameLogo()
        {
            GameLogo.GetComponent<Image>().DOFade(1.0f, LongDuration).OnComplete(CallAnim);
        }

        private void CallAnim()
        {
            menuAnim.SetTrigger("Menu");
            gameLogoAnim.SetTrigger("Logo");
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