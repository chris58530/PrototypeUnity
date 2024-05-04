using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class BossBController : MonoBehaviour
{
    [Header("Objects")] 
    [SerializeField] private BehaviorTree rightSmallHandBt;
    [SerializeField] private BehaviorTree leftSmallHandBt;


    public SharedBool ReturnRightGather()
    {
        return (SharedBool)rightSmallHandBt.GetVariable("OnDestination");
    }
    public SharedBool ReturnLeftGather()
    {
        return (SharedBool)leftSmallHandBt.GetVariable("OnDestination");
    }
}