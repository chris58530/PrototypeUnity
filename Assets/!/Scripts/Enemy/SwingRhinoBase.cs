using System;
using UniRx;
using UnityEngine;

namespace @_.Scripts.Enemy
{
    public class SwingRhinoBase : Enemy
    {
        [SerializeField] private GameObject AbsortCollider;
        [SerializeField] private float gravity;
        private CharacterController _controller;

        protected override void Awake()
        {
            base.Awake();
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Fall();
        }

        private void Fall()
        {
            _controller.Move(transform.up * (gravity * Time.deltaTime));
        }

        public void RestRhinoTimeLine()
        {
            SwingTrigger.OnSwingTrigger?.Invoke();

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Ground") ||
                other.gameObject.GetComponent<SwingRhinoBase>()) return;
            _controller.enabled = false;
            AbsortCollider.SetActive(true);
            bt.SendEvent("OnGround");
            enabled = false;
        }
    }
}