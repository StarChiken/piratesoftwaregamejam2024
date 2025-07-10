using DesignPatterns.DependencyInjection.Attributes;
using DesignPatterns.DependencyInjection.Interfaces;
using Logging.Loggers;
using UnityEngine;

namespace Logging
{
    public class LoggingManager : MonoBehaviour, ISelfProvider
    {
        [SerializeField] private LoggingConfiguration loggingConfiguration;

        [Inject]
        private static LoggingManager _instance;

        public static void TryLog(object obj, string message)
        {
            if (!_instance || obj == null) return;

            _instance.Log(obj, message);
        }

        public void Log(object obj, string message)
        {
            var loggingType = GetLoggingTypeFromObject(obj);

            if (loggingConfiguration.LoggingSettingsDictionary.TryGetValue(loggingType, out var isLoggingEnabled) && isLoggingEnabled)
            {
                Debug.Log(loggingType + " > " + message);
            }
        }

        private static LoggingType GetLoggingTypeFromObject(object obj)
        {
            return obj switch
            {
                FrameRateLogger => LoggingType.FrameRateLogger,
                _ => LoggingType.Unknown
            };
        }
    }
}