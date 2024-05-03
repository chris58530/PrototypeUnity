using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Enemy;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BossBBase : Enemy, IDamageable
{
    [Header("Base setting")] [SerializeField]
    private int hpMax;
    private int currentHp;
    [SerializeField] private float[] blockHp;

    [SerializeField] private Image hpValue;

    [Header("Event")] [SerializeField] private UnityEvent onTakeDamagedEvent;
    [SerializeField] private UnityEvent onDiedEvent;
public static Action<int> onTakeDamage;
    private void Start()
    {
        //初始化血量
        currentHp = hpMax;
        UpdateHpValue();
    }

    private void OnEnable()
    {
        onTakeDamage += OnTakeDamage;
    }

    

    public void OnTakeDamage(int value)
    {
        // bt.SendEvent("Ground_Spike");

        onTakeDamagedEvent?.Invoke();
        
        currentHp -= value;
        UpdateHpValue();
        
        if (currentHp <= 0) OnDied();
    }

    public void UpdateHpValue()
    {
        hpValue.fillAmount = (float) currentHp / hpMax;

    }

    public void OnDied()
    {
        onDiedEvent?.Invoke();
    }
    
    [ContextMenu("Send Event")]
    private void Sentevent()
    {
        bt.SendEvent("Ground_Spike");

    }
    private void OnDisable()
    {
        onTakeDamage -= OnTakeDamage;
    }
}