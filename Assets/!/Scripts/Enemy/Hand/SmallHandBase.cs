using System;
using _.Scripts.Event;
using _.Scripts.Interface;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace @_.Scripts.Enemy.Hand
{
    public class SmallHandBase : Enemy, IDamageable, IBreakable
    {
        [SerializeField] private BreakState breakState;


        [SerializeField] private int hp;
        [SerializeField] private UnityEvent onTakeDamagedEvent;
        [SerializeField] private UnityEvent onDiedEvent;
        private HandEffect _hadnEffect;

        public void OnTakeDamage(int value, Vector3 sparkleDirection, Quaternion rotation)
        {
            // SystemActions.onFrameSlow?.Invoke(0.03f); // 调用帧率减慢事件
            // SparkleEffect.onPlaySparkleEffect(SparkleType.Normal, sparkleDirection, rotation);
            //
            // if (hp <= 0) OnDied();
            //
            // onTakeDamagedEvent?.Invoke();
            // hp -= value;
        }

        public void OnDied()
        {
            onDiedEvent?.Invoke();
            Destroy(gameObject);
        }


        void Start()
        {
            _hadnEffect = GetComponent<HandEffect>();
            SwitchBreakMaterial();
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<CartRhinoBase>(out var cartRhino))
            {
                if (cartRhino.isGetCatch) return;
                cartRhino.CatchRhino();
                bt.SendEvent("CatchRhino");
            }
        }


        public void SwitchBreakMaterial()
        {
            if (breakState == BreakState.Break1)
                _hadnEffect.SwitchBreakMaterial(BreakState.Break1);

            if (breakState == BreakState.Break2)
                _hadnEffect.SwitchBreakMaterial(BreakState.Break2);
        }

        public void OnTakeBreakableAttack()
        {
            if (breakState == BreakState.Break2)
            {
                _hadnEffect.PlayBreakEffect();
                return;
            }

            breakState += 1;
            SwitchBreakMaterial();
        }

        private void OnEnable()
        {
            BossBBase.onBodyDied += OnDied;
        }

        private void OnDisable()
        {
            AutoTurnAroundDetect.onRemoveDetectList?.Invoke(this.gameObject);
            BossBBase.onBodyDied -= OnDied;
        }
    }
}