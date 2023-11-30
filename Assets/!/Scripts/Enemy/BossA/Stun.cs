using _.Scripts.Enemy.TypeA;
using BehaviorDesigner.Runtime.Tasks;

namespace @_.Scripts.Enemy.BossA
{
    [TaskCategory("BossA")]
    public class Stun : BossAAction
    {
        public override void OnStart()
        {
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