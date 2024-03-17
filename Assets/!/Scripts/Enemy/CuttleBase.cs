using System;
using _.Scripts.Enemy;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
public class CuttleBase : Enemy, IDamageable
{

    [SerializeField] private float maxHp;
    private ReactiveProperty<float> _currentHp = new ReactiveProperty<float>();

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        _currentHp.Value = maxHp;
    }

    public void OnTakeDamage(int value)
    {
        bt.SendEvent("GetHurt");
        _currentHp.Value -= value;

        if (_currentHp.Value <= 0) OnDied();
    }
    public void OnDied()
    {
        bt.SendEvent("OnDied");
    }
   
}
