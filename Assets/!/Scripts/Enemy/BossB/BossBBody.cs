using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Event;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossBBody : MonoBehaviour
{
    [SerializeField] private GameObject shakeModel;
    [SerializeField] private float shakeDuration;

    private void Start()
    {
    }

    public void ShakeBody()
    {
        StartCoroutine(ShakeCoroutine());
    }

    IEnumerator ShakeCoroutine()
    {
        Vector3 originalPos = shakeModel.transform.localPosition; 
        float elapsedTime = 0f; // 添加一個變量來跟踪經過的時間
        float shakeRange = .5f; // 設定震動速度
        while (elapsedTime < shakeDuration) // 使用經過的時間作為迴圈條件
        {
            // 透過添加隨機噪音更新位置
            shakeModel.transform.localPosition = originalPos + new Vector3(Random.Range(-shakeRange, shakeRange),
                Random.Range(-shakeRange, shakeRange), 0);
        
            elapsedTime += Time.deltaTime; // 增加經過的時間
            yield return null; // 讓出控制權並繼續下一幀的執行
        }

        shakeModel.transform.localPosition = originalPos; // 重置位置到初始值
        yield return null; 

    }
}