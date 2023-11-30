using System;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UniRx;
using UnityEngine;

namespace _.Scripts.Enemy.TypeA
{
    [TaskCategory("BossA")]
    public class ThrowBomb : BossAAction
    {
        public float trackTime;

        private float _startTime;
        private Vector3 _target;
        private bool _success;

        public override void OnStart()
        {
            _success = false;
            controller.PreviewThrow(player);
            Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(trackTime)).First().Subscribe(_ => { _success = true; })
                .AddTo(this.gameObject);
        }

        public override TaskStatus OnUpdate()
        {
            if (_success)
            {
                _target = new Vector3(player.position.x, player.position.y, player.position.z);
                Debug.Log($"throw to {_target}");
                return TaskStatus.Success;
            }

            return TaskStatus.Running;
        }

        public override void OnEnd()
        {
            controller.ThrowBomb(_target);
        }
    }
}