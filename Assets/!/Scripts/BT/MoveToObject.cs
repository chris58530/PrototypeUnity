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
        animator.Play("Walk");
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = 7;
        animator.speed = 2;
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
        Vector3 targetPos = Target.Value.transform.position;
        Vector3 dir = targetPos - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(dir.normalized, transform.up);
        transform.rotation = toRotation;
    }
}