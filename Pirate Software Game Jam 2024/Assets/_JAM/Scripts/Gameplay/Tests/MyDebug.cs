using System.Text;
using Base.Core.Components;
using TMPro;
using UnityEngine;

namespace Base.Gameplay
{
    /// <summary>
    /// Testing Purposes Only
    /// </summary>
    // Captures and displays log messages in a TextMeshProUGUI component.
    public class DebugWindow : MyMonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        private StringBuilder _logStringBuilder = new();

        private void Awake()
        {
            Application.logMessageReceived += ShowLog;
        }

        private void OnDestroy()
        {
            Application.logMessageReceived -= ShowLog;
        }
        
        private void ShowLog(string logString, string stackTrace, LogType type)
        {
            _logStringBuilder.AppendLine(logString);
            text.text = _logStringBuilder.ToString();
        }
    }
}