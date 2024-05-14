using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GoblinTorch : MonoBehaviour
{
    [SerializeField] private GameObject shakeModel;
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject golbinPrefab;
    [SerializeField] private GameObject darkBlock;

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
        darkBlock.SetActive(false);
        isDark = false;
        StopCoroutine(ShakeCoroutine());
    }

    public void CloseTorchLight()
    {
        fire.SetActive(false);
        StartCoroutine(ShakeCoroutine());

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
    IEnumerator ShakeCoroutine()
    {
        Vector3 originalPos = shakeModel.transform.localPosition;
        float shakeRange = .5f; // 設定震動速度
        while (isDark) // 使用經過的時間作為迴圈條件
        {
            if (Vector3.Distance(transform.position, player.transform.position) < triggerRadius)
            {
                // 透過添加隨機噪音更新位置
                shakeModel.transform.localPosition = originalPos + new Vector3(Random.Range(-shakeRange, shakeRange),
                    Random.Range(-shakeRange, shakeRange), 0);
            }
            yield return null; // 讓出控制權並繼續下一幀的執行
        }

        shakeModel.transform.localPosition = originalPos; // 重置位置到初始值
        yield return null;
    }
}