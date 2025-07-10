using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Shows a tooltip or action name when hovering over a button.
/// </summary>
public class ButtonShowText : MyMonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button m_button;
    [SerializeField] private TextMeshProUGUI m_text;
    [SerializeField] private string m_actionName;

    private void Start()
    {
        m_button = GetComponent<Button>();
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