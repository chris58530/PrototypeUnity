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

        [SerializeField] private Renderer[] renderers;
        [SerializeField] private Material brokenMaterial1;
        [SerializeField] private Material brokenMaterial2;
        [SerializeField] private int hp;
        [SerializeField] private UnityEvent onTakeDamagedEvent;
        [SerializeField] private UnityEvent onDiedEvent;
        private HandEffect _hadnEffect;

        public void OnTakeDamage(int value)
        {
            SystemActions.onFrameSlow?.Invoke(0.03f); // 调用帧率减慢事件

            if (hp <= 0) OnDied();

            onTakeDamagedEvent?.Invoke();
            hp -= value;
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

        public void OnTakeAttack()
        {
            breakState += 1;
            SwitchBreakMaterial();
        }
    }
}