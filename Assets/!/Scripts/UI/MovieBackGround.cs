using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class MovieBackGround : MonoBehaviour
{
    [SerializeField] private Image up;
    [SerializeField] private Image down;

    private IDisposable outPlayModeInsuranceDisposable;

    private void OnEnable()
    {
        TimeLineManager.onPlayTimelLine += InPlayMode;
        TimeLineManager.onQuitTimelLine += OutPlayMode;

    }

    private void InPlayMode()
    {
        outPlayModeInsuranceDisposable?.Dispose();
        
        up.transform.gameObject.SetActive(true);
        down.transform.gameObject.SetActive(true);

        up.transform.DOLocalMove(new Vector3(0, 525, 0), 1f);
        down.transform.DOLocalMove(new Vector3(0, -525, 0), 1f);
    }
    private void OutPlayMode()
    {
        up.transform.DOLocalMove(new Vector3(0, 650, 0), 1f);
        down.transform.DOLocalMove(new Vector3(0, -650, 0), 1f);
        
        outPlayModeInsuranceDisposable = Observable.EveryFixedUpdate().First().Delay(TimeSpan.FromSeconds(5)).Subscribe(_ =>
        {
            up.transform.gameObject.SetActive(false);
            down.transform.gameObject.SetActive(false);
        }).AddTo(this);
    }

    private void OnDisable()
    {
        TimeLineManager.onPlayTimelLine -= InPlayMode;
        TimeLineManager.onQuitTimelLine -= OutPlayMode;
    }
}