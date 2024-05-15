using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossBDieFadeOutCanvas : MonoBehaviour
{
    [SerializeField]private Image fadeOutImage;
    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeOutCoroutine()
    {
        
    }
}