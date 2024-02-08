
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;


public class EnableNavMeshAgent : EnemyAction
{
    public SharedGameObject target;
    public bool enabled;
    public override void OnStart()
    {
        if (target.Value.TryGetComponent(out NavMeshAgent nav))
        {
            nav.enabled = enabled;
        }else Debug.Log($"BT : {this.gameObject.name} have no NavMeshAgent");
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }

    public override void OnEnd()
    {
    }
}
