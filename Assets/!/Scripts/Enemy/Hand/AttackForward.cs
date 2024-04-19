using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("Hand")]
public class AttackForward : EnemyAction
{
    public SharedFloat speed;
    public SharedFloat distance;
    private Vector3 targetPosition;

    public override void OnStart()
    {
        targetPosition = transform.forward;

 
    }


    public override TaskStatus OnUpdate()
    {
        // Return a task status of success once we've reached the target
        if (Vector3.Magnitude(transform.position - targetPosition) < 0.1f)
        {
            return TaskStatus.Success;
        }
        Vector3 moveDirection = new Vector3(targetPosition.x, 0, targetPosition.z).normalized;

        transform.position += moveDirection * speed.Value * Time.deltaTime;
        // transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed.Value * Time.deltaTime);

        return TaskStatus.Running;
    }


    public override void OnEnd()
    {
    }
}