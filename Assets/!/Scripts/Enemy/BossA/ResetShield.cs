using System;
using _.Scripts.Enemy.TypeA;
using BehaviorDesigner.Runtime.Tasks;
using UniRx;
using UnityEngine;

namespace @_.Scripts.Enemy.BossA
{
    [TaskCategory("BossA")]
    public class ResetShield : BossAAction
    {
       

        public override void OnStart()
        {
            bossBase.isShielded = true;
            controller.ResetShield();
            bossBase.IsShield(true);
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
}