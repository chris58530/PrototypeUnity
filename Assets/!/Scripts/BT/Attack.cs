using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class Attack : EnemyAction
{
    public SharedGameObject AttackObject;
    public float KeepTime;
    private float _startTime;

    public override void OnStart()
    {
        //AttackObject.Value.SetActive(true);
        _startTime = Time.time;
        animator.Play("Attack");
        animator.speed = 1;
        AudioManager.Instance.PlaySFX("EnemyAttack");
        // AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        // float currentTime = stateInfo.normalizedTime * stateInfo.length;
        //
        // KeepTime = currentTime;
    }

    public override TaskStatus OnUpdate()
    {
        if (Time.time - _startTime < KeepTime)
            return TaskStatus.Running;

        return TaskStatus.Success;
    }

    public override void OnEnd()
    {
        AttackObject.Value.SetActive(false);
    }
}