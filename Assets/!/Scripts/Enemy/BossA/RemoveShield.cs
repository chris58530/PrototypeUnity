using _.Scripts.Enemy.TypeA;
using BehaviorDesigner.Runtime.Tasks;

namespace @_.Scripts.Enemy.BossA
{
    [TaskCategory("BossA")]
    public class RemoveShield : BossAAction
    {
        public override void OnStart()
        {
            bossBase.isShielded = false;
            bossBase.IsShield(false);
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }

    
    }
}