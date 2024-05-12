using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

public class GoblinTorch : MonoBehaviour
{
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject golbinPrefab;

    [SerializeField] private bool isDark;
    [SerializeField] private GameObject spawnPoint;
    [Range(0, 5)] [SerializeField] private float spawnCoolTime;
    [SerializeField] private float triggerRadius;
    private GameObject player;
    private bool _canSpawnGolbin = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (isDark)
        {
            CloseTorchLight();
        }
    }

    private void Update()
    {
        if (!isDark) return;
        if (!_canSpawnGolbin) return;
        if (Vector3.Distance(transform.position, player.transform.position) > triggerRadius) return;
        SpawnGolbin();
    }

    public void OpenTorchLight()
    {
        fire.SetActive(true);
        isDark = false;
    }

    public void CloseTorchLight()
    {
        fire.SetActive(false);
    }

    void SpawnGolbin()
    {
        Debug.Log("Spawn Golbin");
        Instantiate(golbinPrefab, spawnPoint.transform.position, Quaternion.identity);
        Invoke(nameof(SpanwCoolDown), spawnCoolTime);
        _canSpawnGolbin = false;
    }

    void SpanwCoolDown()
    {
        _canSpawnGolbin = true;
    }
}