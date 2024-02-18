using System;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UniRx;
using UnityEngine;

public class AddforceMove : EnemyAction
{
    //MAKE GAMEOBJECT ADDFORCE TO MOVE ADD VELOCITY == 0 WHEN TIME END
    public float distance;
    public float time;

    private bool _isSuccess;

    public override void OnStart()
    {
        rb.isKinematic = false;
        rb.AddForce(transform.forward * distance);
        Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(time)).First().Subscribe(_ =>
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            _isSuccess = true;
        }).AddTo(this.gameObject);
    }

    public override TaskStatus OnUpdate()
    {
        if (_isSuccess)
            return TaskStatus.Success;
        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
    }
}