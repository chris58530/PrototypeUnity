using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class TypeABomb : MonoBehaviour
{
    public Transform target;

    private void OnEnable()
    {
    }

    private void Update()
    {
        //增加曲線
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            Destroy(gameObject);
    }
}