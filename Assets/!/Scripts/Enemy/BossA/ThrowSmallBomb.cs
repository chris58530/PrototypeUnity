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

        private float _startTime;
        private Vector3 _target;

        public override void OnStart()
        {
            controller.ThrowSmallBomb();
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