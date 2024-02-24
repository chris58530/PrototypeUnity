using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class WithVecterDistance : Conditional
{
    public SharedGameObject targetObject;
    public SharedFloat magnitude = 5;


    public override void OnStart()
    {
        if (targetObject.Value == null)
        {
            targetObject.Value= GameObject.FindWithTag("Player");
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (Vector3.Magnitude(transform.position - targetObject.Value.transform.position) < magnitude.Value)
            return TaskStatus.Success;
        return TaskStatus.Failure;
    }

    public override void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (Owner == null || magnitude == null)
        {
            return;
        }

        var oldColor = UnityEditor.Handles.color;
        UnityEditor.Handles.color = Color.red;
        var transform1 = Owner.transform;
        UnityEditor.Handles.DrawWireDisc(transform1.position, transform1.up, magnitude.Value);
        UnityEditor.Handles.color = oldColor;
#endif
    }
}