using System;
using _.Scripts.Enemy;
using BehaviorDesigner.Runtime;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class KeyMonsterBase : Enemy, ITaskResult
{
    [SerializeField] private bool canAttack;

    public void Start()
    {
        bt.SetVariable("CanAttack",(SharedBool)canAttack);
    }

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