using System;
using _.Scripts.Enemy;
using UniRx;
using UnityEngine;
using UnityEngine.Events;


public class CuttleBase : Enemy, IDamageable
{
    [SerializeField] private float maxHp;
    private ReactiveProperty<float> _currentHp = new ReactiveProperty<float>();
    
    [SerializeField] private UnityEvent onTakeDamagedEvent;
    [SerializeField] private UnityEvent onStunEvent;
    [SerializeField] private UnityEvent onDiedEvent;

    [SerializeField] private GameObject cuttleModel;
  
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
        onTakeDamagedEvent?.Invoke();
        AudioManager.Instance.PlaySFX("MobInjured");

        if (_currentHp.Value <= 0)
        {
            OnDied();
            return;
        }

        _currentHp.Value -= value;
        if (_currentHp.Value <= 0)
        {
            bt.SendEvent("OnStun");
            onStunEvent?.Invoke();
        }
    }

    public void OnDied()
    {
        AutoTurnAroundDetect.onDieRemoveDetectList?.Invoke(gameObject);

        
        onDiedEvent?.Invoke();

        bt.SendEvent("OnDied");
    }

 
}