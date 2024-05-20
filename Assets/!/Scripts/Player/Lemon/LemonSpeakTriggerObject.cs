using System;
using UniRx;
using UnityEngine;

public class LemonSpeakTriggerObject : MonoBehaviour
{
    [SerializeField] private bool repeat;

    [SerializeField] private bool enableSpeak;

    [SerializeField] private float delayTime;
    [SerializeField] private LemonSpeakEnum speakEvent;

    private void OnEnable()
    {
        if (enableSpeak)
        {
            Observable.EveryUpdate().First().Delay(TimeSpan.FromSeconds(delayTime)).Subscribe(_ =>
            {
                LemonBase.onUseBTSpeak?.Invoke(speakEvent);
                if (!repeat) enabled = false;
            }).AddTo(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Observable.EveryUpdate().First().Delay(TimeSpan.FromSeconds(delayTime)).Subscribe(_ =>
            {
                LemonBase.onUseBTSpeak?.Invoke(speakEvent);
                if (!repeat) enabled = false;
            }).AddTo(this);
        }
    }
}