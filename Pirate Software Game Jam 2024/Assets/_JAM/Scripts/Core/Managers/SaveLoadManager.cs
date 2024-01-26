using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Base.Core.Managers
{
    public class PlayerSettingsData : ISaveData
    {
        public Dictionary<SettingsType, SettingsData> SettingsList = new()
        {
            { SettingsType.MasterVolume, new SettingsData { SettingsType = SettingsType.MasterVolume, SettingsAmount = 0.5f } },
            { SettingsType.MusicVolume, new SettingsData { SettingsType = SettingsType.MusicVolume, SettingsAmount = 0.5f } },
            { SettingsType.SFXVolume, new SettingsData { SettingsType = SettingsType.SFXVolume, SettingsAmount = 0.5f } }
        };
    }
    
    public class SaveLoadManager : BaseManager
    {
        private PlayerSettingsData playerSettings; // Stores the player's settings data.
        
        public SaveLoadManager(Action<BaseManager> onComplete) : base(onComplete)
        {
            playerSettings ??= new PlayerSettingsData();
            OnInitComplete();
        }
        
        public void SaveData(ISaveData saveData)
        {
            var typeName = saveData.GetType().FullName;
            var savePath = $"{Application.persistentDataPath}/{typeName}.SaveFile";
            //var dataText = JsonConvert.SerializeObject(saveData);
            //File.WriteAllText(savePath, dataText);
        }
        
        public void ChangeValue(float value, SettingsType scoreTypes) // Add value of settings the given type 
        {
            if (!playerSettings.SettingsList.TryGetValue(scoreTypes, out var scoreData)) // Check if the settings type exists in the dictionary
            {
                // If the settings type doesn't exist, add a new entry to the dictionary with default value (0)
                playerSettings.SettingsList.Add(scoreTypes, new SettingsData
                {
                    SettingsType = scoreTypes,
                    SettingsAmount = 0
                });
            }
            
            playerSettings.SettingsList[scoreTypes].ChangeAmount(value);
            SaveData(playerSettings);
        }
        
        public float GetValueAsFloat(SettingsType scoreTypes)
        {
            if (playerSettings.SettingsList.TryGetValue(scoreTypes, out var settingsData))
            {
                return settingsData.GetSettingsAmountInt();
            }
            playerSettings.SettingsList.Add(scoreTypes, new SettingsData{ SettingsType = scoreTypes,SettingsAmount = 0} );
            
            Debug.Log($"Given key {scoreTypes} was not present in the dictionary, created new and set score to 0.");
            
            return 0;
        }
    }

    public interface ISaveData
    {

    }
    
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