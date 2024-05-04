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

        bool right = _bossBController.ReturnRightGather().Value;
        Debug.Log("Right: " + right);
        bool left = _bossBController.ReturnLeftGather().Value;
        Debug.Log("Left: " + left);
        if (_bossBController.ReturnRightGather().Value && _bossBController.ReturnLeftGather().Value)
        {
            return TaskStatus.Success;
        }
         return TaskStatus.Failure;
    }
}