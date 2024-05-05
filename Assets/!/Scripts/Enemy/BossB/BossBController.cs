using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class BossBController : MonoBehaviour
{
    [Header("Objects")] [SerializeField] private BehaviorTree rightSmallHandBt;
    [SerializeField] private BehaviorTree leftSmallHandBt;


    public SharedBool ReturnRightGather()
    {
        if (rightSmallHandBt.gameObject.activeSelf)
            return (SharedBool)rightSmallHandBt.GetVariable("OnDestination");
        return true;
    }

    public SharedBool ReturnLeftGather()
    {
        if (leftSmallHandBt.gameObject.activeSelf)
            return (SharedBool)leftSmallHandBt.GetVariable("OnDestination");
        return true;
    }
}