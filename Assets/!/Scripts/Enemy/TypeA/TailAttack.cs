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
        public float tailDistance;

        private bool _success;

        public override void OnStart()
        {
            navMeshAgent.enabled = false;
            _success = false;

            if (Vector3.Distance(transform.position, player.position) > tailDistance)
            {
                controller.JumpToPlayer(navMeshAgent,player,jumpTime);
                Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(1f))
                    .First()
                    .Subscribe(_ => { _success = true; })
                    .AddTo(this.gameObject);
            }
            else
                Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(1f))
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
            navMeshAgent.enabled = true;
            animator.Play("TailAttack");
            controller.ShakeTail();
        }
    }
}