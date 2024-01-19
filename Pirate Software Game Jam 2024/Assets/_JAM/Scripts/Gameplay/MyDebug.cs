using System.Text;
using Base.Core.Components;
using TMPro;
using UnityEngine;

namespace Base.Gameplay.UI
{
    public class DebugWindow : MyMonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        private StringBuilder _logStringBuilder = new();
        private bool _finished;
        private int _debugCounter;
        private int _maxDebugCount = 10;

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
        
        // private void Update()
        // {
        //     if (_debugCounter >= _maxDebugCount)
        //     {
        //         return;
        //     }
        //
        //     switch (_debugCounter)
        //     {
        //         case 0:
        //             Debug.Log("This is a test to see if the spacing between long strings is ok");
        //             break;
        //         case 1:
        //             Debug.Log("This is a test to see if the spacing between long strings is ok");
        //             Debug.Log("Also, this is a test to see if it can take more than one message");
        //             break;
        //         case 2:
        //             Debug.Log("This is a test to see if the spacing between long strings is ok");
        //             Debug.Log("Also, this is a test to see if it can take more than one message");
        //             Debug.Log("Another debug message for testing");
        //             break;
        //         case 3:
        //             Debug.Log("This is a test to see if the spacing between long strings is ok");
        //             Debug.Log("Also, this is a test to see if it can take more than one message");
        //             Debug.Log("Another debug message for testing");
        //             Debug.Log("Yet another debug message for testing");
        //             break;
        //         default:
        //             Debug.Log($"Default debug message for counter value {_debugCounter}");
        //             break;
        //     }
        //
        //     _debugCounter++;
        // }

    }
}