using System;
using _.Scripts.Enemy;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class KeyMonsterBase : Enemy, ITaskResult
{
    public void OnTakeDamage()
    {
        bt.SendEvent("GetHurt");
    }
    public void OnGetAbiltyEvent()
    {
        Destroy(gameObject);
    }

    public void DoResult()
    {
        Destroy(gameObject);
    }
}