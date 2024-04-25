using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class ObjectMoveTowards :Action
{
    [UnityEngine.Tooltip("The speed of the agent")]
    public SharedFloat speed;
    [UnityEngine.Tooltip("The agent has arrived when the magnitude is less than this value")]
    public SharedFloat arriveDistance = 0.1f;
    [UnityEngine.Tooltip("Should the agent be looking at the target position?")]
    public SharedBool lookAtTarget = true;
    [UnityEngine.Tooltip("Max rotation delta if lookAtTarget is enabled")]
    public SharedFloat maxLookAtRotationDelta;
    [UnityEngine.Tooltip("The GameObject that the agent is moving towards")]
    public SharedGameObject target;
    [UnityEngine.Tooltip("The GameObject that is moved")]

    public SharedGameObject moveObject;
    [UnityEngine.Tooltip("If target is null then use the target position")]
    public SharedVector3 targetPosition;
    public SharedVector3 addPosition;

    public override TaskStatus OnUpdate()
    {
        var position = Target();
        // Return a task status of success once we've reached the target
        if (Vector3.Magnitude(moveObject.Value.transform.position - position) < arriveDistance.Value) {
            return TaskStatus.Success;
        }
        // We haven't reached the target yet so keep moving towards it
        moveObject.Value.transform.position = Vector3.MoveTowards(moveObject.Value.transform.position, 
            position, speed.Value * Time.deltaTime);
        if (lookAtTarget.Value && (position - moveObject.Value.transform.position).sqrMagnitude > 0.01f) {
            moveObject.Value.transform.rotation = Quaternion.RotateTowards(moveObject.Value.transform.rotation, 
                Quaternion.LookRotation(position - moveObject.Value.transform.position), maxLookAtRotationDelta.Value);
        }
        return TaskStatus.Running;
    }

    // Return targetPosition if targetTransform is null
    private Vector3 Target()
    {
        if (target == null || target.Value == null) {
            return targetPosition.Value;
        }
        return target.Value.transform.position +addPosition.Value;
    }

    // Reset the public variables
    public override void OnReset()
    {
        arriveDistance = 0.1f;
        lookAtTarget = true;
    }
}
