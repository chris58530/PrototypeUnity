using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace @_.Scripts.Enemy.TypeA
{    [TaskCategory("BossA")]

    public class TailRotate : BossAAction
    {
        public SharedGameObject target;
        public Vector3 offset;
        public float rotateSpeed;
        private Vector3 dir;
        private Quaternion q;
        public override void OnStart()
        {
           dir = target.Value.transform.position-transform.position;
            dir.y = 0;
            q = Quaternion.LookRotation(dir);
        }
        

        public override TaskStatus OnUpdate()
        {
      
            transform.rotation = Quaternion.Slerp(transform.rotation, q, rotateSpeed);
            if (Vector3.Angle(dir, transform.forward + offset) < 0.1f)
            {
                return TaskStatus.Success;
            }
                return TaskStatus.Running;
        }

        public override void OnEnd()
        {
        }
    }
}