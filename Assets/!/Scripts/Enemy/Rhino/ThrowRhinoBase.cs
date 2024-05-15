using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Enemy.Hand;
using UniRx;
using UnityEngine;

namespace @_.Scripts.Enemy
{
    public class ThrowRhinoBase : Enemy
    {
        [SerializeField] private GameObject AbsortCollider;
        [SerializeField] private float gravity;
        private CharacterController _controller;

        protected override void Awake()
        {
            base.Awake();
            _controller = GetComponent<CharacterController>();
            Observable.EveryUpdate().First().Delay(TimeSpan.FromSeconds(0.25f)).Subscribe(_ =>
            {
                _controller.enabled = false;
                AbsortCollider.SetActive(true);
                bt.SendEvent("OnGround");
                enabled = false;
            }).AddTo(this);
            Observable.EveryUpdate().First().Delay(TimeSpan.FromSeconds(4)).Subscribe(_ =>
            {
             Destroy(gameObject);
            }).AddTo(this);
        }

        private void Update()
        {
            Fall();
        }

        private void Fall()
        {
            _controller.Move(transform.up * (gravity * Time.deltaTime));

            // Debug.Log(other.gameObject.name + " collided with Rhino");
        }

        public void RestRhinoTimeLine()
        {
            SwingTrigger.OnSwingTrigger?.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<EliteHandBase>() ||
                other.gameObject.GetComponent<SmallHandBase>()) return;
        }
    }
}