using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Event;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHurtPanel : MonoBehaviour
{
    [SerializeField] private Image hurtImage;

    private void OnEnable()
    {
        hurtImage.enabled = false;
        PlayerActions.onPlayerHurt += OpenImage;
    }

    private void OnDisable()
    {
        PlayerActions.onPlayerHurt -= OpenImage;
    }

    void OpenImage()
    {
        hurtImage.enabled = true;
        StartCoroutine(nameof(CloseImage));
    }

    IEnumerator CloseImage()
    {
        yield return new WaitForSeconds(.1f);
        hurtImage.enabled = false;
    }
}