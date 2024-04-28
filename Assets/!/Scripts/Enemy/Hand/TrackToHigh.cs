using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
[TaskCategory("Hand")]

public class TrackToHigh : EnemyAction
{
    public SharedFloat speed;
    public SharedFloat high;
    public SharedFloat arriveDistance = 0.1f;
    public SharedGameObject target;
    public SharedGameObject moveObject;
    private Vector3 targetPosition;

    public override void OnStart()
    {
  
    }


    public override TaskStatus OnUpdate()
    {
        targetPosition = target.Value.transform.position;
        targetPosition.y += high.Value;

        // Return a task status of success once we've reached the target
        if (Vector3.Magnitude(moveObject.Value.transform.position - targetPosition) < arriveDistance.Value)
        {
            return TaskStatus.Success;
        }

        moveObject.Value.transform.position = Vector3.MoveTowards(moveObject.Value.transform.position, targetPosition, speed.Value * Time.deltaTime);

        return TaskStatus.Running;
    }


    public override void OnEnd()
    {
    }
}
