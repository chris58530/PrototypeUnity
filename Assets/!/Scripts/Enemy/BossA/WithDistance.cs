using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace _.Scripts.Enemy.TypeA
{
    public class WithDistance : Conditional
    {
        public SharedGameObject player;
        public float distance;

        public override void OnStart()
        {
        }

        public override TaskStatus OnUpdate()
        {
            if (Vector3.Magnitude(transform.position - player.Value.transform.position) < distance)
                return TaskStatus.Success;
            return TaskStatus.Failure;
        }

        public override void OnEnd()
        {
        }
    }
}