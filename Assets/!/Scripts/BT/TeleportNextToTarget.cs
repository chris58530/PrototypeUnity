using _.Scripts.Enemy;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace @_.Scripts.BT
{
    [TaskCategory("Customized")]
    public class TeleportNextToTarget : EnemyAction
    {
        public SharedGameObject player;
        public float distance;

        public override void OnStart()
        {
            Vector3 destination = player.Value.transform.forward;
            destination *= -distance;
            transform.position = destination;
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }

        public override void OnEnd()
        {
        }
    }
}