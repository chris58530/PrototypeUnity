using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace @_.Scripts.Enemy.TypeA
{
    [TaskCategory("BossA")]
    public class GoJugglePoint : BossAAction
    {
        public SharedGameObject player;
        public SharedGameObject pointA;
        public SharedGameObject pointB;
        public SharedGameObject pointC;
        public SharedGameObject pointD;
        public SharedGameObject farthestPoint;
        public SharedFloat KeepDistance;
        public SharedFloat Speed;

        public override void OnStart()
        {
            farthestPoint = GetFarthestPoint();
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = Speed.Value;
        }

        public override TaskStatus OnUpdate()
        {
            if (Vector3.Distance(transform.position, farthestPoint.Value.transform.position) < KeepDistance.Value)
            {
                return TaskStatus.Success;
            }

            navMeshAgent.SetDestination(farthestPoint.Value.transform.position);

            return TaskStatus.Running;
        }

        public override void OnEnd()
        {
        }

        SharedGameObject GetFarthestPoint()
        {
            //choose far distance 
            var position = player.Value.transform.position;
            float distanceA = Vector3.Distance(position, pointA.Value.transform.position);
            float distanceB = Vector3.Distance(position, pointB.Value.transform.position);
            float distanceC = Vector3.Distance(position, pointC.Value.transform.position);
            float distanceD = Vector3.Distance(position, pointD.Value.transform.position);
            if (distanceA >= distanceB && distanceA >= distanceC && distanceA >= distanceD)
            {
                return pointA;
            }
            else if (distanceB >= distanceA && distanceB >= distanceC && distanceB >= distanceD)
            {
                return pointB;
            }
            else if (distanceC >= distanceA && distanceC >= distanceB && distanceC >= distanceD)
            {
                return pointC;
            }
            else
            {
                return pointD;
            }
        }
    }
}