using UnityEngine;

public class TimeControl : MonoBehaviour
{
    // 减缓时间的倍数
    public float slowdownFactor = 0.25f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            // 按下数字键盘0时减缓时间
            Time.timeScale = slowdownFactor;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            // 松开数字键盘0时恢复正常时间
            Time.timeScale = 1f;
        }
    }
}