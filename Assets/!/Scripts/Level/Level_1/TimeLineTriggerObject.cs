using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Player;
using UnityEngine;

public class TimeLineTriggerObject : MonoBehaviour
{
    [Tooltip("TimeLineManagers PlayableDirector Number")] [SerializeField]
    private int timeLineNumber;

    [SerializeField] private bool repeat;
    [SerializeField] private bool needConfrim;

    public bool CanConfirmTimeline;

    private PlayerUseTimeLineUI _playerUseTimeLineUI;

    private void Update()
    {
        if (!CanConfirmTimeline) return;

        ConfirmTimeline();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.GetComponentInChildren<PlayerUseTimeLineUI>()) return;
        if (!needConfrim)
        {
            TimeLineManager.Instance.PlayTimeLine(timeLineNumber);
            Debug.Log($"Play number {timeLineNumber} TimeLine");
            if (repeat) return;

            Destroy(gameObject);
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
        if (!Input.GetKeyDown(KeyCode.Q)) return;

        TimeLineManager.Instance.PlayTimeLine(timeLineNumber);
        Debug.Log($"Play number {timeLineNumber} TimeLine");
        
        _playerUseTimeLineUI.ShowCanConfirmImage(false);


        if (repeat) return;
        Destroy(gameObject);
    }
}