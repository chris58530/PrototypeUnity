using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LemonSpeaker : MonoBehaviour, ITaskResult
{
    [SerializeField] private string lemonDialogEventText;
    [SerializeField] private bool AddDestination;
    private LemonBase _lemonBase;
    

    public void DoResult()
    {
        if (_lemonBase == null)
            _lemonBase = FindObjectOfType<LemonBase>();

        if (AddDestination)
            _lemonBase.SetDestination(lemonDialogEventText, this.gameObject);
  
    }

  
}