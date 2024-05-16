using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Event;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class EliteHandSpawner : MonoBehaviour
{
    [SerializeField] private GameObject eliteHand;
    [SerializeField] private Transform spawnPoint;

    private void OnEnable()
    {
        SystemActions.onPlayerRespawn += OpenCollider;
    }

    private void OnDisable()
    {
        SystemActions.onPlayerRespawn -= OpenCollider;
    }

    void OpenCollider()
    {
        GetComponent<Collider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<Collider>().enabled = false;
            Instantiate(eliteHand, spawnPoint.position, Quaternion.Euler(360, 180, 0));
            FindObjectOfType<EliteHandBase>().OpenBT();
        }
    }
}