using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
[TaskCategory("Hand")]

public class MoveToHigh : EnemyAction
{
    public SharedFloat speed;
    public SharedFloat high;
    public SharedFloat arriveDistance = 0.1f;
    public SharedGameObject target;
    private Vector3 targetPosition;

    public override void OnStart()
    {
        targetPosition = target.Value.transform.position;
        targetPosition.y += high.Value;
    }


    public override TaskStatus OnUpdate()
    {
        // Return a task status of success once we've reached the target
        if (Vector3.Magnitude(transform.position - targetPosition) < arriveDistance.Value)
        {
            return TaskStatus.Success;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed.Value * Time.deltaTime);

        return TaskStatus.Running;
    }


    public override void OnEnd()
    {
    }
}