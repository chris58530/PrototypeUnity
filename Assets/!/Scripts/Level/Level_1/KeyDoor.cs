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
        if (keyMonster != null)
            keyMonster.SetActive(false);
    }

    public void DoResult()
    {
        if (keyMonster != null)
            keyMonster.SetActive(true);
        //disable this 腳本
        this.GetComponent<KeyDoor>().enabled = false;

        //此物件鎖頭先撥放選轉開鎖動畫
        locker.GetComponent<Animator>().Play("Spin");
        AudioManager.Instance.PlaySFX("DoorLock");
        Debug.Log("Key Door Open");

        //撥放door的開門動畫
        Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(1.8f)).First().Subscribe(_ =>
        {
            door.GetComponent<Animator>().Play("KeyDoorOpen");
            
            //隱藏door object  
            Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(2)).First().Subscribe(_ =>
            {
                door.gameObject.SetActive(false);
            }).AddTo(this);
        }).AddTo(this);
    }
}