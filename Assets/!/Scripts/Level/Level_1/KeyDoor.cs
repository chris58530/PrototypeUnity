using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class KeyDoor : MonoBehaviour, ITaskResult
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject locker;
    [SerializeField] private GameObject keyMonster;

    private void Start()
    {
        this.tag = "KeyDoor";
        if (keyMonster != null)
            keyMonster.SetActive(false);
    }

    public void DoResult()
    {
        if (keyMonster != null)
            keyMonster.SetActive(true);
        locker.GetComponent<Animator>().Play("Spin");
        Debug.Log("Key Door Open");
        Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(1.8f)).First().Subscribe(_ =>
        {
            door.GetComponent<Animator>().Play("KeyDoorOpen");
            Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(2)).First().Subscribe(_ =>
            {
                door.gameObject.SetActive(false);
            }).AddTo(this);
        }).AddTo(this);
    }
}