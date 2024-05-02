using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Enemy;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
public class BossBBase : Enemy, IDamageable
{
    [Header("Base setting")] [SerializeField]
    private int hp;

    [Header("Event")] [SerializeField] private UnityEvent onTakeDamagedEvent;
    [SerializeField] private UnityEvent onDiedEvent;

 
[ContextMenu("Send Event")]
    private void Sentevent()
    {
        bt.SendEvent("Ground_Spike");

    }

    public void OnTakeDamage(int value)
    {
        bt.SendEvent("Ground_Spike");

        onTakeDamagedEvent?.Invoke();
        hp -= value;
        if (hp <= 0) OnDied();
    }

    public void OnDied()
    {
    }
}