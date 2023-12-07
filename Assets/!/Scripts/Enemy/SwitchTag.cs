using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class SwitchTag : Action
{
    public bool canHurt;
    
    public override void OnStart()
    {
        if (canHurt)
            transform.tag = "Enemy";
        else  transform.tag = "Defualt";
        
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
