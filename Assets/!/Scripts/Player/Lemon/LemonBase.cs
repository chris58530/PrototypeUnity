using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using TMPro;
using UnityEngine;

public class LemonBase : MonoBehaviour
{
    private BehaviorTree bt;
    
    
    public static Action<string[]> onMissionSpeak;
    public static Action<string[],float> onSpeak;
    // private void Awake()
    // {
    //     bt = GetComponent<BehaviorTree>();
    // }
    //
    //

    public void SetDestination(string[] text, GameObject targetTransObj, bool isMission)
    {
        if (bt == null)
        {
            bt = GetComponent<BehaviorTree>();
        }
        onMissionSpeak?.Invoke(text);
Debug.Log("lemon in mission");
        //set behaviour disgner the target position
        SharedGameObject targetShared = targetTransObj;
        bt.SetVariable("MissionPosObject", targetShared);

        //Set behaviour disgner InMission
        SharedBool islastSpeakShared = isMission;
        bt.SetVariable("InMission", islastSpeakShared);

    }
    public void SetSpeak(string[] text, bool isMission)
    {
        //set canvas text
        onSpeak?.Invoke(text,1.5f);

       
        SharedBool islastSpeakShared = isMission;
        bt.SetVariable("InMission", islastSpeakShared);
    }

   
}