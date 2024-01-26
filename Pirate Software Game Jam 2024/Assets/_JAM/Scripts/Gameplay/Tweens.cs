using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Tweens : MonoBehaviour
{
    private Image JamLogo;
    private Image GameLogo;

    public float FadeInDuration = 0.5f;
    public float FadeOutDuration = 1.5f;

    // Use this for initialization
    void Start () 
    {
        StartCoroutine(TweenJamLogo());
    }

    public IEnumerator TweenJamLogo()
    {
        JamLogo.DOFade(1.0f, FadeInDuration);
        yield return new WaitForSeconds(3f);
        JamLogo.DOFade(0.0f, FadeOutDuration).OnComplete(TweenComplete);
    }

    private void TweenComplete()
    {
        StartCoroutine(TweenGameLogo());
    }

    public IEnumerator TweenGameLogo()
    {
        GameLogo.DOFade(1.0f, FadeInDuration);
        yield return new WaitForSeconds(3f);
        GameLogo.DOFade(0.0f, FadeOutDuration).OnComplete(TweenComplete);
    }
}
