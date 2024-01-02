using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace _.Scripts.Enemy.TypeA
{
    public class WithDistance : Conditional
    {
        public SharedGameObject player;
        public SharedTransform thisTransform;
        public float distance;

        public override void OnStart()
        {
        }

        public override TaskStatus OnUpdate()
        {
            if (Vector3.Distance(transform.position, player.Value.transform.position) <= distance)
                return TaskStatus.Success;
            else
            {
                return TaskStatus.Failure;

            }
        }

        public override void OnEnd()
        {
        }
    }
}