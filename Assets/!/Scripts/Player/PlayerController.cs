using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Event;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace _.Scripts.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float walkSpeed;
        [SerializeField] private float rotateSpeed;

        [Header("Attack Setting")] [SerializeField]
        public float attackTime;

        [SerializeField] private GameObject weapon;
        [SerializeField] private GameObject attackChancePreview;

        [Header("Roll Setting")] [SerializeField]
        public float rollTime;

        [Header("Dash Setting")] [SerializeField]
        public float dashSpeed;

        public float dashDistance = 10;
        [SerializeField] public float dashTime;
        [SerializeField] public float dashChanceTime;

        [SerializeField] public float dashFailTime;

        [SerializeField] public float SingleDashTime;
        [SerializeField] public float MultiDashTime;
        [SerializeField] public float BackDashTime;

        [Header("BackDash Bullet Setting")] [SerializeField]
        private GameObject bullet;

        [SerializeField] private Transform shootPoint;


        public bool finishChance;

        // [SerializeField] private GameObject dashPreviewObj;
        public bool isSingleDash;


        [Header("Gravity Setting")] [SerializeField]
        private float gravity;

        public bool IsGround => _controller.isGrounded;
        private CharacterController _controller;
        private IDisposable chanceDis;


        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }


        public void Move(Vector3 dir)
        {
            Quaternion toRotation = Quaternion.LookRotation(dir, transform.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
            _controller.Move(dir * (walkSpeed * (Time.deltaTime)));
        }

        private IDisposable _attack;

        public void Attack(float f)
        {
            chanceDis?.Dispose();
            _attack?.Dispose();

            finishChance = false;
            transform.LookAt(GetDirection());
            weapon.SetActive(true);
            _attack = Observable.EveryUpdate().First().Delay(TimeSpan.FromSeconds(f)).Subscribe(_ =>
            {
                chanceDis = Observable.EveryUpdate()
                    .Delay(TimeSpan.FromSeconds(dashChanceTime))
                    .First()
                    .Subscribe(_ => { finishChance = true; }).AddTo(this);
            }).AddTo(this);
        }

        public void CancelAttack()
        {
            weapon.SetActive(false);
        }

        public void AttackChancePreview(Color color)
        {
            attackChancePreview.GetComponent<MeshRenderer>().material.color = color;
        }

        public void Roll()
        {
            chanceDis?.Dispose();
            finishChance = false;

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

            StartCoroutine(Roll(dashDirection, dashTime));
        }

        IEnumerator Roll(Vector3 dashDirection, float time)
        {
            Vector3 endPosition = transform.position + dashDirection * dashDistance;
            // transform.LookAt(endPosition);

            float elapsedTime = 0f;

            while (elapsedTime < time)
            {
                _controller.Move(transform.forward * (dashSpeed * Time.deltaTime));
                // transform.Translate(endPosition * (dashSpeed * Time.deltaTime));
                // transform.position = Vector3.Lerp(transform.position, endPosition, elapsedTime / dashTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }


        #region Dash

        public void SingleDash()
        {
            chanceDis?.Dispose();
            finishChance = false;

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

            StartCoroutine(SingleDash(dashDirection, dashTime));
        }

        IEnumerator SingleDash(Vector3 dashDirection, float time)
        {
            isSingleDash = true;
            Vector3 endPosition = transform.position + dashDirection * dashDistance;
            transform.LookAt(endPosition);

            float elapsedTime = 0f;
            while (elapsedTime < time)
            {
                //test 
                if (!isSingleDash) break;
                _controller.Move(transform.forward * (dashSpeed * Time.deltaTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            chanceDis = Observable.EveryUpdate()
                .Delay(TimeSpan.FromSeconds(dashChanceTime))
                .First()
                .Subscribe(_ => { finishChance = true; }).AddTo(this);
            isSingleDash = false;
        }

        public void MultiDash()
        {
            chanceDis?.Dispose();
            finishChance = false;

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

            StartCoroutine(MultiDash(dashDirection, dashTime));
        }


        private Vector3 _dashDir;

        public void ShowDashDirection(bool isShow)
        {
            // if (!isShow)
            // {
            //     dashPreviewObj.SetActive(false);
            //     return;
            // }

            _dashDir = new Vector3(GetDirection().x, transform.position.y, GetDirection().z).normalized;
            // dashPreviewObj.SetActive(true);
            // dashPreviewObj.transform.LookAt(_dashDir);
        }


        IEnumerator MultiDash(Vector3 dashDirection, float time)
        {
            Vector3 endPosition = transform.position + dashDirection * dashDistance;
            transform.LookAt(endPosition);

            float elapsedTime = 0f;

            while (elapsedTime < time)
            {
                //test to 0
                _controller.Move(transform.forward * (dashSpeed * Time.deltaTime * 0.5f));
                // transform.Translate(endPosition * (dashSpeed * Time.deltaTime));
                // transform.position = Vector3.Lerp(transform.position, endPosition, elapsedTime / dashTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            chanceDis = Observable.EveryUpdate()
                .Delay(TimeSpan.FromSeconds(dashChanceTime))
                .First()
                .Subscribe(_ => { finishChance = true; }).AddTo(this);
        }

        public void BackDash()
        {
            chanceDis?.Dispose();
            finishChance = false;


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

            StartCoroutine(BackDash(dashDirection, dashTime));
        }

        IEnumerator BackDash(Vector3 dashDirection, float time)
        {
            Vector3 endPosition = transform.position + dashDirection * dashDistance;
            transform.LookAt(endPosition); //spawn bullet
            Instantiate(bullet, shootPoint.position, shootPoint.rotation);
            float elapsedTime = 0f;

            while (elapsedTime < time)
            {
                _controller.Move(-transform.forward * (dashSpeed * Time.deltaTime));
                // transform.Translate(endPosition * (dashSpeed * Time.deltaTime));
                // transform.position = Vector3.Lerp(transform.position, endPosition, elapsedTime / dashTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            chanceDis = Observable.EveryUpdate()
                .Delay(TimeSpan.FromSeconds(dashChanceTime))
                .First()
                .Subscribe(_ => { finishChance = true; }).AddTo(this);
        }

        #endregion


        IEnumerator MoveToTarget()
        {
            Vector3 endPosition = transform.position + transform.forward * dashDistance;


            float t = 0;
            while (true)
            {
                t += Time.deltaTime;
                float a = t / dashTime;

                transform.position = Vector3.Lerp(transform.position, endPosition, a);
                if (a >= 1f)
                {
                    chanceDis = Observable.EveryUpdate()
                        .Delay(TimeSpan.FromSeconds(dashChanceTime))
                        .First()
                        .Subscribe(_ => { finishChance = true; }).AddTo(this);
                    break;
                }

                yield return null;
            }
        }

        Vector3 GetDirection()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var layerMask = 1 << 9;
            RaycastHit hit;
            var hitpoint = Vector3.zero;
            if (Physics.Raycast(ray, out hit, 1000, layerMask))
            {
                hitpoint = hit.point;
                hitpoint.y = transform.position.y;
                return hitpoint;
            }

            return hitpoint;
        }


        #region Fall

        public void Fall()
        {
            if (IsGround) return;
            _controller.Move(transform.up * (gravity * Time.deltaTime));
        }

        #endregion
    }
}