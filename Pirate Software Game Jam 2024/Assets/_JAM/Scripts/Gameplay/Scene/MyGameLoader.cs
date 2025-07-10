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
    [SerializeField, Tooltip("Text component for loading display")] private TMP_Text m_TMPText;
    [SerializeField, Tooltip("Name of the scene to load")] private string m_sceneName;
    [SerializeField, Tooltip("Slider for loading bar fill")] private Slider m_sliderFillImage;
    [SerializeField, Tooltip("Duration for fill amount animation")] private float m_fillAmountDuration = 0.1f;
    [SerializeField, Tooltip("Whether the game is currently loading")] private bool m_isLoading = false;
    [SerializeField, Tooltip("Value to set for loading bar animation")] private int m_DOValue = 10;
    [SerializeField, Tooltip("Image for fade transitions")] private Image m_fade;
    [SerializeField, Tooltip("Jam logo GameObject")] private GameObject m_JamLogo;
    [SerializeField, Tooltip("Game logo GameObject")] private GameObject m_GameLogo;
    [SerializeField, Tooltip("Menu GameObject")] private GameObject m_Menu;
    [SerializeField, Tooltip("Intro audio clip")] private AudioClip m_Intro;
    [SerializeField, Tooltip("Opening loop audio clip")] private AudioClip m_OpeningLoop;
    [SerializeField, Tooltip("Gameplay loop audio clip")] private AudioClip m_GameplayLoop;
    [SerializeField, Tooltip("Lose sound audio clip")] private AudioClip m_LoseSound;
    [SerializeField, Tooltip("Win sound audio clip")] private AudioClip m_WinSound;
    [SerializeField, Tooltip("Short duration for animations")] private float m_ShortDuration = 0.5f;
    [SerializeField, Tooltip("Long duration for animations")] private float m_LongDuration = 1.5f;
    private Animator m_menuAnim;
    private Animator m_gameLogoAnim;
    private AudioComponent m_audio;
    private bool m_doOnce = true;

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
        var audioManagerObj = GameObject.Find("AudioManager");
        if (audioManagerObj != null && audioManagerObj.TryGetComponent<AudioComponent>(out var audioComponent))
            m_audio = audioComponent;
        else
            Debug.LogError("AudioManager or AudioComponent missing!");
        if (m_Menu != null && m_Menu.TryGetComponent<Animator>(out var menuAnim))
            m_menuAnim = menuAnim;
        else
            Debug.LogError("Menu GameObject or Animator missing!");
        if (m_GameLogo != null && m_GameLogo.TryGetComponent<Animator>(out var gameLogoAnim))
            m_gameLogoAnim = gameLogoAnim;
        else
            Debug.LogError("GameLogo GameObject or Animator missing!");
    }
    
    private void FadeOut(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == m_sceneName)
        {
            m_fade.DOFade(0, 1f);
        }
    }
    
    private void Update()
    {
        if (m_isLoading)
        {
            DoLoadBar();
        }
        GamePlaySound();
    }

    private void GamePlaySound()
    {
        if (SceneManager.GetActiveScene().name == m_sceneName && m_doOnce)
        {
            m_audio.MusicAudioSource.loop = true;
            m_audio.MusicAudioSource.volume = 0;
            m_audio.PlayBackgroundSound(m_GameplayLoop);
            m_audio.MusicAudioSource.DOFade(1, 5f);
            m_doOnce = false;
        }
    }

    /// <summary>
    /// Starts the game and loads the gameplay scene.
    /// </summary>
    public void StartButton()
    {
        m_audio.MusicAudioSource.DOFade(0, 0.5f).OnComplete(() =>
        {
            m_fade.DOFade(1, 1f);
            SceneManager.LoadScene(m_sceneName);
        });
    }

    private void Start()
    {
        StartCoroutine(TweenJamLogo());
    }

    private IEnumerator TweenJamLogo()
    {
        yield return new WaitForSeconds(1f);
        m_audio.PlayBackgroundSound(m_Intro);
        if (m_JamLogo != null && m_JamLogo.TryGetComponent<Image>(out var jamLogoImage))
            jamLogoImage.DOFade(1.0f, m_ShortDuration);
        else
            Debug.LogError("JamLogo GameObject or Image missing!");
        yield return new WaitForSeconds(1f);
        if (m_JamLogo != null && m_JamLogo.TryGetComponent<Image>(out var jamLogoImage2))
            jamLogoImage2.DOFade(0.0f, m_ShortDuration).OnComplete(TweenGameLogo);
        else
            Debug.LogError("JamLogo GameObject or Image missing!");
    }
    
    private void TweenGameLogo()
    {
        if (m_GameLogo != null && m_GameLogo.TryGetComponent<Image>(out var gameLogoImage))
            gameLogoImage.DOFade(1.0f, m_LongDuration).OnComplete(CallAnim);
        else
            Debug.LogError("GameLogo GameObject or Image missing!");
    }

    private void CallAnim()
    {
        m_menuAnim.SetTrigger("Menu");
        m_gameLogoAnim.SetTrigger("Logo");
    }

    private void DoLoadBar()
    {
        m_sliderFillImage.DOValue(m_DOValue, m_fillAmountDuration).SetEase(Ease.InBounce);
        m_TMPText.text = m_sceneName == null ? "Loading . . ." : $"Loading {m_sceneName}";
    }
}