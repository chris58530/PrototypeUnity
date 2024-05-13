using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.Serialization;

public class Elite_Hand_CanAttackScripts : MonoBehaviour
{
    [SerializeField] private BehaviorTree eliteHandBt;
    [SerializeField] private float dalayTime;

    private void Start()
    {
        Invoke(nameof(EnableBt), dalayTime);
    }

    void EnableBt()
    {
        eliteHandBt.enabled = true;
        Destroy(gameObject);
    }
}