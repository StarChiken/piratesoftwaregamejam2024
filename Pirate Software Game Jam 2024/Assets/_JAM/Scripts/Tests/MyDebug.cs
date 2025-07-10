using System.Text;
using TMPro;
using UnityEngine;

/// <summary>
/// Testing Purposes Only - Captures and displays log messages in a TextMeshProUGUI component.
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class DebugWindow : MyMonoBehaviour
{
    #region Fields
    [SerializeField, Tooltip("Text component to display debug messages")]
    private TextMeshProUGUI m_text;
    
    private readonly StringBuilder m_logStringBuilder = new();
    #endregion

    #region Unity Lifecycle
    private void Awake()
    {
        ValidateRequiredComponents();
        SubscribeToLogEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromLogEvents();
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Validates that all required components are assigned.
    /// </summary>
    private void ValidateRequiredComponents()
    {
        if (m_text == null)
        {
            m_text = GetComponent<TextMeshProUGUI>();
            if (m_text == null)
            {
                Debug.LogError("TextMeshProUGUI component is missing from DebugWindow.");
            }
        }
    }

    /// <summary>
    /// Subscribes to Unity's log message events.
    /// </summary>
    private void SubscribeToLogEvents()
    {
        try
        {
            Application.logMessageReceived += ShowLog;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error subscribing to log events: {ex.Message}");
        }
    }

    /// <summary>
    /// Unsubscribes from Unity's log message events.
    /// </summary>
    private void UnsubscribeFromLogEvents()
    {
        try
        {
            Application.logMessageReceived -= ShowLog;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error unsubscribing from log events: {ex.Message}");
        }
    }
    
    /// <summary>
    /// Displays log messages in the text component.
    /// </summary>
    /// <param name="logString">The log message to display.</param>
    /// <param name="stackTrace">The stack trace of the log message.</param>
    /// <param name="type">The type of log message.</param>
    private void ShowLog(string logString, string stackTrace, LogType type)
    {
        try
        {
            if (string.IsNullOrEmpty(logString))
            {
                return;
            }

            m_logStringBuilder.AppendLine(logString);
            
            if (m_text != null)
            {
                m_text.text = m_logStringBuilder.ToString();
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error showing log message: {ex.Message}");
        }
    }
    #endregion
}