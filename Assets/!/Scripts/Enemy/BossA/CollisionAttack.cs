using _.Scripts.Enemy.TypeA;
using BehaviorDesigner.Runtime.Tasks;

namespace @_.Scripts.Enemy.BossA
{
    [TaskCategory("BossA")]
    public class CollisionAttack : BossAAction
    {
        public bool canHurt;

        public override void OnStart()
        {
            if (canHurt)
                controller.OpenAttack();
            else controller.CloseAttack();
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