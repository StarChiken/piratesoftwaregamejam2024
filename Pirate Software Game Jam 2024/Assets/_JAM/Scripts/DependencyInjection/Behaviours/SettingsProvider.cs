using DesignPatterns.DependencyInjection.Attributes;
using DesignPatterns.DependencyInjection.Interfaces;
using Logging.Loggers;
using UnityEngine;

namespace DesignPatterns.DependencyInjection.Behaviours
{
    public class SettingsProvider : MonoBehaviour, IProvider
    {
        // Could provide the same settings multiple times under different names by providing interfaces present on configuration scriptable objects
        [SerializeField, Provide] private FrameRateLoggerConfiguration frameRateLoggerConfiguration;
    }
}