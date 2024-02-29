using System;
using _.Scripts.Enemy.TypeA;
using BehaviorDesigner.Runtime.Tasks;
using UniRx;

namespace _.Scripts.Enemy.BossA
{
    [TaskCategory("BossA")]
    public class YellToCleanPlace:BossAAction
    {
        public override void OnStart()
        {
            BossABomb.bossABigBombEvent?.Invoke();

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