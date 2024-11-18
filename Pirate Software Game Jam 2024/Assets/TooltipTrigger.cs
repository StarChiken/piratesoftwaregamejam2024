using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string header;
    [TextArea] [SerializeField] private string content; 
    
    public void OnPointerEnter (PointerEventData eventData)
    {
        TooltipSystem.Show(content, header);
    }
    
    public void OnPointerExit (PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }
    
}
