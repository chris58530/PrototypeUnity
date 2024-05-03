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

public class BigHandController : MonoBehaviour, IBreakable
{
    [SerializeField] private GameObject moveTarget;
    [SerializeField] private HandType handType;
    [SerializeField] private float normalAttackMoveDistance;
    [SerializeField] private float rhinoMoveDistance;
    private float moveDuration = 0.15f; 

    
    void IBreakable.OnTakeAttack()
    {
        Debug.Log(" IBreakable.OnTakeAttack()");
        SystemActions.onCameraShake?.Invoke();  // 调用摄像机震动事件
        StartCoroutine(MoveCoroutine());

        // SystemActions.onFrameSlow?.Invoke(0.2f);  // 调用帧率减慢事件

   
    }

    IEnumerator MoveCoroutine()
    {
        float dir = (float)handType;  // 获取手的类型
        float moveSpeed = rhinoMoveDistance / moveDuration;  // 计算移动速度
        float timeElapsed = 0f;
        while (timeElapsed < moveDuration)
        {
            float step = moveSpeed * Time.deltaTime * dir;  // 计算移动步长
            moveTarget.transform.position += new Vector3(step, 0, 0);  // 根据计算出的步长更新位置
            timeElapsed += Time.deltaTime;
            yield return null;
        }
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