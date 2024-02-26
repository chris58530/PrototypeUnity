using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class BossATower : MonoBehaviour, IDamageable
{
    [SerializeField] private int hp;
    [SerializeField] private BehaviorTree bt;
    [SerializeField] private GameObject towerObj;

    private void Start()
    {
        GetComponent<Collider>().enabled = false;
    }

    public void OnTakeDamage(int value)
    {
        hp -= value;
        if (hp <= 0) OnDied();
    }

    public void OnDied()
    {
        GetComponent<BoxCollider>().enabled = false;
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

    private void OnTriggerEnter(Collider other)
    {
    }
}