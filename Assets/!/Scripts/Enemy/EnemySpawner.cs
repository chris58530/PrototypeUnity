using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("生成物件"), SerializeField] private GameObject spawnObj;
    [Header("生成速率"), SerializeField] private float spawnRate;

    [Header("距離玩家多遠可以開始生成"), SerializeField]
    private int spawnRange;

    private Transform _playerTrans;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 1, spawnRate);
    }

    void Spawn()
    {
        if (_playerTrans == null) _playerTrans = GameObject.FindWithTag("Player").transform;
        if (Vector3.Distance(_playerTrans.position, transform.position) > spawnRange) return;
        Instantiate(spawnObj, transform.position, transform.rotation);
    }
}