using _.Scripts.Enemy.TypeA;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine.AI;
using UnityEngine;


[TaskCategory("Lemon")]
public class LemonMoveTowards : Action
{
    private SharedFloat _speed;

    public SharedFloat arriveDistance = 0.1f;

    public SharedBool lookAtTarget = true;

    public SharedFloat maxLookAtRotationDelta;

    public SharedGameObject target;
    public Vector3 offset;


    public override TaskStatus OnUpdate()
    {
        var position = Target();
        var distance = Vector3.Magnitude(transform.position - position);
        _speed = distance * 1f;
     
        if (distance < arriveDistance.Value)
        {
            return TaskStatus.Success;
        }

        transform.position = Vector3.MoveTowards(transform.position, position, _speed.Value* Time.deltaTime);
        
        if (lookAtTarget.Value && (position - transform.position).sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                Quaternion.LookRotation(position - transform.position), maxLookAtRotationDelta.Value);
        }

        return TaskStatus.Running;
    }

    private Vector3 Target()
    {
        return target.Value.transform.position + offset;
    }

    // Reset the public variables
    public override void OnReset()
    {
        arriveDistance = 0.1f;
        lookAtTarget = true;
    }
}