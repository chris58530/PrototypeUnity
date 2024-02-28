using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Task;
using MagicaCloth2;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UniRx;
using Observable = UniRx.Observable;
using TimeSpan = System.TimeSpan;
using Unity.VisualScripting;

public class TaskCrystal : MonoBehaviour,ITaskObject, IDamageable
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

    public void OnTakeDamage(int value)
    {
        Debug.Log($"{this.name + " " + "on take damage"}");
        if (_currentHp > 1)
        {
            _currentHp--;
        }
        else OnDied();
    }


    public void OnDied()
    {
        if (_collider == null)
            _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
        
        isDone = true;
        TaskManager.checkTaskAction?.Invoke();
        //debugs
        GameObject obj = Instantiate(detroyCrystal, transform.position, transform.rotation);
        Destroy(obj, 5);
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
            }).AddTo(this);
    }

    public bool isDone { get; set; }
}