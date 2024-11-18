
using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI headerField;
    public TextMeshProUGUI contentField;
    public LayoutElement layoutElement;
    public int characterWrapLimit;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetText(string content, string header = "")
    {
        if (string.IsNullOrEmpty(header))
        {
            headerField.gameObject.SetActive(false);
        }
        else
        {
           
            headerField.gameObject.SetActive(true);
            headerField.text = header;
            
        }
        
        contentField.text = content;
        
       
        layoutElement.enabled = Math.Max(headerField.preferredWidth, contentField.preferredWidth) >= layoutElement.preferredWidth;
        //int headerLength = headerField.text.Length;
        //int contentLength = contentField.text.Length;
        //layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;

    }
    
    public InputSystemUIInputModule inputModule;
    private void Update()
    {
        if (Application.isEditor)
        {
            layoutElement.enabled = Math.Max(headerField.preferredWidth, contentField.preferredWidth) >= layoutElement.preferredWidth;

            // int headerLength = headerField.text.Length;
            // int contentLength = contentField.text.Length;
            //layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
        }

        var MousePos = inputModule.point.action.ReadValue<Vector2>();
        
        // float pivotX = MousePos.x / Screen.width; 
        // float pivotY = MousePos.y / Screen.height;
        // rectTransform.pivot = new Vector2(pivotX, pivotY);
        
        var normalizedPosition = new Vector2(MousePos.x / Screen.width, MousePos.y / Screen.height);
        var pivot = CalculatePivot(normalizedPosition);
        rectTransform.pivot = pivot;
        
        // finalPivot = new Vector2 (finalPivotX, finalPivotY);
        //
        // if (rectTransform.pivot != finalPivot)
        // {
        //     moveSequence.Kill();
        //
        //     moveSequence = DOTween.Sequence()
        //         .Join(DOTween.To(() => rectTransform.pivot, x => rectTransform.pivot = x, new Vector2(finalPivotX, finalPivotY), .5f))
        //         .Join(DOTween.To(() => rectTransform.pivot, y => rectTransform.pivot = y, new Vector2(finalPivotX, finalPivotY), 1f))
        //         .SetRelative(false)
        //         .Play();
        // }
        
        transform.position = MousePos;

    }
    
    private Vector2 CalculatePivot(Vector2 normalizedPosition)
    {
        var pivotTopLeft = new Vector2(-0.05f, 1.05f);
        var pivotTopRight = new Vector2(1.05f, 1.05f);
        var pivotBottomLeft = new Vector2(-0.05f, -0.05f);
        var pivotBottomRight = new Vector2(1.05f, -0.05f);

        if (normalizedPosition.x < 0.5f && normalizedPosition.y >= 0.5f)
        {
            return pivotTopLeft;
        }
        else if (normalizedPosition.x > 0.5f && normalizedPosition.y >= 0.5f)
        {
            return pivotTopRight;
        }
        else if (normalizedPosition.x <= 0.5f && normalizedPosition.y < 0.5f)
        {
            return pivotBottomLeft;
        }
        else
        {
            return pivotBottomRight;
        }
    }
    
    
}