using System;
using _.Scripts.Interface;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace @_.Scripts.Enemy.Hand
{
    public class SmallHandBase : Enemy, IDamageable, IBreakable
    {
        [FormerlySerializedAs("handState")] [SerializeField]
        private BreakState breakState;

        [SerializeField] private Renderer[] renderers;
        [SerializeField] private Material brokenMaterial1;
        [SerializeField] private Material brokenMaterial2;
        [SerializeField] private int hp;
        [SerializeField] private UnityEvent onTakeDamagedEvent;
        [SerializeField] private UnityEvent onDiedEvent;

        public void OnTakeDamage(int value)
        {

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
            if (breakState == BreakState.Break1)
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
                foreach (var m in renderers)
                {
                    m.material = brokenMaterial1;
                }
            if(breakState == BreakState.Break2)
                foreach (var m in renderers)
                {
                    m.material = brokenMaterial2;
                }
        }

        public void OnTakeAttack()
        {
            SwitchBreakMaterial();
        }
    }
}