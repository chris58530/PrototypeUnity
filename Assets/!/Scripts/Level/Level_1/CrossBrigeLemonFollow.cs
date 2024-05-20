using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class CrossBrigeLemonFollow : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SharedBool islastSpeakShared = false;
            FindObjectOfType<LemonBase>().moveBT.SetVariable("InMission", islastSpeakShared);
        }
    }
}