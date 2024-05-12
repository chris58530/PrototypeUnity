using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Range(0, 5)] [SerializeField] private float spawnCoolTime;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject spawnPrefab;
    private bool _canSpawn = true;
    private void Update()
    {
        if (!_canSpawn) return;
        
        Spawn();
    }
    void Spawn()
    {
        Debug.Log("Spawn" + spawnPrefab.name);
        Instantiate(spawnPrefab, spawnPoint.transform.position, Quaternion.identity);
        Invoke(nameof(SpanwCoolDown), spawnCoolTime);
        _canSpawn = false;
    }
    void SpanwCoolDown()
    {
        _canSpawn = true;
    }
}
