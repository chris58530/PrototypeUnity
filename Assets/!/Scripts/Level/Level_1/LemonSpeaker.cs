using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemonSpeaker : MonoBehaviour, ITaskResult
{
    [SerializeField] private string[] lemonDialogText;
    [SerializeField] private bool isMission;
    private LemonBase _lemonBase;

    public void DoResult()
    {
        if (_lemonBase == null)
            _lemonBase = FindObjectOfType<LemonBase>();
        
        if (isMission)
            _lemonBase.SetMission(lemonDialogText, this.gameObject, isMission);
        else
            _lemonBase.SetSpeak(lemonDialogText, this.gameObject, isMission);
    }
}