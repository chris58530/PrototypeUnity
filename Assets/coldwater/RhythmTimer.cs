using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhythmTimer : MonoBehaviour
{
    public float beatInterval = 5f; // 節奏的時間間隔
    public float tolerance = 1f; // 時間容忍度，用於判斷節奏準確度

    private float beatTimer = 0.0f;

    [SerializeField] private Image timerImg;    
    // 計算是否在節奏點上的函數
    private bool isTrue = false;

    private void Start()
    {
        StartCoroutine(CycleBool());
    }

    private IEnumerator CycleBool()
    {
        while (true)
        {
            isTrue = false; // 設置為 false
            timerImg.enabled = !timerImg.enabled;
            yield return new WaitForSeconds(beatInterval); // 等待 5 秒

            
            isTrue = true; // 設置為 true
            timerImg.enabled = !timerImg.enabled;
            yield return new WaitForSeconds(tolerance); // 等待 1 秒
        }
    }
    public bool IsOnBeat()
    {
        return isTrue;
    }
}
