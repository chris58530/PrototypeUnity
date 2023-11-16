using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class Attack : EnemyAction
{
    public SharedGameObject AttackObject;
    public SharedGameObject Target;

    public float KeepTime;
    private float _startTime;

    public override void OnStart()
    {
        //AttackObject.Value.SetActive(true);

        _startTime = Time.time;
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX("EnemyAttack");
        Vector3 targetPos = Target.Value.transform.position;
        Vector3 dir = targetPos - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(dir.normalized, transform.up);
        transform.rotation = toRotation;
        rb.AddForce(dir*100);
    }

    public override TaskStatus OnUpdate()
    {
        if (Time.time - _startTime < KeepTime)
        {
            return TaskStatus.Running;
        }
        return TaskStatus.Success;
    }

    public override void OnEnd()
    {
        rb.velocity = Vector3.zero;
        // AttackObject.Value.SetActive(false);
    }
}