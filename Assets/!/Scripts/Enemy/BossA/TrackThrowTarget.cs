using System;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UniRx;

namespace _.Scripts.Enemy.TypeA
{
    public class TrackThrowTarget : BossAAction
    {
        public SharedGameObject player;

        public override void OnStart()
        {
            controller.PreviewThrow(player.Value.transform);
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }

        public override void OnEnd()
        {
        }
    }
}