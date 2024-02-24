using System;
using BehaviorDesigner.Runtime.Tasks;
using UniRx;
using UnityEngine;

public class Knock : EnemyAction
{
    //MAKE GAMEOBJECT ADDFORCE TO MOVE ADD VELOCITY == 0 WHEN TIME END
    public float distance;
    public float time;

    private bool isSuccess;

    public override void OnStart()
    {
        isSuccess = false;
        rb.velocity = Vector3.zero;
        rb.isKinematic = false;
        rb.velocity = (transform.forward * distance);
        Observable.EveryUpdate().First().Delay(TimeSpan.FromSeconds(time)).Subscribe(_ => { isSuccess = true; })
            .AddTo(gameObject);
    }

    public override TaskStatus OnUpdate()
    {
        if (isSuccess)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
    
    }
}