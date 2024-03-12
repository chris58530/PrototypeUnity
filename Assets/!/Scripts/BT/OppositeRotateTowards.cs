using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class OppositeRotateTowards : EnemyAction
{

    public bool usePhysics2D;

    public SharedFloat rotationEpsilon = 0.5f;

    public SharedFloat maxLookAtRotationDelta = 1;

    public SharedBool onlyY;

    public SharedGameObject target;

    public SharedVector3 targetRotation;
    public Vector3 offset;


    public override TaskStatus OnUpdate()
    {
        var rotation = Target();
        // Return a task status of success once we are done rotating
        if (Quaternion.Angle(transform.rotation, rotation) < rotationEpsilon.Value)
        {
            return TaskStatus.Success;
        }

        // We haven't reached the target yet so keep rotating towards it
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, maxLookAtRotationDelta.Value);
        return TaskStatus.Running;
    }

    // Return targetPosition if targetTransform is null
    private Quaternion Target()
    {
        if (target == null || target.Value == null)
        {
            return Quaternion.Euler(targetRotation.Value);
        }

        var position = -((target.Value.transform.position + offset) - transform.position);
        if (onlyY.Value)
        {
            position.y = 0;
        }

        if (usePhysics2D)
        {
            var angle = Mathf.Atan2(position.y, position.x) * Mathf.Rad2Deg;
            return Quaternion.AngleAxis(angle, Vector3.forward);
        }

        return Quaternion.LookRotation(position);
    }

    // Reset the public variables
    public override void OnReset()
    {
        usePhysics2D = false;
        rotationEpsilon = 0.5f;
        maxLookAtRotationDelta = 1f;
        onlyY = false;
        target = null;
        targetRotation = Vector3.zero;
    }
}