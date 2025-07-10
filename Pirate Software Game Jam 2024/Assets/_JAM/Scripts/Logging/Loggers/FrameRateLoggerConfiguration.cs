using UnityEngine;

namespace Logging.Loggers
{
    [CreateAssetMenu(fileName = "FrameRateLoggerConfiguration", menuName = "Configuration/FrameRateLoggerConfiguration", order = 0)]
    public class FrameRateLoggerConfiguration : ScriptableObject
    {
        [field: SerializeField, Min(0.1f)] public float LoggingIntervalInSeconds { get; private set; } = 1f;
        [field: SerializeField, Min(1f)] public float AccountedTimespanInSeconds { get; private set; } = 1f;
    }
}