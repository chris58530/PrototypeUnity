using System;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace _.Scripts.Enemy
{
    public class Enemy : MonoBehaviour
    {
        protected BehaviorTree bt;
        protected Rigidbody rb;


        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody>();
            bt = GetComponent<BehaviorTree>();
        }

         


     
    }
}