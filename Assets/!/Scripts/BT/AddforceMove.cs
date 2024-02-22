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

    private float _timer;

    public override void OnStart()
    {
        _timer = 0;
        rb.velocity = Vector3.zero;
        rb.isKinematic = false;
        rb.AddForce(transform.forward * distance);
    }

    public override TaskStatus OnUpdate()
    {
        _timer += Time.deltaTime;
        if (_timer >= time)
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