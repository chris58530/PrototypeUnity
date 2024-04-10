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

    void ReSpawnFadeOut()
    {
        ani.SetTrigger("ReSpawn");
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

        SystemActions.onPlayerRespawn += ReSpawnFadeOut;
    }

  

    private void OnDisable()
    {
        SystemActions.onSwitchScene -= FadeOutAnimation;
        
        SystemActions.onSceneStart -= FadeInAnimation;
        
        SystemActions.onPlayerRespawn -= ReSpawnFadeOut;

    }
}