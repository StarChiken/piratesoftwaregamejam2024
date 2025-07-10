using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Shows a tooltip or action name when hovering over a button.
/// </summary>
public class ButtonShowText : MyMonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField, Tooltip("Button to show text")] private Button m_button;
    [SerializeField, Tooltip("Text component to display action name")] private TextMeshProUGUI m_text;
    [SerializeField, Tooltip("Action name to display on hover")] private string m_actionName;

    private void Start()
    {
        if (!TryGetComponent<Button>(out m_button))
        {
            Debug.LogError("Button component is missing from ButtonShowText.");
        }
    }

    /// <summary>
    /// Shows the action name when the pointer enters the button.
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        m_text.text = m_actionName;
    }

    /// <summary>
    /// Clears the action name when the pointer exits the button.
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        m_text.text = "";
    }
}