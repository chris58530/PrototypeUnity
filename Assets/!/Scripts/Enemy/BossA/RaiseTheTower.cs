using System;
using _.Scripts.Enemy.TypeA;
using BehaviorDesigner.Runtime.Tasks;
using UniRx;

namespace @_.Scripts.Enemy.BossA
{
    [TaskCategory("BossA")]
    public class RaiseTheTower : BossAAction
    {
        public bool isRaise;
        public override void OnStart()
        {
            controller.RaiseTheTower(isRaise);
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