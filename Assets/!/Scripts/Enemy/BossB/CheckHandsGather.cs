using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class CheckHandsGather : Conditional
{
    BossBController _bossBController;

    public override void OnStart()
    {
        _bossBController = UnityEngine.GameObject.Find("BossB_Base").GetComponent<BossBController>();
        

    }

    public override TaskStatus OnUpdate()
    {

        if (_bossBController.ReturnRightGather().Value && _bossBController.ReturnLeftGather().Value)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}