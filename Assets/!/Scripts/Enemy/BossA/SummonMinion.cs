using System;
using _.Scripts.Enemy.TypeA;
using BehaviorDesigner.Runtime.Tasks;
using UniRx;

[TaskCategory("BossA")]
public class SummonMinion : BossAAction
{
    public override void OnStart()
    {
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;

    }

    public override void OnEnd()
    {
    }
}