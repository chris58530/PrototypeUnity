using System.Collections;
using System.Collections.Generic;
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
        public void FadeInAnimation()
    {
        if (ani == null) return;
        ani.SetTrigger("FadeIn");
        Debug.Log("FadeIn");

    }
    public void FadeOutAnimation()
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

    // private void OnEnable()
    // {
    //     Actions.GameOverUI += FadeOutAnimation;
    //     Actions.GameStartUI += FadeInAnimation;
    // }
    public void GroupOn()
    {
        canvasGroup.alpha = 1;
    }
    public void GroupOff()
    {
        canvasGroup.alpha = 0;

    }
    // private void OnDisable()
    // {
    //     Actions.GameOverUI -= FadeOutAnimation;
    //     Actions.GameStartUI -= FadeInAnimation;

    // }

}
