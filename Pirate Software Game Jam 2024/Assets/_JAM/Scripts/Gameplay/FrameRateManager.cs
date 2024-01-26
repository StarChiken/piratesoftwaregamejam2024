using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FrameRateManager : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

    private int frameIndex = 0;

    private int minFrames = 10000;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        frameIndex++;
        if (frameIndex == 10)
        {
            frameIndex = 0;
            int frameRate = (int)(1 / Time.deltaTime);
            if (frameRate < minFrames)
            {
                minFrames = frameRate;
            }

            textMesh.text = $"Framerate: {frameRate} \n Min FPS: {minFrames}";
        }
    }
}
