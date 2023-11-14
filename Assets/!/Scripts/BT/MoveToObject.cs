using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;


public class MoveToObject : EnemyAction
{
    public float Speed;
    public SharedFloat KeepDistance;
    public SharedGameObject Target;
    public bool trackPosY;

    public override void OnStart()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = 11;
    }

    public override TaskStatus OnUpdate()
    {
        if (Vector3.SqrMagnitude(transform.position - Target.Value.transform.position) < KeepDistance.Value)
        {
            return TaskStatus.Success;
        }

        // Vector3 tartgetPos = new Vector3(Target.Value.position.x, transform.position.y, Target.Value.position.z);
        //
        // transform.LookAt(tartgetPos);
        //
        // transform.position = Vector3.MoveTowards(transform.position, Target.Value.position, Speed * Time.deltaTime);
        navMeshAgent.SetDestination(Target.Value.transform.position);

        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        navMeshAgent.isStopped = true;
    
    }
}