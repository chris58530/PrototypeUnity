using _.Scripts.Event;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("Hand")]
public class DownAttack : EnemyAction
{
    public SharedFloat speed;
    public SharedFloat arriveDistance = 0.1f;
    public SharedGameObject target;
    private Vector3 targetPosition;
    public Vector3 offset;
    public bool shake = true;

    public override void OnStart()
    {
        targetPosition = new Vector3(transform.position.x, target.Value.transform.position.y,
            transform.position.z)+ offset;
    }


    public override TaskStatus OnUpdate()
    {
        // Return a task status of success once we've reached the target
        if (Vector3.Magnitude(transform.position - targetPosition) < arriveDistance.Value)
        {
            if (shake)
                SystemActions.onCameraShake?.Invoke();
            return TaskStatus.Success;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed.Value * Time.deltaTime);

        return TaskStatus.Running;
    }


    public override void OnEnd()
    {
    }
}