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
    private void Awake()
    {
        bt = GetComponent<BehaviorTree>();
    }

  

    public void SetMission(string[] text, GameObject targetTransObj, bool isMission)
    {
        //set canvas text
        onMissionSpeak?.Invoke(text);

        //set behaviour disgner the target position
        SharedGameObject targetShared = targetTransObj;
        bt.SetVariable("MissionPosObject", targetShared);

        //Set behaviour disgner InMission
        SharedBool islastSpeakShared = isMission;
        bt.SetVariable("InMission", islastSpeakShared);

    }
    public void SetSpeak(string[] text, GameObject targetTransObj, bool isMission)
    {
        //set canvas text
        onSpeak?.Invoke(text,3);

        //set behaviour disgner the target position
        SharedGameObject targetShared = targetTransObj;
        bt.SetVariable("MissionPosObject", targetShared);

        //Set behaviour disgner InMission
        SharedBool islastSpeakShared = isMission;
        bt.SetVariable("InMission", islastSpeakShared);

    }

   
}