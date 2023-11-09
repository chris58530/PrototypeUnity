using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class temple1108 : MonoBehaviour
{
    private float beatTimer = 0;//計時器
    private bool isOnBeat = false;//節點布林
    public float beatInterval = 0.45f;//節奏長度
    public float tolerance = 0.05f; // 寬容區間
    // [SerializeField] private Image timerImg;//節奏中心圖
    // [SerializeField] private Slider timerBar;//節奏節點

    public RhythmTimer rhythmTimer;
    public Animator hitDetec;


    void Start()
    {

        // //節奏節點的總值為:節奏長度+寬容區間
        // timerBar.maxValue = beatInterval + tolerance;
        // //計時器同理
        // beatTimer = beatInterval + tolerance;
    }

    void Update()
    {
        //計時器運作
        beatTimer -= Time.deltaTime;
        //計時器同步至Bar
        // timerBar.value = beatTimer;
        //計時器歸零迴圈
        // if (beatTimer <= 0)
        // {
        //     beatTimer = beatInterval + tolerance;
        //     Debug.Log("reset!" + beatTimer);
        // }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Debug.Log("Space!" + beatTimer);
            float AniTime = rhythmTimer.AniTime();
            bool onBeat = rhythmTimer.IsOnBeat();
            if (onBeat)
            {
                Debug.Log("Perfect!" + AniTime);
                hitDetec.Play("Perfect", -1, 0);
            }
            else
            {
                Debug.Log("Missed the beat!" + AniTime);
                hitDetec.Play("Fail", -1, 0);
            }
        }


    }

}
