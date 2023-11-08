using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


    public class PreviewAttack : EnemyAction
    {
        public SharedGameObject PreviewObject;
        public float KeepTime;
        private float _startTime;

        public override void OnStart()
        {
            PreviewObject.Value.SetActive(true);
            _startTime = Time.time;
        }

        public override TaskStatus OnUpdate()
        {
            if (Time.time - _startTime < KeepTime)
                return TaskStatus.Running;

            return TaskStatus.Success;
        }

        public override void OnEnd()
        {
            PreviewObject.Value.SetActive(false);
        }
    }
