using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Event;
using _.Scripts.Interface;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using _.Scripts.Player;
using Unity.VisualScripting;
using UnityEngine.UI;

public enum BreakState
{
    NoBreak,
    Break1,
    Break2,
}

public enum BodyType
{
    Head = 0,
    RightHand = -1,
    LeftHand = 1
}

public class BossBBody : MonoBehaviour, IDamageable, IBreakable
{
    [SerializeField] private BodyType bodyType;


    [SerializeField] private GameObject shakeModel; //被打到震動的模型

    private readonly float _shakeDuration = 0.2f;

    // [SerializeField] private float breakValue; //護頓值
    [SerializeField] private Image breaktHpImage; //護頓血條

    public bool canBreak;
    public bool isBroken;

    [SerializeField] private GameObject moveTarget; //要位移的物件


    private readonly float _normalAttackMoveDistance = 0.1f; //移動距離
    private readonly float _rhinoMoveDistance = 10; //移動距離
    private readonly float _moveDuration = 0.15f; //移動時間

    private void Start()
    {
        // breaktHpImage.gameObject.SetActive(false);
    }

    public void ShakeBody()
    {
        StartCoroutine(ShakeCoroutine());
        Debug.Log("Shake Body");
    }

    IEnumerator ShakeCoroutine()
    {
        Vector3 originalPos = shakeModel.transform.localPosition;
        float elapsedTime = 0f; // 添加一個變量來跟踪經過的時間
        float shakeRange = .5f; // 設定震動速度
        while (elapsedTime < _shakeDuration) // 使用經過的時間作為迴圈條件
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

    public void OnTakeDamage(int value)
    {
        Debug.Log("BossBBody Take Damage");
        BossBBase.onBodyTakeDamage?.Invoke(value);
    }

    void IBreakable.OnTakeAttack()
    {
        Debug.Log(" IBreakable.OnTakeAttack()");
        SystemActions.onCameraShake?.Invoke(); // 调用摄像机震动事件
        
        if (canBreak) BossBBase.onBodyBreakDamage?.Invoke(bodyType, this); // 调用Body被打断事件

       

        // SystemActions.onFrameSlow?.Invoke(0.2f);  // 调用帧率减慢事件 這個會導致MoveCoroutine()無法正常運行
    }

    public void OnPush()
    {
        if (bodyType == BodyType.Head) return;
        StartCoroutine(MoveCoroutine());
    }

    IEnumerator MoveCoroutine()
    {
        float dir = (float)bodyType; // 获取手的类型
        float moveSpeed = _rhinoMoveDistance / _moveDuration; // 计算移动速度
        float timeElapsed = 0f;
        while (timeElapsed < _moveDuration)
        {
            float step = moveSpeed * Time.deltaTime * dir; // 计算移动步长
            moveTarget.transform.position += new Vector3(step, 0, 0); // 根据计算出的步长更新位置
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    public void OnDied()
    {
    }

    public void HitBreak(int value)
    {
        if (value <= 0)
        {
            breaktHpImage.gameObject.SetActive(false);
            Debug.Log($" {this.name} 已經被打爆了 ");
            return;
        }

        //當 isBroken = false 被玩家攻擊中
        Debug.Log($" {this.name}HitBreak {value}");
        breaktHpImage.gameObject.GetComponent<Animator>().Play("HitBreak");
        breaktHpImage.fillAmount = (float)value / 3;
    }

    public void OpenBreak() //護頓首次登場
    {
        breaktHpImage.color = Color.red;
        breaktHpImage.gameObject.GetComponent<Animator>().Play("OpenBreak");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerBase>(out var player))
        {
            player.OnTouch(this.transform);
        }
    }
}