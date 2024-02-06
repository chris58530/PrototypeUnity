using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class SetAttackInt : EnemyAction
{
    public SharedInt attackInt;
    public SharedInt attackInt1212;

    
    public override void OnStart()
    {
        attackInt = attackInt1212;
    }

    public override TaskStatus OnUpdate()
    {
      
        return TaskStatus.Success;
    }

    public override void OnEnd()
    {
     
    }
}