using System;
using System.Collections;
using System.Collections.Generic;
using MagicaCloth2;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UniRx;
using Observable = UniRx.Observable;
using TimeSpan = System.TimeSpan;

public class Crystal : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHp;
    private int _currentHp;
    [SerializeField] private bool canRelife;
    [SerializeField] private float relifeTime;
    private Collider _collider;

    private void OnEnable()
    {
        _currentHp = maxHp;

    }

    public void OnTakeDamage(float value)
    {
        if (_currentHp > 1)
        {
            _currentHp--;
        }
        else OnDied();
    }

    public void OnKnock(Transform trans)
    {
    }

    public void OnDied()
    {
        if (_collider == null)
            _collider = GetComponent<Collider>();
        
        _collider.isTrigger = true;
        //debug
        this.GetComponent<MeshRenderer>().enabled = false;
        
        if(!canRelife)return;
        Observable.EveryUpdate()
            .Delay(TimeSpan.FromSeconds(relifeTime))
            .First()
            .Subscribe(_ =>
            {
                _currentHp = maxHp;
                this.GetComponent<MeshRenderer>().enabled = true;
                _collider.isTrigger = false;
            }).AddTo(this);
    }
}