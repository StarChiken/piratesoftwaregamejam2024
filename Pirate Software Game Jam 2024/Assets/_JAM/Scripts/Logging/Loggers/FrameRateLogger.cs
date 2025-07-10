using System.Collections.Generic;
using UnityEngine;

namespace Logging.Loggers
{
    public class FrameRateLogger : MonoBehaviour
    {
        [SerializeField] private FrameRateLoggerConfiguration frameRateLoggerConfiguration;

        private float _lastLogTime;
        private float _totalTime;
        private float _longestFrameTime;
        private float _longestFrameTimeCountdown;
        private readonly Queue<float> _frameTimeQueue = new();

        private void Update()
        {
            var accountedTimespanInSeconds = frameRateLoggerConfiguration.AccountedTimespanInSeconds;
            var loggingIntervalInSeconds = frameRateLoggerConfiguration.LoggingIntervalInSeconds;
            var deltaTime = Time.unscaledDeltaTime;

            _totalTime += deltaTime;
            _frameTimeQueue.Enqueue(deltaTime);

            if (deltaTime > _longestFrameTime)
            {
                _longestFrameTime = deltaTime;
                _longestFrameTimeCountdown = _frameTimeQueue.Count;
            }

            while (_totalTime > accountedTimespanInSeconds)
            {
                var oldestFrameTime = _frameTimeQueue.Peek();
                var decrementedTotalTime = _totalTime - oldestFrameTime;

                if (decrementedTotalTime < accountedTimespanInSeconds) break;

                _frameTimeQueue.Dequeue();
                _totalTime = decrementedTotalTime;
                _longestFrameTimeCountdown -= 1;

                if (_longestFrameTimeCountdown != 0) continue;

                _longestFrameTime = 0f;

                var index = 0;

                foreach (var frameTime in _frameTimeQueue)
                {
                    if (frameTime > _longestFrameTime)
                    {
                        _longestFrameTime = frameTime;
                        _longestFrameTimeCountdown = index + 1;
                    }

                    index++;
                }
            }

            var unscaledTime = Time.unscaledTime;

            if (unscaledTime < _lastLogTime + loggingIntervalInSeconds) return;

            _lastLogTime = unscaledTime;

            var frameRate = _frameTimeQueue.Count / _totalTime;
            var lowestFrameRate = 1f / _longestFrameTime;

            LoggingManager.TryLog(this, $"Frame rate : {frameRate}, Lowest frame rate : {lowestFrameRate}");
        }
    }
}