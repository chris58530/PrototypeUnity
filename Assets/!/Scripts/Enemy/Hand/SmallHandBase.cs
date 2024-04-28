using System;
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
        }
    }
}