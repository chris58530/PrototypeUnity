using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace @_.Scripts.BT
{
    [TaskCategory("Customized")]
    public class InstantiateBullet : EnemyAction
    {
        public SharedGameObject bullet;
        public SharedGameObject shootPoint;

        public override void OnStart()
        {
            GameObject.Instantiate(bullet.Value, shootPoint.Value.transform.position,
                shootPoint.Value.transform.rotation);
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