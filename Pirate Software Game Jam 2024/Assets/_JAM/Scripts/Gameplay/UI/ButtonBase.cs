using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles UI logic for opening/closing a panel and managing other panels.
/// </summary>
public class ButtonBase : MyMonoBehaviour
{
    [SerializeField, Tooltip("Panel to open/close")] private GameObject m_panel;
    [SerializeField, Tooltip("Other panels to manage")] private GameObject[] m_otherPanels;
    private bool m_panelState;
    private float m_startTime;
    
    private void Awake()
    {
        OpenClosePanel(false);
    }

    /// <summary>
    /// Opens or closes the panel, and manages other panels accordingly.
    /// </summary>
    public void OpenClosePanel(bool isActive)
    {
        m_panel.SetActive(isActive);
        foreach (var obj in m_otherPanels)
        {
            if (obj.activeSelf)
            {
                obj.SetActive(!isActive);
            }
        }
        m_panelState = isActive;
        // Reset open count and start timer when opening the panel
        if (isActive)
        {
            m_startTime = Time.time;
        }
    }

    private void Update()
    {
        // Example: auto-close after 10 seconds (commented out for now)
        // if (m_panelState && Time.time - m_startTime > 10)
        //     OpenClosePanel(false);
    }
}
