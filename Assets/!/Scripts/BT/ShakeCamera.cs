using _.Scripts.Event;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
public class ShakeCamera : Action
{
   

    public override void OnStart()
    {
        SystemActions.onCameraShake?.Invoke();
    }

    public override TaskStatus OnUpdate()
    {
        
            return TaskStatus.Success;

    
    }

    public override void OnEnd()
    {

    }
}
