using System.Collections;
using System.Collections.Generic;
using _.Scripts.Event;
using UnityEngine;

public class FadeCanvas : MonoBehaviour
{
    Animator ani;
    CanvasGroup canvasGroup;


    // 測試
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            FadeInAnimation();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            FadeOutAnimation();
        }
    }

    void FadeInAnimation()
    {
        if (ani == null) return;
        ani.SetTrigger("FadeIn");
        Debug.Log("FadeIn");
    }

    void FadeOutAnimation()
    {
        if (ani == null) return;
        ani.SetTrigger("FadeOut");
        Debug.Log("FadeOut");
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