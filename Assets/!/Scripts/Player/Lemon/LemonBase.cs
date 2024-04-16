using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public enum LemonSpeakEnum
{
    Normal,
    Shield,
    Keymonster,
    QuackNormal,
    QuackBigBomb,
    QuackDieNormal
}

public class LemonBase : MonoBehaviour
{
    [SerializeField]private BehaviorTree bt;


    public static Action<string[]> onMissionSpeak;

    public static Action<string[], float> onSpeak;
    
    public static Action<LemonSpeakEnum> onUseBTSpeak;
    private bool _isSpeaking;
    private void Awake()
    {
        SentSpeakEvent(LemonSpeakEnum.Normal);

    }


    private void OnEnable()
    {
        onUseBTSpeak += SentSpeakEvent;
    }
    private void OnDisable()
    {
        onUseBTSpeak -= SentSpeakEvent;
    }


    public void SentSpeakEvent(LemonSpeakEnum eventName)
    {
        string name = eventName.ToString();        

        if (eventName == LemonSpeakEnum.Normal)
        {
            name = SceneManager.GetActiveScene().name;
        }


        bt.SendEvent(name);
    }

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
        onSpeak?.Invoke(text, 1.5f);


        SharedBool islastSpeakShared = isMission;
        bt.SetVariable("InMission", islastSpeakShared);
    }
}