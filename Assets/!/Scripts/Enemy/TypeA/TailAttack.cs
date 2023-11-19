using System;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UniRx;
using UnityEngine;

namespace _.Scripts.Enemy.TypeA
{
    public class TailAttack : TypeAAction
    {
        public float jumpTime;
        public float waitTime;
        public float tailDistance;

        private bool _success;

        public override void OnStart()
        {
            navMeshAgent.isStopped = true;
            _success = false;

            if (Vector3.Distance(transform.position, player.position) > tailDistance)
            {
                controller.JumpToPlayer(player,jumpTime);
                Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(waitTime))
                    .First()
                    .Subscribe(_ => { _success = true; })
                    .AddTo(this.gameObject);
            }
            else
                Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(waitTime))
                    .First()
                    .Subscribe(_ => { _success = true; })
                    .AddTo(this.gameObject);
        }

        public override TaskStatus OnUpdate()
        {
            if (_success)
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Running;
        }

        public override void OnEnd()
        {
            controller.ShakeTail();
        }
    }
}