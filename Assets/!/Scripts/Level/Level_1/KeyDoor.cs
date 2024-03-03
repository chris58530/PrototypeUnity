using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class KeyDoor : MonoBehaviour, ITaskResult
{
    [SerializeField] private GameObject door;
    private void Start()
    {
        this.tag = "KeyDoor";
    }

    public void DoResult()
    {
        Debug.Log("Key Door Open");
        Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(1.8f)).First().Subscribe(_ =>
        {
            door.gameObject.SetActive(false);
        }).AddTo(this);

      
    }
}