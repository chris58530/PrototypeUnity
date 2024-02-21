using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BossABomb : MonoBehaviour
{
    [SerializeField] private GameObject explode;
    public LayerMask groundLayer;

  

    private void OnTriggerEnter(Collider other)
    {
        if (groundLayer == (groundLayer | (1 << other.gameObject.layer)))
        {
            // 销毁当前脚本所附加的游戏对象
            Destroy(gameObject);
        }

     
    }

    private void OnDestroy()
    {
        var obg = Instantiate(explode, transform.position, Quaternion.identity);
        Destroy(obg.gameObject,1.2f);
    }
}