using System;
using System.Collections.Generic;
using UnityEngine;
using Base.Core.Managers;

namespace Base.Core.Config
{
    /// <summary>
    /// Configuration data for initializing SaveLoadManager and default settings.
    /// </summary>
    [CreateAssetMenu(fileName = "SaveLoadManagerConfig", menuName = "Game/Config/Save Load Manager Config")]
    public class SaveLoadManagerConfig : BaseConfig
    {
        [Header("Default Settings")]
        [SerializeField] private Dictionary<SettingsType, float> defaultSettings = new()
        {
            { SettingsType.MasterVolume, 0.5f },
            { SettingsType.MusicVolume, 0.5f },
            { SettingsType.SFXVolume, 0.5f }
        };

        // Public property for backward compatibility
        public Dictionary<SettingsType, float> DefaultSettings => defaultSettings;

        protected override string ConfigFileName => "SaveLoadManagerConfig";
    }
} 