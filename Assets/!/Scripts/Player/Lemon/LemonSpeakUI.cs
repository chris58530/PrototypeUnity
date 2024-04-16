using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;
using Random = UnityEngine.Random;


public class LemonSpeakUI : MonoBehaviour
{
    [SerializeField] private GameObject speakCanvas;
    [SerializeField] private TMP_Text dialogText;

    private string[] _randomSpeak;

    [SerializeField] private float intervalSpeakTime;

    private IDisposable _randomSpeakDisposable;
    private IDisposable _showUIDisposable;

    private void OnEnable()
    {
        LemonBase.onMissionSpeak += MissionSpeak;
        LemonBase.onSpeak += Speak;
    }

    private void OnDisable()
    {
        LemonBase.onMissionSpeak -= MissionSpeak;
        LemonBase.onSpeak -= Speak;
    }

    private void MissionSpeak(string[] text)
    {
        _randomSpeakDisposable?.Dispose();
        _showUIDisposable?.Dispose();

        _randomSpeak = text;


        _randomSpeakDisposable = Observable.Interval(TimeSpan.FromSeconds(intervalSpeakTime))
            .First().Delay(TimeSpan.FromSeconds(.2f))
            .Subscribe(_ =>
            {
                speakCanvas.SetActive(true);

                int randomText = Random.Range(0, _randomSpeak.Length);

                dialogText.text = _randomSpeak[randomText];
            }).AddTo(this);
    }

    private void Speak(string[] text, float time)
    {
        _randomSpeakDisposable?.Dispose();
        _showUIDisposable?.Dispose();

        speakCanvas.SetActive(true);
        int randomText = Random.Range(0, _randomSpeak.Length);
        dialogText.text = _randomSpeak[randomText];

        // _randomSpeakDisposable = Observable.Interval(TimeSpan.FromSeconds(intervalSpeakTime))
        //     .First().Subscribe(_ =>
        //     {
        //         int randomText = Random.Range(0, _randomSpeak.Length);
        //         dialogText.text = _randomSpeak[randomText];
        //     }).AddTo(this);

        _showUIDisposable = Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(time)).First().Subscribe(_ =>
        {
            speakCanvas.SetActive(false);
        }).AddTo(this);
    }
}