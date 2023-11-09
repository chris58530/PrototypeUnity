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
    [SerializeField] private Slider comboBar;
    public RhythmTimer rhythmTimer;
    public Animator hitDetec;

    public float dashDistance = 10f;
    public float dashDuration = 0.5f;
    private bool isDashing = false;


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
        if (Input.GetKeyDown(KeyCode.Q) && !isDashing)
        {
            Dash();
            float AniTime = rhythmTimer.AniTime();
            bool onBeat = rhythmTimer.IsOnBeat();
            if (onBeat)
            {
                comboBar.value += 1;
                Debug.Log("Perfect!" + AniTime);
                hitDetec.Play("Perfect", -1, 0);
            }
            else
            {
                comboBar.value = 0;
                Debug.Log("Missed the beat!" + AniTime);
                hitDetec.Play("Fail", -1, 0);
            }
        }


    }
    void Dash()
    {

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        LayerMask mask = 1 << LayerMask.NameToLayer("Ground");
        var targetPosition = Vector3.zero;
        if (Physics.Raycast(ray, out RaycastHit hit, 1000, mask))
        {
            Debug.DrawLine(ray.origin, hit.point);
            targetPosition = hit.point;
            targetPosition.y = transform.position.y;
            Debug.Log(hit.point);
        }

        Vector3 dashDirection = (targetPosition - transform.position).normalized;

        StartCoroutine(PerformDash(dashDirection));
    }
    IEnumerator PerformDash(Vector3 dashDirection)
    {
        isDashing = true;
        Vector3 endPosition = transform.position + dashDirection * dashDistance;

        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            transform.position = Vector3.Lerp(transform.position, endPosition, elapsedTime / dashDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isDashing = false;
    }

}
