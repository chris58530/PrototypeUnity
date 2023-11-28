using System;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UniRx;

namespace _.Scripts.Enemy.TypeA
{
    public class TrackThrowTarget : BossAAction
    {
        public SharedTransform player;

        public override void OnStart()
        {
            controller.PreviewThrow(player.Value);
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