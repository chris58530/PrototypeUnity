using System;
using System.Collections;
using System.Collections.Generic;
using MagicaCloth2;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UniRx;
using Observable = UniRx.Observable;
using TimeSpan = System.TimeSpan;
using Unity.VisualScripting;

public class Crystal : TaskObject, IDamageable
{
    [SerializeField] private int maxHp;
    private int _currentHp;
    [SerializeField] private bool canRelife;
    [SerializeField] private float relifeTime;
    [SerializeField] private GameObject detroyCrystal;
    private Collider _collider;


    private void OnEnable()
    {
        _currentHp = maxHp;
    }

    public void OnTakeDamage(float value)
    {
        Debug.Log("1234569");
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
        isDone = true;
        _collider.isTrigger = true;
        //debugs
        GameObject obj = Instantiate(detroyCrystal, transform.position, transform.rotation);
        Destroy(obj, 4);
        this.gameObject.active = false;

        if (!canRelife) return;
        Observable.EveryUpdate()
            .Delay(TimeSpan.FromSeconds(relifeTime))
            .First()
            .Subscribe(_ =>
            {
                _currentHp = maxHp;
                this.gameObject.active = true;
                _collider.isTrigger = false;
                isDone = false;
            }).AddTo(this);
    }
}