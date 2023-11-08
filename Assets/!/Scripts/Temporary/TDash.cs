using System;
using System.Collections;
using _.Scripts.Player;
using UnityEngine;
using UniRx;
using Unity.VisualScripting;
using UnityEngine.UI;
using _.Scripts.Temporary;

namespace _.Scripts.Temporary
{
    public enum DashState
    {
        None = 0,
        Dash = 1,
        ChanceTime = 2,
        ExtraDash = 3
    }

    public class TDash : PlayerBehaviourSimple
    {
        [Header("Dash UI")] [SerializeField] private Image _dashCDImage;
        [SerializeField] private float _dashCD;
        private float _currentDashCD;

        [Header("Dash Setting")] [SerializeField]
        public float dashSpeed;

        [SerializeField] public float dashTime;
        [SerializeField] private GameObject dashWeapon;
        [SerializeField] private GameObject dashPreviewObj;
        [SerializeField] private GameObject model;

        [Header("Extra Dash Setting")] [SerializeField]
        private float timeToExtra = 1f;

        private float currentTimeToExtra;

        private float speedAddition;

        private bool onTheWall;
        private bool _isDashing;
        private bool canDash;
        private Vector3 _dashDir;
        private ReactiveProperty<DashState> _dashState = new ReactiveProperty<DashState>();

        private void Start()
        {
            currentTimeToExtra = timeToExtra;

            dashPreviewObj.SetActive(false);
            _currentDashCD = _dashCD;
            _dashState.Value = DashState.None;

            _dashState.Subscribe(_ => { Debug.Log($"current state : {_dashState.Value}"); }).AddTo(this);
        }

        protected override void Update()
        {
            SwitchDashState();
            UpdateDashImage();
            ResetDashCD();
        }

        void SwitchDashState()
        {
            switch (_dashState.Value)
            {
                case DashState.None:
                    _dashState.Value = DashState.Dash;
                    break;

                case DashState.Dash:

                    if (input.IsPressedDash) ShowDashDirection(true);
                    if (input.IsReleasedDash) Dash();
                    base.Update();

                    break;
                case DashState.ChanceTime:

                    timeToExtra -= Time.deltaTime;
                    if (timeToExtra <= 0)
                        _dashState.Value = DashState.Dash;

                    else if (input.IsPressedDash)
                    {
                        _currentDashCD = _dashCD;
                        Time.timeScale = 0.1f;

                        _dashState.Value = DashState.ExtraDash;
                    }

                    break;
                case DashState.ExtraDash:
                    currentTimeToExtra = timeToExtra;

                    if (input.IsPressedDash)
                    {
                        model.transform.localEulerAngles = new Vector3(90, 0, 0);

                        ShowDashDirection(true);
                    }

                    if (input.IsReleasedDash)
                    {
                        model.transform.localEulerAngles = new Vector3(0, 0, 0);

                        Dash();
                    }

                    base.Update();
                    break;
            }
        }

        public void UpdateDashImage()
        {
            _dashCDImage.fillAmount = _currentDashCD / _dashCD;
        }

        void ResetDashCD()
        {
            if (!canDash)
            {
                _currentDashCD += 0.02f;
            }

            if (_currentDashCD >= _dashCD)
            {
                canDash = true;
            }
        }

        public void ShowDashDirection(bool isShow)
        {
            if (!canDash) return;
            dashPreviewObj.SetActive(true);

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
            if (!canDash) return;
            Time.timeScale = 1f;
            onTheWall = false;


            transform.parent = null;
            _currentDashCD = 0;
            _isDashing = true;
            dashPreviewObj.SetActive(false);
            dashWeapon.SetActive(true);

            transform.LookAt(_dashDir);

            var doDash = Observable.EveryFixedUpdate();
            var timerSubscription = doDash.Subscribe(_ =>
            {
                controller.Move(transform.forward * (Time.deltaTime * dashSpeed));
                model.transform.localEulerAngles = new Vector3(0, 0, 0);
            });


            Observable.Timer(TimeSpan.FromSeconds(dashTime)).Subscribe(_ =>
            {
                timerSubscription.Dispose();
                dashWeapon.SetActive(false);
                _isDashing = false;
                _dashState.Value = Temporary.DashState.Dash;

                canDash = false;
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

        private void OnTriggerEnter(Collider other)
        {
            if (_dashState.Value == DashState.ChanceTime) return;
            if (other.gameObject.CompareTag("DashObject"))
            {
                _dashState.Value = Temporary.DashState.ChanceTime;
            }
        }
    }
}