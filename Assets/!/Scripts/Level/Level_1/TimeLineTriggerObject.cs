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

    public bool CanConfirmTimeline;

    private void Update()
    {
        if (!CanConfirmTimeline) return;
        
        ConfirmTimeline();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.GetComponentInChildren<PlayerUseTimeLineUI>()) return;
        other.gameObject.GetComponentInChildren<PlayerUseTimeLineUI>().ShowCanConfirmImage(true);
        CanConfirmTimeline = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.GetComponentInChildren<PlayerUseTimeLineUI>()) return;

        other.gameObject.GetComponentInChildren<PlayerUseTimeLineUI>().ShowCanConfirmImage(false);

        CanConfirmTimeline = false;
    }

    public void ConfirmTimeline()
    {
        if (!Input.GetKeyDown(KeyCode.Q)) return;

        TimeLineManager.Instance.PlayTimeLine(timeLineNumber);
        Debug.Log($"Play number {timeLineNumber} TimeLine");

        if (repeat) return;
        Destroy(gameObject);
    }
}