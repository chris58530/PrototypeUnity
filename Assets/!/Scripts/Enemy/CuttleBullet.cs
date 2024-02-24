using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class CuttleBullet : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private float _speed;

    void Start()
    {
        // 获取玩家的初始位置
        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player != null)
        {
            // 计算朝向玩家位置的方向
            Vector3 direction = (_player.transform.position - transform.position).normalized;
            direction.y = 0;
            // 将对象朝向设定为朝向玩家的方向
            transform.forward = direction;
        }
    }

    void Update()
    {

        Vector3 velocity = transform.forward * (_speed * Time.deltaTime);

        transform.position += velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}