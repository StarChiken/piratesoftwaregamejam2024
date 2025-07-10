using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles UI logic for opening/closing a panel and managing other panels.
/// </summary>
public class ButtonBase : MyMonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject[] otherPanels;
    private bool panelState;
    private float startTime;
    
    private void Awake()
    {
        OpenClosePanel(false);
    }

    /// <summary>
    /// Opens or closes the panel, and manages other panels accordingly.
    /// </summary>
    public void OpenClosePanel(bool isActive)
    {
        panel.SetActive(isActive);
        foreach (var obj in otherPanels)
        {
            if (obj.activeSelf)
            {
                obj.SetActive(!isActive);
            }
        }
        panelState = isActive;
        // Reset open count and start timer when opening the panel
        if (isActive)
        {
            startTime = Time.time;
        }
    }

    private void Update()
    {
        // Example: auto-close after 10 seconds (commented out for now)
        // if (panelState && Time.time - startTime > 10)
        //     OpenClosePanel(false);
    }
}
