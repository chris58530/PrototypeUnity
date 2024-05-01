using System;
using _.Scripts.Interface;
using UnityEngine;

namespace @_.Scripts.Enemy.Hand
{
    public class SmallHandBase : Enemy
    {

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
    }
}