using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;


public class MoveTo : EnemyAction
{
    public float speed;
    public SharedTransform Target;

    public override void OnStart()
    {
        animator.Play("Walk");
        navMeshAgent.isStopped = false;
    }

    public override TaskStatus OnUpdate()
    {
        if (Vector3.SqrMagnitude(transform.position - Target.Value.position) < 5f)
        {
            return TaskStatus.Success;
        }

        // Vector3 tartgetPos = new Vector3(Target.Value.position.x, transform.position.y, Target.Value.position.z);
        // transform.LookAt(tartgetPos);
        // transform.position = Vector3.MoveTowards(transform.position, Target.Value.position, speed * Time.deltaTime);
        navMeshAgent.SetDestination(Target.Value.position);

        return TaskStatus.Running;
    }
}