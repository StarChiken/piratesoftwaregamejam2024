using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Base.Core.Managers
{
    /// <summary>
    /// Stores player settings data for saving/loading.
    /// </summary>
    public class PlayerSettingsData : ISaveData
    {
        public Dictionary<SettingsType, SettingsData> SettingsList;
        public PlayerSettingsData(Dictionary<SettingsType, float> defaultSettings)
        {
            SettingsList = new();
            foreach (var kvp in defaultSettings)
            {
                SettingsList[kvp.Key] = new SettingsData { SettingsType = kvp.Key, SettingsAmount = kvp.Value };
            }
        }
    }
    
    /// <summary>
    /// Manages saving/loading and player settings.
    /// </summary>
    public class SaveLoadManager : BaseManager
    {
        private readonly SaveLoadManagerConfig _config;
        private PlayerSettingsData _playerSettings;
        
        public SaveLoadManager(SaveLoadManagerConfig config, Action<BaseManager> onComplete) : base(onComplete)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _playerSettings = new PlayerSettingsData(_config.DefaultSettings);
            OnInitComplete();
        }
        
        public void SaveData(ISaveData saveData)
        {
            var typeName = saveData.GetType().FullName;
            var savePath = $"{Application.persistentDataPath}/{typeName}.SaveFile";
            //var dataText = JsonConvert.SerializeObject(saveData);
            //File.WriteAllText(savePath, dataText);
        }
        
        public void ChangeValue(float value, SettingsType scoreTypes)
        {
            if (!_playerSettings.SettingsList.TryGetValue(scoreTypes, out var scoreData))
            {
                _playerSettings.SettingsList.Add(scoreTypes, new SettingsData
                {
                    SettingsType = scoreTypes,
                    SettingsAmount = 0
                });
            }
            _playerSettings.SettingsList[scoreTypes].ChangeAmount(value);
            SaveData(_playerSettings);
        }
        
        public float GetValueAsFloat(SettingsType scoreTypes)
        {
            if (_playerSettings.SettingsList.TryGetValue(scoreTypes, out var settingsData))
            {
                return settingsData.GetSettingsAmountInt();
            }
            _playerSettings.SettingsList.Add(scoreTypes, new SettingsData{ SettingsType = scoreTypes,SettingsAmount = 0} );
            Debug.Log($"Given key {scoreTypes} was not present in the dictionary, created new and set score to 0.");
            return 0;
        }
    }

    public interface ISaveData { }
    
    public class SettingsData
    {
        public SettingsType SettingsType;
        public float SettingsAmount;
        
        public void ChangeAmount(float value)
        {
            float newAmount = value;
            if (newAmount < 0)
            {
                Debug.Log("Cannot decrease SettingsAmount below 0.");
                return;
            }
            SettingsAmount = value;
        }
        public float GetSettingsAmountInt() => SettingsAmount;
    }
    
    public enum SettingsType
    {
        MasterVolume, MusicVolume, SFXVolume 
    }
}