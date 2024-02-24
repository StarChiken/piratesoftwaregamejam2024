using Base.Core.Components;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonShowText : MyMonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    public TextMeshProUGUI text;
    public string actionName;
    void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.text = actionName;
    }

    // Called when mouse pointer exits the button
    public void OnPointerExit(PointerEventData eventData)
    {
        text.text = "";
    }
}