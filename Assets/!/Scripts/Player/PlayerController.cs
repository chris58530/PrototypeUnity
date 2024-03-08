using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Event;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace _.Scripts.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float walkSpeed;
        [SerializeField] private float rotateSpeed;

        [Header("Roll Setting")] [SerializeField]
        public float rollTime;

        public bool finsihRoll;
        private IDisposable _rollTimer;

        [Header("Dash Setting")] [SerializeField]
        public float dashSpeed;

        public float dashDistance = 10;
        [SerializeField] public float dashTime;


        [Header("Gravity Setting")] [SerializeField]
        private float gravity;

        public bool IsGround => _controller.isGrounded;
        private CharacterController _controller;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }


        public void Move(PlayerInput input)
        {
            Vector2 getInput = input.MoveVector;
            Vector3 dir = new Vector3(getInput.x, 0, getInput.y);
            Quaternion toRotation = Quaternion.LookRotation(dir, transform.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
            _controller.Move(dir * (walkSpeed * (Time.deltaTime)));
        }

        public void FaceInputDireaction(PlayerInput input)
        {
            Vector2 getInput = input.MoveVector;
            Vector3 dir = new Vector3(getInput.x, 0, getInput.y);
            Quaternion toRotation = Quaternion.LookRotation(dir, transform.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 1000 * Time.deltaTime);
        }


        public void Roll()
        {
            finsihRoll = false;
            _rollTimer?.Dispose();
            // transform.tag = "Undamaged";

            #region PerformDash

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask mask = 1 << LayerMask.NameToLayer("DashDetect");
            var targetPosition = Vector3.zero;
            if (Physics.Raycast(ray, out RaycastHit hit, 1000, mask))
            {
                Debug.DrawLine(ray.origin, hit.point);
                targetPosition = hit.point;
                targetPosition.y = transform.position.y;
            }

            Vector3 dashDirection = (targetPosition - transform.position).normalized;

            #endregion

            _rollTimer = Observable.EveryUpdate().First().Delay(TimeSpan.FromSeconds(rollTime)).Subscribe(_ =>
            {
                finsihRoll = true;
            }).AddTo(this);
            StartCoroutine(Roll(dashDirection, dashTime));

            //Audio
            AudioManager.Instance.PlaySFX("Dash");
        }


        IEnumerator Roll(Vector3 dashDirection, float time)
        {
            Vector3 endPosition = transform.position + dashDirection * dashDistance;

            float elapsedTime = 0f;

            while (elapsedTime < time)
            {
                _controller.Move(transform.forward * (dashSpeed * Time.deltaTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.tag = "Player";
        }

        public void Fall()
        {
            if (IsGround) return;
            _controller.Move(transform.up * (gravity * Time.deltaTime));
        }
    }
}