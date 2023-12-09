using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BossABomb : MonoBehaviour
{
    [HideInInspector]public Transform target;

  

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Sword"))
            Destroy(gameObject);
        if (!other.gameObject.CompareTag("Player"))return;
            Destroy(gameObject);
    }
}