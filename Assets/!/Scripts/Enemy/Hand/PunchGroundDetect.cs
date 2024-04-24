using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class PunchGroundDetect : MonoBehaviour
{
    [SerializeField] private BehaviorTree bt;
    [SerializeField] private string eventName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bt.SendEvent(eventName);
            gameObject.SetActive(false);
            Debug.Log("punch ground detect");
        }
    }
}