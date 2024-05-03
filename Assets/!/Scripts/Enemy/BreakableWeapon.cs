using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Enemy.Hand;
using _.Scripts.Interface;
using UnityEngine;
using BehaviorDesigner.Runtime;


public class BreakableWeapon : MonoBehaviour
{
    [SerializeField] private BehaviorTree bt;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IBreakable>(out IBreakable target))
        {
            if(other.GetComponent<SmallHandBase>())return;
            target.OnTakeAttack();
            bt.SendEvent("HitBody");

        }
        if (other.gameObject.TryGetComponent<BossBBody>(out var bossBBody))
        {
            bossBBody.ShakeBody();
            bt.SendEvent("HitBody");
        }
    }
}