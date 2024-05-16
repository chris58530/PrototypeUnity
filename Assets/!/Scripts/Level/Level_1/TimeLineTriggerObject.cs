using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Event;
using _.Scripts.Level;
using _.Scripts.Player;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

public class TimeLineTriggerObject : MonoBehaviour
{
    [Tooltip("TimeLineManagers PlayableDirector Number")] [SerializeField]
    private int timeLineNumber;

    [SerializeField] private bool repeat;
    [SerializeField] private bool needConfrim;

    public bool CanConfirmTimeline;
    [SerializeField] private bool spawnReset;
    private PlayerUseTimeLineUI _playerUseTimeLineUI;
    private bool _isPlaying;
    private bool _isPlayed;
    private UIInput _uiInput;
    [SerializeField] private UnityEvent onTimelineConfirmed;

    private void Start()
    {
        _uiInput = FindObjectOfType<UIInput>();
    }

    private void Update()
    {
        if (!CanConfirmTimeline) return;

        ConfirmTimeline();
    }

    private void OnTriggerStay(Collider other)
    {
        if (_isPlaying) return;
        if (!repeat && _isPlayed) return;

        if (!other.gameObject.GetComponentInChildren<PlayerUseTimeLineUI>()) return;
        if (!needConfrim)
        {
            TimeLineManager.Instance.PlayTimeLine(timeLineNumber);
            Debug.Log($"Play number {timeLineNumber} TimeLine");
            _isPlayed = true;
            return;
        }


        _playerUseTimeLineUI = other.gameObject.GetComponentInChildren<PlayerUseTimeLineUI>();
        _playerUseTimeLineUI.ShowCanConfirmImage(true);

        CanConfirmTimeline = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.GetComponentInChildren<PlayerUseTimeLineUI>()) return;


        _playerUseTimeLineUI = other.gameObject.GetComponentInChildren<PlayerUseTimeLineUI>();
        _playerUseTimeLineUI.ShowCanConfirmImage(false);

        CanConfirmTimeline = false;
    }

    public void ConfirmTimeline()
    {
        if (!_uiInput.Confirm) return;
        if (_isPlaying) return;
        _isPlayed = true;

        _isPlaying = true;
        TimeLineManager.Instance.PlayTimeLine(timeLineNumber);
        onTimelineConfirmed?.Invoke();
        Debug.Log($"Play number {timeLineNumber} TimeLine");

        _playerUseTimeLineUI.ShowCanConfirmImage(false);
    }

    public void ResetPlayed()
    {
        _isPlayed = false;
    }


    private void NotPlaying()
    {
        Observable.EveryUpdate().First().Delay(TimeSpan.FromSeconds(2)).Subscribe(_ => { _isPlaying = false; })
            .AddTo(this);
    }

    private void OnEnable()
    {
        TimeLineManager.onQuitTimelLine += NotPlaying;

        if (spawnReset)
        {
            SystemActions.onPlayerRespawn += ResetPlayed;
        }
    }

    private void OnDisable()
    {
        TimeLineManager.onQuitTimelLine -= NotPlaying;


        if (spawnReset)
        {
            SystemActions.onPlayerRespawn -= ResetPlayed;
        }
    }
}