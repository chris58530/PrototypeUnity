using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossABomb : MonoBehaviour
{
    [SerializeField] private GameObject explode;
    public LayerMask groundLayer;


    private bool _readyToCollision;

    private void Start()
    {
        Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(0.1f)).First().Subscribe(_ => _readyToCollision = true)
            .AddTo(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_readyToCollision) return;
        if (groundLayer == (groundLayer | (1 << other.gameObject.layer)))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Vector3 offset = new Vector3(transform.position.x, 0, transform.position.z);
        var obg = Instantiate(explode, offset, Quaternion.identity);

        // Destroy(obg.gameObject,1.2f);
    }
}