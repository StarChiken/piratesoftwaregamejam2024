using System;
using System.Collections.Generic;

namespace Base.Core.Managers
{
    /// <summary>
    /// Configuration data for initializing SaveLoadManager and default settings.
    /// </summary>
    [Serializable]
    public class SaveLoadManagerConfig
    {
        public Dictionary<SettingsType, float> DefaultSettings = new()
        {
            { SettingsType.MasterVolume, 0.5f },
            { SettingsType.MusicVolume, 0.5f },
            { SettingsType.SFXVolume, 0.5f }
        };
    }
} 