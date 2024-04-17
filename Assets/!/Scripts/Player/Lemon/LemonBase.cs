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
    InsertCrystal
}

public class LemonBase : MonoBehaviour
{
    [SerializeField]private BehaviorTree speakBT;
    [SerializeField]private BehaviorTree moveBT;



    
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


        speakBT.SendEvent(name);
    }

    public void SetDestination(string eventText, GameObject targetTransObj)
    {
    
        Debug.Log("lemon in mission");
        //set behaviour disgner the target position
        SharedGameObject targetShared = targetTransObj;
        moveBT.SetVariable("MissionPosObject", targetShared);
        SharedBool islastSpeakShared = true;
        moveBT.SetVariable("InMission", islastSpeakShared);
        speakBT.SendEvent(eventText);
    }

}