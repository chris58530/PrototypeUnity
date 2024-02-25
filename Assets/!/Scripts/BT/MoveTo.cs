using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;


public class MoveTo : EnemyAction
{
    public float speed;
    //arrive distance
    public float distance;
    public SharedGameObject Target;
    private Vector3 GoTarget;

    public override void OnStart()
    {
        // animator.Play("Walk");
        navMeshAgent.speed = speed;

        navMeshAgent.isStopped = false;
        GoTarget= new Vector3(Target.Value.transform.position.x, transform.position.y,
            Target.Value.transform.position.z);
    }

    public override TaskStatus OnUpdate()
    {
        if (Vector3.Distance(transform.position, GoTarget) < distance)
        {
            return TaskStatus.Success;
        }

        // Vector3 tartgetPos = new Vector3(Target.Value.position.x, transform.position.y, Target.Value.position.z);
        // transform.LookAt(tartgetPos);
        // transform.position = Vector3.MoveTowards(transform.position, Target.Value.position, speed * Time.deltaTime);
        navMeshAgent.SetDestination(GoTarget);
        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        navMeshAgent.isStopped = true;

    }
}