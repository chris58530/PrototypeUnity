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
    private bool _isExecuted;

    private void OnTriggerEnter(Collider other)
    {
        if (!repeat && _isExecuted) return;
        if (!other.gameObject.TryGetComponent<PlayerInput>(out var player)) return;
        
        _isExecuted = true;
        TimeLineManager.Instance.PlayTimeLine(timeLineNumber);
        Debug.Log($"Play number {timeLineNumber} TimeLine");
    }
}