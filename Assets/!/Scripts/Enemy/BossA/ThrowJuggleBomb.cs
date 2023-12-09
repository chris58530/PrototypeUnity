using System;
using _.Scripts.Enemy.TypeA;
using BehaviorDesigner.Runtime.Tasks;
using UniRx;
using UnityEngine;
[TaskCategory("BossA")]
public class ThrowJuggleBomb : BossAAction
{
 
        private float _startTime;
        private Vector3 _target;

        public override void OnStart()
        {
            var position = player.position;
            _target = new Vector3(position.x, position.y, position.z);
            controller.ThrowJuggleBomb(_target);
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;


        }

        public override void OnEnd()
        {
        }
  
}
