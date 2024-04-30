using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Event;
using _.Scripts.Interface;
using _.Scripts.Player;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

public enum HandType
{
    Right = -1,
    Left = 1
}

public class BigHandController : MonoBehaviour, IBreakable, IDamageable
{
    [SerializeField] private GameObject moveTarget;
    [SerializeField] private HandType handType;
    [SerializeField] private float normalAttackMoveDistance;
    [SerializeField] private float rhinoMoveDistance;
    private float moveDuration = 0.05f; 
    public bool canBeBreak;

    private void OnEnable()
    {
        canBeBreak = true;
    }

    void IBreakable.OnTakeAttack()
    {
        if (!canBeBreak) return;
        
        canBeBreak = false;
        SystemActions.onCameraShake?.Invoke();

        float dir = (float)handType;
        float moveSpeed = rhinoMoveDistance / moveDuration;
        Observable.EveryUpdate()
            // 使用 TakeUntil 方法来设置结束条件
            .TakeUntil(Observable.Timer(TimeSpan.FromSeconds(moveDuration)))
            .Subscribe(_ =>
            {
                // 在每帧更新位置
                float step = moveSpeed * Time.deltaTime * dir; // 根据 handType 控制移动方向
                moveTarget.transform.position += new Vector3(step, 0, 0);
            })
            .AddTo(this); // 确保适当的清理
    }


    void IDamageable.OnTakeDamage(int value)
    {
        if (!canBeBreak) return;

        Debug.Log("Got Normal Attack");

        float dir = (float)handType;
        moveTarget.transform.position += new Vector3(normalAttackMoveDistance * dir, 0, 0);
    }

    public void OnDied()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerBase>(out var player))
        {
            player.OnTouch(this.transform);
            Debug.Log("touch palter");
        }
    }
}