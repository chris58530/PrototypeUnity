using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
[TaskCategory("Enemy")]

public class CollisionAttack : Action
{
    public bool canHurt;
    
    public override void OnStart()
    {
        if (canHurt)
            transform.tag = "Enemy";
        else  transform.tag = "Default";
        
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;

        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
    }
}
