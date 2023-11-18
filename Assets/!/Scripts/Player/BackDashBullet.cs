using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackDashBullet : MonoBehaviour
{
    private float _lifetime = 2;
    private float _speed = 100;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        rb.AddForce(transform.forward * _speed, ForceMode.Impulse);
        Destroy(gameObject, _lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IMarkable>(out var markable))
        {
            markable.Mark();
            Destroy(gameObject);
        }
    }
}