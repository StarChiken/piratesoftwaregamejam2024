using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Configuration data for audio clips, VFX, and sound settings.
/// </summary>
[CreateAssetMenu(fileName = "AudioConfig", menuName = "Game/Config/Audio Config")]
public class AudioConfig : BaseConfig
{
    [Header("Music Clips")]
    [SerializeField] private AudioClip introMusic;
    [SerializeField] private AudioClip openingLoop;
    [SerializeField] private AudioClip gameplayLoop;
    [SerializeField] private AudioClip loseSound;
    [SerializeField] private AudioClip winSound;
    
    [Header("Sound Effects")]
    [SerializeField] private AudioClip idleSound;
    [SerializeField] private AudioClip castSound;
    [SerializeField] private List<AudioClip> audioClips = new(); // 0 is idle, 1 is cast
    
    [Header("VFX Prefabs")]
    [SerializeField] private GameObject vfxPrefab;
    [SerializeField] private float tempOverlapRadius = 10f;
    
    [Header("Audio Settings")]
    [SerializeField] private float shortDuration = 0.5f;
    [SerializeField] private float longDuration = 1.5f;

    // Public properties for backward compatibility
    public AudioClip IntroMusic => introMusic;
    public AudioClip OpeningLoop => openingLoop;
    public AudioClip GameplayLoop => gameplayLoop;
    public AudioClip LoseSound => loseSound;
    public AudioClip WinSound => winSound;
    public AudioClip IdleSound => idleSound;
    public AudioClip CastSound => castSound;
    public List<AudioClip> AudioClips => audioClips;
    public GameObject VfxPrefab => vfxPrefab;
    public float TempOverlapRadius => tempOverlapRadius;
    public float ShortDuration => shortDuration;
    public float LongDuration => longDuration;

    protected override string ConfigFileName => "AudioConfig";
} 