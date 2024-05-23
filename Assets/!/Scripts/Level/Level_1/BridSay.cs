using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts;
using UniRx;
using UnityEngine;

public class BridSay : MonoBehaviour
{
    [SerializeField] private GameObject sayCanvas;
    [SerializeField] private GameObject hurtText;
    [SerializeField] private GameObject explainText;
    IDisposable _hurtDisposable;

    public void GetHurtCanvas()
    {
        _hurtDisposable?.Dispose();
        hurtText.SetActive(true);
        explainText.SetActive(false);

        _hurtDisposable = Observable.EveryUpdate().First().Delay(TimeSpan.FromSeconds(2)).Subscribe(_ =>
        {
            explainText.SetActive(true);

            hurtText.SetActive(false);
        }).AddTo(this);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sayCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sayCanvas.SetActive(false);
        }
    }
}