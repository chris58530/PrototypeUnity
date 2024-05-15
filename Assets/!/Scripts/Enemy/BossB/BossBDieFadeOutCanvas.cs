using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossBDieFadeOutCanvas : MonoBehaviour
{
    [SerializeField] private GameObject fadeOutImageObject;


    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeOutCoroutine()
    {
        //use dotween let fadeoutimage opacity to 100%
        yield return new WaitForSeconds(6);
        fadeOutImageObject.SetActive(true);
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("EndingScene");
    }
}