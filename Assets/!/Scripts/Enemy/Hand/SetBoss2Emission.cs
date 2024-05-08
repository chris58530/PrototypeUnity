using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("Hand")]
public class SetBoss2Emission : Action
{
    public SharedGameObject target;
    public bool isOn;
    HandEffect _handEffect;

    public override void OnStart()
    {
        _handEffect = target.Value.GetComponent<HandEffect>();
        _handEffect.SetMaterialsEmission(isOn);

    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}