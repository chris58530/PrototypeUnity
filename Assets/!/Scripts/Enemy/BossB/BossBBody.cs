using System;
using System.Collections;
using _.Scripts.Event;
using _.Scripts.Interface;
using UnityEngine;
using Random = UnityEngine.Random;
using _.Scripts.Player;
using UniRx;
using UnityEngine.Serialization;
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
    [SerializeField] private BreakState breakState;


    [SerializeField] private GameObject shakeModel; //被打到震動的模型

    private readonly float _shakeDuration = 0.2f;


    public bool thisBreakTurn = false;
    public bool isBroken = false;
    private static bool _canDamage = true;
    private static bool _canBreak = true;

    [SerializeField] private GameObject moveTarget; //要位移的物件


    private readonly float _normalAttackMoveDistance = 0.1f; //移動距離
    private readonly float _rhinoMoveDistance = 10; //移動距離
    private readonly float _moveDuration = 0.15f; //移動時間
    [SerializeField] private HandEffect handEffect;

    private void Start()
    {
        // breaktHpImage.gameObject.SetActive(false);
        handEffect.SwitchBreakMaterial(breakState);
    }

    private void OnEnable()
    {
        EnemyActions.setCanDamagedEnemy += SetCanDamaged;
        EnemyActions.setCanBreakBossB += SetCanBreak;
    }

    private void OnDisable()
    {
        EnemyActions.setCanDamagedEnemy -= SetCanDamaged;
        EnemyActions.setCanBreakBossB -= SetCanBreak;
    }

    void SetCanDamaged(bool canDamage)
    {
        if (canDamage)
            _canDamage = true;
        else
            _canDamage = false;
    }

    void SetCanBreak(bool canBreak)
    {
        if (canBreak)
            _canBreak = true;
        else
            _canBreak = false;
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

    IDisposable _damageDisposable;

    public void OnTakeDamage(int value, Vector3 sparkleDirection, Quaternion rotation)
    {
        if (!_canDamage) return;

        SystemActions.onFrameSlow?.Invoke(0.1f);
        BossBBase.onBodyTakeDamage?.Invoke(value);
        SparkleEffect.onPlaySparkleEffect(SparkleType.Normal, sparkleDirection, rotation);
        SetCanDamaged(false);
    }

    public void OnTakeBreakableAttack()
    {
        if (!_canBreak) return;
        SetCanBreak(false);


        SystemActions.onCameraShake?.Invoke();
        SystemActions.onFrameSlow?.Invoke(0.2f); // 调用帧率减慢事件 這個會導致MoveCoroutine()無法正常運行

        if (thisBreakTurn) BossBBase.onBodyBreakDamage?.Invoke(bodyType, this); // 调用Body被打断事件
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

        // Debug.Log($" ---{this.name}----   HitBreak  : {value}");

        if (value <= 0)
        {
            breakState = BreakState.Break1;
            handEffect.SwitchBreakMaterial(breakState);
            Debug.Log($" {this.name} 已經被打爆了 ");
            

            return;
        }

        //當 isBroken = false 被玩家攻擊中
        // breaktHpImage.gameObject.GetComponent<Animator>().Play("HitBreak");
    }




    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerBase>(out var player))
        {
            player.OnTouch(this.transform);
        }
    }
}