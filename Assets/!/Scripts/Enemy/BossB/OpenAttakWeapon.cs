using _.Scripts.Event;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("Hand")]
public class OpenAttakWeapon : Action
{
    public SharedGameObjectList gameObjects;
    public bool active;

    public override void OnStart()
    {
        foreach (GameObject go in gameObjects.Value)
        {
            go.SetActive(active);
        }
    }


    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }


    public override void OnEnd()
    {
    }
}