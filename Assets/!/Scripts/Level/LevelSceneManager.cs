using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Tools;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelSceneManager : Singleton<LevelSceneManager>
{
    [SerializeField] private Transform[] spawnPoint;
    public int currentSpawnNumber;

    private void Start()
    {
        currentSpawnNumber = 0;
    }

    public void ReSpawn(GameObject player)
    {
        player.transform.position = spawnPoint[currentSpawnNumber].position;

     
    }
}