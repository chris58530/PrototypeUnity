using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhythmTimer : MonoBehaviour
{
    public bool OnBeat;
    private float beatTimer2 = 0;//計時器
    public void Update()
    {
        beatTimer2 += Time.deltaTime;

    }
    void SetBeatBoolFalse()
    {
        OnBeat = false;
        // Debug.Log("結束判定點V" + beatTimer2);
    }
    void SetBeatBoolTrue()
    {
        OnBeat = true;
        // Debug.Log("開始判定點A" + beatTimer2);
    }
    void ResetAni()
    {
        // Debug.Log("拍點,動畫計時器:" + beatTimer2);
        beatTimer2 = 0;
    }
    public bool IsOnBeat()
    {
        return OnBeat;
    }
    public float AniTime()
    {

        return beatTimer2;
    }
}
