using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BossABomb : MonoBehaviour
{
    [SerializeField] private GameObject explode;
    [HideInInspector]public Transform target;
    public LayerMask groundLayer;

  

    private void OnTriggerEnter(Collider other)
    {
        if (groundLayer == (groundLayer | (1 << other.gameObject.layer)))
        {
            // 销毁当前脚本所附加的游戏对象
            Destroy(gameObject);
        }
        if(other.gameObject.CompareTag("Sword"))
            Destroy(gameObject);
        if (!other.gameObject.CompareTag("Player"))return;
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        var obg = Instantiate(explode, transform.position, Quaternion.identity);
        Destroy(obg.gameObject,0.2f);
    }
}