using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Shows a tooltip or action name when hovering over a button.
/// </summary>
public class ButtonShowText : MyMonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string actionName;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    /// <summary>
    /// Shows the action name when the pointer enters the button.
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        text.text = actionName;
    }

    /// <summary>
    /// Clears the action name when the pointer exits the button.
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        text.text = "";
    }
}