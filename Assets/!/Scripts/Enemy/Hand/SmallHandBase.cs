using System;
using _.Scripts.Interface;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace @_.Scripts.Enemy.Hand
{
    public enum SmallHandState
    {
        Normal,
        Breakable
    }

    public class SmallHandBase : Enemy, IDamageable, IBreakable
    {
        [SerializeField] private SmallHandState smallHandState;
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
            if (smallHandState == SmallHandState.Breakable)
                SwitchState();
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<CartRhinoBase>(out var cartRhino))
            {
                cartRhino.CatchRhino();
                bt.SendEvent("CatchRhino");
            }

            if (other.gameObject.TryGetComponent<BossBBody>(out var bossBBody))
            {
                bossBBody.ShakeBody();
                bt.SendEvent("HitBody");
            }

            if (other.gameObject.TryGetComponent<IBreakable>(out var breakable))
            {
                breakable.OnTakeAttack();
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