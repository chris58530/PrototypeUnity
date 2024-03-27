using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LemonSpeaker : MonoBehaviour, ITaskResult
{
    [SerializeField] private string[] lemonDialogText;
    [SerializeField] private bool AddDestination;
    private LemonBase _lemonBase;
    

    public void DoResult()
    {
        if (_lemonBase == null)
            _lemonBase = FindObjectOfType<LemonBase>();

        if (AddDestination)
            _lemonBase.SetDestination(lemonDialogText, this.gameObject, AddDestination);
        else
            _lemonBase.SetSpeak(lemonDialogText);
    }

  
}