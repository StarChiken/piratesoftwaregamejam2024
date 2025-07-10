using System;
using System.Collections.Generic;
using UnityEngine;

namespace Logging
{
    [CreateAssetMenu(fileName = "LoggingConfiguration", menuName = "Configuration/LoggingConfiguration", order = 0)]
    public class LoggingConfiguration : ScriptableObject, ISerializationCallbackReceiver
    {
        [Serializable]
        private struct LoggingSettings
        {
            [SerializeField] private LoggingType loggingType;
            [SerializeField] private bool isLoggingEnabled;

            public void Deconstruct(out LoggingType lt, out bool b)
            {
                lt = loggingType;
                b = isLoggingEnabled;
            }
        }

        [SerializeField] private LoggingSettings[] loggingSettingsArray;

        public IReadOnlyDictionary<LoggingType, bool> LoggingSettingsDictionary => _loggingSettingsDictionary;

        private Dictionary<LoggingType, bool> _loggingSettingsDictionary = new();

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            _loggingSettingsDictionary = new Dictionary<LoggingType, bool>();
 
            foreach (var (loggingType, isLoggingEnabled) in loggingSettingsArray)
            {
                _loggingSettingsDictionary.TryAdd(loggingType, isLoggingEnabled);
            }
        }
    }
}