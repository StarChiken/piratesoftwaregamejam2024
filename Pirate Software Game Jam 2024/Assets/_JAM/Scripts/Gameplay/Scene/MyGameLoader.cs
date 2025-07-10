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
    [SerializeField] private TMP_Text m_TMPText;
    [SerializeField] private string m_sceneName;
    [SerializeField] private Slider m_sliderFillImage;
    [SerializeField] private float m_fillAmountDuration = 0.1f;
    [SerializeField] private bool m_isLoading = false;
    [SerializeField] private int m_DOValue = 10;
    [SerializeField] private Image m_fade;
    [SerializeField] private GameObject m_JamLogo;
    [SerializeField] private GameObject m_GameLogo;
    [SerializeField] private GameObject m_Menu;
    [SerializeField] private AudioClip m_Intro;
    [SerializeField] private AudioClip m_OpeningLoop;
    [SerializeField] private AudioClip m_GameplayLoop;
    [SerializeField] private AudioClip m_LoseSound;
    [SerializeField] private AudioClip m_WinSound;
    [SerializeField] private float m_ShortDuration = 0.5f;
    [SerializeField] private float m_LongDuration = 1.5f;
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
        m_audio = GameObject.Find("AudioManager").GetComponent<AudioComponent>();
        m_menuAnim = m_Menu.GetComponent<Animator>();
        m_gameLogoAnim = m_GameLogo.GetComponent<Animator>();
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
        m_JamLogo.GetComponent<Image>().DOFade(1.0f, m_ShortDuration);
        yield return new WaitForSeconds(1f);
        m_JamLogo.GetComponent<Image>().DOFade(0.0f, m_ShortDuration).OnComplete(TweenGameLogo);
    }
    
    private void TweenGameLogo()
    {
        m_GameLogo.GetComponent<Image>().DOFade(1.0f, m_LongDuration).OnComplete(CallAnim);
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