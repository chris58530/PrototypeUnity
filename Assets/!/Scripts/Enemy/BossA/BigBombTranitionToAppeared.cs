using _.Scripts.Enemy.TypeA;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Cinemachine.Editor;
using UnityEngine;

[TaskCategory("BossA")]
public class BigBombTranitionToAppeared : BossAAction
{
    public bool isAppeared;
    public override void OnStart()
    {
        bossBase.BigBombTransition(isAppeared);
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }

    public override void OnEnd()
    {
    }
}