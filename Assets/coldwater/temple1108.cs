using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class temple1108 : MonoBehaviour
{
    private float beatTimer = 0;
    private bool isOnBeat = false;
    public float beatInterval = 0.5f;
    public float tolerance = 0.05f; // 寬容區間
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Image timerImg;
    [SerializeField] private Slider timerBar;

    public RhythmTimer rhythmTimer;
    void Start()
    {
        timerBar.maxValue = beatInterval + tolerance;
        beatTimer = beatInterval + tolerance;
    }

    void Update()
    {
        beatTimer -= Time.deltaTime;
        timerBar.value = beatTimer;
        if (beatTimer <= 0)
        {
            beatTimer = beatInterval + tolerance;
        }
        Debug.Log(beatTimer);
        timerBar.value = beatTimer;

        bool onBeat = rhythmTimer.IsOnBeat();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // bool onBeat = rhythmTimer.IsOnBeat();
            if (onBeat)
            {
                Debug.Log("Perfect!");
            }
            else
            {
                Debug.Log("Missed the beat!");
            }
        }


        // beatTimer += Time.deltaTime;

        // if (beatTimer >= beatInterval)
        // {
        //     beatTimer = 0.0f;
        //     StartCoroutine(toleranceRoutine());
        // }

        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     // 玩家按下按鍵時，計算時間差
        //     float timeDifference = Mathf.Abs(beatTimer - beatInterval);

        //     if (timeDifference <= tolerance || Mathf.Abs(timeDifference - beatInterval) <= tolerance)
        //     {
        //         // 在容忍範圍內按下按鍵，表示按得準確
        //         Debug.Log("Perfect!");
        //     }
        //     else
        //     {
        //         // 按得不準確
        //         Debug.Log("Missed the beat!");
        //     }
        // }


        // if (beatTimer >= beatInterval-tolerance)
        // {
        //     StartCoroutine(toleranceRoutine());
        //     beatTimer = 0;
        // }
        // else
        // {
        //     isOnBeat = false;
        // }
    }
    // private IEnumerator toleranceRoutine()
    // {
    //     while (true)
    //     {
    //         timerImg.enabled = !timerImg.enabled;
    //         isOnBeat = true;
    //         yield return new WaitForSeconds(tolerance);
    //     }
    // }
    private IEnumerator toleranceRoutine()
    {
        while (true)
        {
            timerImg.enabled = !timerImg.enabled;
            yield return new WaitForSeconds(tolerance);
        }
    }
}
