using System;
using _.Scripts.Interface;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace @_.Scripts.Enemy.Hand
{
 

    public class SmallHandBase : Enemy, IDamageable, IBreakable
    {
     [SerializeField] private HandState handState;
        [SerializeField] private Renderer[] renderers;
        [SerializeField] private Material brokenMaterial;
        [SerializeField] private int hp;
        private bool canbreak;
        [SerializeField] private UnityEvent onTakeDamagedEvent;
        [SerializeField] private UnityEvent onDiedEvent;

        public void OnTakeDamage(int value)
        {
            if (!canbreak)return;
            
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
            if (handState == HandState.Break_1)
                SwitchState();
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<CartRhinoBase>(out var cartRhino))
            {
                cartRhino.CatchRhino();
                bt.SendEvent("CatchRhino");
            }
         
        }


        public void SwitchState()
        {
            foreach (var m in renderers)
            {
                m.material = brokenMaterial;
            }
        }

        public void OnTakeAttack()
        {
            canbreak = true;
            SwitchState();
        }
    }
}