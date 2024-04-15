using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UniRx;

public class BossATower : MonoBehaviour, IDamageable
{
    [SerializeField] private int hp;
    [SerializeField] private BehaviorTree bt;
    [SerializeField] private GameObject towerObj;

    [Header("Set Self Effect")] [SerializeField]
    private Material EmssionMat;

    [SerializeField] private Material OringinMat;
    [SerializeField] private Renderer[] _renderers;

    private void Start()
    {
        GetComponent<Collider>().enabled = false;
    }

    public void OnTakeDamage(int value)
    {
        hp -= value;
        SetEmission();
        if (hp <= 0) OnDied();
    }

    public void OnDied()
    {
        GetComponent<Collider>().enabled = false;
        //set Boss A parent to null 
        if (transform.childCount > 0)
            transform.GetChild(0).transform.parent = null;
        //call BT send event "DestroyTower"
        if (bt != null) bt.SendEvent("DestroyTower");
        else
        {
            Debug.LogWarning("BossA_Tower have no Bt");
        }
    }

    public void SetEmission()
    {
        for (int i = 0; i < _renderers.Length; i++)
        {
            _renderers[i].material = EmssionMat;
        }

        Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(0.2f)).First().Subscribe(_ =>
        {
            for (int i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].material = OringinMat;
            }
        }).AddTo(this);
    }

    private void OnTriggerEnter(Collider other)
    {
    }
}