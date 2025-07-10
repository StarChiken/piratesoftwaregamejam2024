using System;
using UnityEngine;

/// <summary>
/// Handles pausing and time scale modification for the game.
/// </summary>
public class Pause : MyMonoBehaviour
{
    [SerializeField, Range(0.1f, 2)] private float m_modifiedScale = 1f;

    private void OnEnable()
    {
        Time.timeScale = 0.1f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
        Time.timeScale = m_modifiedScale;
    }
}
