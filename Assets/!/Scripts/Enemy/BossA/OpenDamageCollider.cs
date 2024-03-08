using _.Scripts.Enemy.TypeA;
using BehaviorDesigner.Runtime.Tasks;

namespace @_.Scripts.Enemy.BossA
{
    [TaskCategory("BossA")]
    public class OpenDamageCollider : BossAAction
    {
        public bool tail;
        public bool head;
        public bool canHurt;

        public override void OnStart()
        {
            if (tail)
                controller.OpenTailAttack(canHurt);
            if (head)
                controller.OpenHeadAttack(canHurt);
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }
    }
}