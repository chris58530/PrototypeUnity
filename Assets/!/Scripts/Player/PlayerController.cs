using System;
using System.Collections.Generic;
using _.Scripts.Event;
using UnityEngine;
using UniRx;

namespace _.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float walkSpeed;
        [SerializeField] private float rotateSpeed;

        [Header("Attack Setting")] [SerializeField]
        public float attackTime;


        [SerializeField] private GameObject attackWeapon;

        [Header("Dash Setting")] [SerializeField]
        public float dashSpeed;

        [SerializeField] public float dashTime;
        [SerializeField] private GameObject dashWeapon;
        [SerializeField] private GameObject dashPreviewObj;

        [Header("Pull Setting")] [SerializeField]
        private float pullTime;

        [SerializeField] private float pullMaxDistance;
        [SerializeField] private GameObject pullVisualizeObject;
        private IPullable _currentPullObject;

        [Header("Gravity Setting")] [SerializeField]
        private float gravity;

        public bool IsGround => _controller.isGrounded;
        private CharacterController _controller;
        private AttackDetect _attackDetect;
        private PlayerWeapon _playerWeapon;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _attackDetect = GetComponentInChildren<AttackDetect>();
            _playerWeapon = GetComponentInChildren<PlayerWeapon>();
        }

        private void Start()
        {
            pullVisualizeObject.transform.localScale = Vector3.zero;
            attackWeapon.SetActive(false);
        }
      

        public void Move(Vector3 dir)
        {
            Quaternion toRotation = Quaternion.LookRotation(dir, transform.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
            _controller.Move(dir * (walkSpeed * (Time.deltaTime)));
        }

        public void Attack()
        {
            attackWeapon.SetActive(true);

            Observable.Timer(TimeSpan.FromSeconds(attackTime)).Subscribe(_ => { attackWeapon.SetActive(false); });
            Vector3 targetPos = _attackDetect.GetAttackTarget();
            Vector3 dir = targetPos - transform.position;
            if (_attackDetect.DamageObj.Count <= 0)
            {
                dir = transform.forward;
            }
            dir.y = 0;
            Quaternion toRotation = Quaternion.LookRotation(dir.normalized, transform.up);
            transform.rotation = toRotation;
        }

        #region Dash

        private Vector3 _dashDir;

        public void ShowDashDirection(bool isShow)
        {
            if (!isShow)
            {
                dashPreviewObj.SetActive(false);
                return;
            }

            _dashDir = GetDirection();
            dashPreviewObj.SetActive(true);
            dashPreviewObj.transform.LookAt(_dashDir);
        }

        public void Dash()
        {
            dashPreviewObj.SetActive(false);
            dashWeapon.SetActive(true);

            transform.LookAt(_dashDir);

            var doDash = Observable.EveryFixedUpdate();
            var timerSubscription = doDash.Subscribe(_ =>
            {
                _controller.Move(transform.forward * (Time.deltaTime * dashSpeed));
            });


            Observable.Timer(TimeSpan.FromSeconds(dashTime)).Subscribe(_ =>
            {
                timerSubscription.Dispose();
                dashWeapon.SetActive(false);
            });
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

        #region Pull

        //注意地圖或是周遭物件不是IgnoreRayacst

        private readonly List<IPullable> _pullableObject = new List<IPullable>();

        public void SetPullTarget()
        {
            pullVisualizeObject.transform.localScale = new Vector3(
                pullMaxDistance * 2, 1, pullMaxDistance * 2);
            Time.timeScale = 0f;

            if (Input.GetMouseButton(0)) GetTarget();
            if (_currentPullObject != null) SetTargetPullDirection();
        }

        void GetTarget()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var layerMask = (1 << 7);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                GameObject hitObject = hit.collider.gameObject;
                // if (hitObject == null) return;
                if ((hitObject.transform.position - transform.position).magnitude > pullMaxDistance) return;

                if (!hitObject.TryGetComponent<IPullable>(out var pullable)) return;

                if (_currentPullObject != null)
                    _currentPullObject.PullDirection = Vector3.zero;
                _currentPullObject = pullable;
                if (!_pullableObject.Contains(pullable))
                {
                    _pullableObject.Add(pullable);
                    Debug.Log($"{hitObject.name} 加入 {_pullableObject.Count}List");
                }
            }
        }

        void SetTargetPullDirection()
        {
            Debug.Log("set pull direction");

            if (Input.GetMouseButtonUp(0))
            {
                _currentPullObject = null;
                return;
            }

            var hitpoint = Vector3.zero;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity))
            {
                hitpoint = hitInfo.point;
                // _currentPullObject.SetVisualizePullDirection(hitpoint);
            }

            hitpoint.y = 0;


            _currentPullObject.PullDirection = hitpoint;
        }

        public void PullTarget()
        {
            pullVisualizeObject.transform.localScale = Vector3.zero;
            Time.timeScale = 1;
            if (_pullableObject.Count <= 0) return;

            foreach (var target in _pullableObject)
            {
                target.Pull();
                target.PullDirection = Vector3.zero;
            }

            _pullableObject.Clear();
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