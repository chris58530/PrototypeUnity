using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Event;
using UniRx;
using UnityEngine;

public class FadeCanvas : MonoBehaviour
{
    Animator ani;
    CanvasGroup canvasGroup;


    void FadeInAnimation()
    {
        ani.SetTrigger("FadeIn");
    }

    void FadeOutAnimation(float time)
    {
        Observable.EveryUpdate().First().Delay(TimeSpan.FromSeconds(time))
            .Subscribe(_ => { ani.SetTrigger("FadeOut"); }).AddTo(this);
    }

    void Awake()
    {
        ani = GetComponent<Animator>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        SystemActions.onSwitchScene += FadeOutAnimation;
        SystemActions.onSceneStart += FadeInAnimation;
    }

    public void GroupOn()
    {
        canvasGroup.alpha = 1;
    }

    public void GroupOff()
    {
        canvasGroup.alpha = 0;
    }

    private void OnDisable()
    {
        SystemActions.onSwitchScene -= FadeOutAnimation;
        SystemActions.onSceneStart -= FadeInAnimation;
    }
}