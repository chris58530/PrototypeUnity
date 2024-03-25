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

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent<PlayerInput>(out var player)) return;

        TimeLineManager.Instance.PlayTimeLine(timeLineNumber);
        Debug.Log($"Play number {timeLineNumber} TimeLine");
        
        if (repeat) return;
        Destroy(gameObject);
    }
}