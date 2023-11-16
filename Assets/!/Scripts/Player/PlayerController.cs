using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Event;
using UnityEngine;
using UniRx;

namespace _.Scripts.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float walkSpeed;
        [SerializeField] private float rotateSpeed;


        [Header("Dash Setting")] [SerializeField]
        public float dashSpeed;

        public float dashDistance = 10;
        [SerializeField] public float dashTime;
        [SerializeField] public float dashChanceTime;

        [SerializeField] public float dashFailTime;

        [SerializeField] public float PureDashTime;
        [SerializeField] public float SingleDashTime ;
        [SerializeField] public float MultiDashTime;
        [SerializeField] public float BackDashTime ;

        [Header("BackDash Bullet Setting")] [SerializeField]
        private GameObject bullet;
        [SerializeField] private float lifeTime;
        [SerializeField]private Transform shootPoint;
        [SerializeField]private  float speed;
        public bool finishChance;


        // [SerializeField] private GameObject dashPreviewObj;
        public bool isDashing;


        [Header("Gravity Setting")] [SerializeField]
        private float gravity;

        public bool IsGround => _controller.isGrounded;
        private CharacterController _controller;
        private AttackDetect _attackDetect;
        private PlayerWeapon _playerWeapon;
        private IDisposable chanceDis;


        //debug
        public bool useMousePosToDash;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _attackDetect = GetComponentInChildren<AttackDetect>();
            _playerWeapon = GetComponentInChildren<PlayerWeapon>();
        }

        private void Start()
        {
        }


        public void Move(Vector3 dir)
        {
            Quaternion toRotation = Quaternion.LookRotation(dir, transform.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
            _controller.Move(dir * (walkSpeed * (Time.deltaTime)));
        }


        #region Dash

        public void PureDash()
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

            StartCoroutine(PerformDash(dashDirection, dashTime));
        }

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

            StartCoroutine(PerformDash(dashDirection, dashTime));
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

            StartCoroutine(PerformDash(dashDirection, dashTime));
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

        public void Dash()
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


            if (useMousePosToDash)
            {
                StartCoroutine(PerformDash(dashDirection, dashTime));
            }
            else StartCoroutine(MoveToTarget());
        }

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

        IEnumerator PerformDash(Vector3 dashDirection, float time)
        {
            isDashing = true;
            Vector3 endPosition = transform.position + dashDirection * dashDistance;
            transform.LookAt(endPosition);
         
            float elapsedTime = 0f;

            while (elapsedTime < time)
            {
                _controller.Move(transform.forward * (dashSpeed * Time.deltaTime));
                // transform.Translate(endPosition * (dashSpeed * Time.deltaTime));
                // transform.position = Vector3.Lerp(transform.position, endPosition, elapsedTime / dashTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            chanceDis = Observable.EveryUpdate()
                .Delay(TimeSpan.FromSeconds(dashChanceTime))
                .First()
                .Subscribe(_ => { finishChance = true; }).AddTo(this);
            isDashing = false;
        }
        IEnumerator BackDash(Vector3 dashDirection, float time)
        {
            isDashing = true;
            Vector3 endPosition = transform.position + dashDirection * dashDistance;
            transform.LookAt(endPosition);   //spawn bullet
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
            isDashing = false;
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

        #endregion


        #region Fall

        public void Fall()
        {
            if (IsGround) return;
            _controller.Move(transform.up * (gravity * Time.deltaTime));
        }

        #endregion
    }
}