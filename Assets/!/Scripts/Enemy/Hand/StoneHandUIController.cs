using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("Hand")]
public class StoneHandUIController : EnemyAction
{
    public SharedGameObject stoneHandUIObject;
    public bool isOpen;

    public override void OnStart()
    {
        stoneHandUIObject.Value.SetActive(true);
        var stoneHandUI = stoneHandUIObject.Value.GetComponent<StoneHandUI>();
        stoneHandUI.ScaleShowImage(isOpen);
    }


    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}