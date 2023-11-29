using System;
using _.Scripts.Enemy.TypeA;
using BehaviorDesigner.Runtime.Tasks;
using UniRx;
using UnityEngine;

namespace @_.Scripts.Enemy.BossA
{
    [TaskCategory("BossA")]
    public class ThrowSmallBomb : BossAAction
    {
        public float throwQuantity;

        private float _startTime;
        private Vector3 _target;
        private bool _success = false;

        public override void OnStart()
        {
            _target = new Vector3(player.position.x, player.position.y, player.position.z);
            controller.ThrowSmallBomb(_target);
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;


            return TaskStatus.Running;
        }

        public override void OnEnd()
        {
        }
    }
}