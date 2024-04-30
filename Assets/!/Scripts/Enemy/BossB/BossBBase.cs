using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Enemy;
using UnityEngine;
using UnityEngine.Events;

public class BossBBase : Enemy, IDamageable
{
    [Header("Base setting")] [SerializeField]
    private int hp;

    [Header("Event")] [SerializeField] private UnityEvent onTakeDamagedEvent;
    [SerializeField] private UnityEvent onDiedEvent;
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        Debug.Log("send event");
    }

    public void OnTakeDamage(int value)
    {
        bt.SendEvent("Ground_Thorn");

        onTakeDamagedEvent?.Invoke();
        hp -= value;
        if (hp <= 0) OnDied();
    }

    public void OnDied()
    {
    }
}