using System;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UniRx;
using UnityEngine;

public class Move : EnemyAction
{
    public float distance;
    public float time;

    public override void OnStart()
    {
        rb.isKinematic = false;
        rb.AddForce(transform.forward * distance); 
        Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(time)).First().Subscribe(_ =>
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
        }).AddTo(this.gameObject);
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }

    public override void OnEnd()
    {
    }
}