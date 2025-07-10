using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

/// <summary>
/// Handles game loading, transitions, and music for the main menu and gameplay scenes.
/// </summary>
public class MyGameLoader : MyMonoBehaviour
{
    [SerializeField] private TMP_Text TMPText;
    [SerializeField] private string sceneName;
    [SerializeField] private Slider sliderFillImage;
    [SerializeField] private float fillAmountDuration = 0.1f;
    [SerializeField] private bool isLoading = false;
    [SerializeField] private int DOValue = 10;
    [SerializeField] private Image fade;
    [SerializeField] private GameObject JamLogo;
    [SerializeField] private GameObject GameLogo;
    [SerializeField] private GameObject Menu;
    [SerializeField] private AudioClip Intro;
    [SerializeField] private AudioClip OpeningLoop;
    [SerializeField] private AudioClip GameplayLoop;
    [SerializeField] private AudioClip LoseSound;
    [SerializeField] private AudioClip WinSound;
    [SerializeField] private float ShortDuration = 0.5f;
    [SerializeField] private float LongDuration = 1.5f;
    private Animator menuAnim;
    private Animator gameLogoAnim;
    private AudioComponent audio;
    private bool doOnce = true;

    /// <summary>
    /// Quits the application.
    /// </summary>
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

    /// <summary>
    /// Starts the game and loads the gameplay scene.
    /// </summary>
    public void StartButton()
    {
        audio.MusicAudioSource.DOFade(0, 0.5f).OnComplete(() =>
        {
            fade.DOFade(1, 1f);
            SceneManager.LoadScene(sceneName);
        });
    }

    private void Start()
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

    private void DoLoadBar()
    {
        sliderFillImage.DOValue(DOValue, fillAmountDuration).SetEase(Ease.InBounce);
        TMPText.text = sceneName == null ? "Loading . . ." : $"Loading {sceneName}";
    }
}