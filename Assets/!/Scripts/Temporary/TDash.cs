using System;
using System.Collections;
using _.Scripts.Player;
using UnityEngine;
using UniRx;
using Unity.VisualScripting;
using UnityEngine.UI;
using _.Scripts.Temporary;
using TMPro;
using UnityEngine.Serialization;

namespace _.Scripts.Temporary
{
    public enum DashState
    {
        None,
        Dash,
        ExtraReadyToDash,
        ExtraDash
    }

    public class TDash : PlayerBehaviourSimple
    {
        [Header("DebugUI")] [SerializeField] private TMP_Text state;
        [SerializeField] private TMP_Text atk;
        [SerializeField] private TMP_Text time;

        [Header("Dash UI")] [SerializeField] private Image _dashCDImage;
        [SerializeField] private Image _ReadyToDashImage;
        [SerializeField] private float _dashCD;

        private float _currentDashCD;

        [Header("Dash Setting")] [SerializeField]
        public float dashSpeed;

        [SerializeField] public float dashTime;
        [SerializeField] public float currentDashTime;
        [SerializeField] public bool canDoubleTime;
        [SerializeField] private GameObject dashWeapon;
        [SerializeField] private GameObject dashPreviewObj;
        [SerializeField] private GameObject model;

        [Header("Extra Dash Setting")] [SerializeField]
        private float ExtraReadyToDashKeepTime = 3;

        [SerializeField] private float currentExtraReadyToDashKeepTime;
        [SerializeField] private int dashQuanity;
        private float speedAddition;

        private bool _isDashing = false;
        private bool _isOnWall = false;
        private bool canDash;
        private Vector3 _dashDir;
        private ReactiveProperty<DashState> _dashState = new ReactiveProperty<DashState>();
        private IDisposable timerSubscription;

        private void Start()
        {
            _currentDashCD = _dashCD;
            currentDashTime = dashTime;
            dashQuanity = 0;
            _dashState.Value = DashState.None;
            _dashState.Subscribe(_ =>
            {
                Debug.Log($"current state : {_dashState.Value}");
                state.text = _dashState.Value.ToString();
            }).AddTo(this);
        }

        protected override void Update()
        {
            SwitchDashState();
            UpdateDashImage();
            UpdateReadyToDashImage();
            ResetDashCD();
            if (_isDashing)
            {
                currentDashTime -= Time.deltaTime;
                if (currentDashTime <= 100)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Debug.Log("success");
                        currentDashTime = dashTime;
                        Dash();
                    }
                }
            }
            else
            {
                currentDashTime = dashTime;
                dashQuanity = 0;
            }
        }

        void SwitchDashState()
        {
            switch (_dashState.Value)
            {
                case DashState.None:
                    base.Update();
                    model.transform.localEulerAngles = new Vector3(0, 0, 0);

                    currentExtraReadyToDashKeepTime = ExtraReadyToDashKeepTime;
                    dashPreviewObj.SetActive(false);

                    Time.timeScale = 1f;
                    time.text = 1.ToString();

                    if (input.IsReleasedDash && canDash)
                    {
                        ShowDashDirection(true);
                        transform.LookAt(_dashDir);
                        _dashState.Value = DashState.Dash;
                        Dash();
                    }
                    break;

                case DashState.Dash:
                    _dashState.Value = DashState.None;
                    break;


                case DashState.ExtraReadyToDash:
                    // currentExtraReadyToDashKeepTime -= Time.deltaTime;
                    Time.timeScale = 0.1f;
                    time.text = 0.1.ToString();
                    // if (currentExtraReadyToDashKeepTime <= 0)
                    // {
                    //     _dashState.Value = DashState.None;
                    //     Time.timeScale = 1f;
                    //     time.text = 1.ToString();
                    // }
                    ShowDashDirection(true);
                    if (input.IsReleasedDash)
                    {
                        transform.LookAt(_dashDir);

                        model.transform.localEulerAngles = new Vector3(90, 0, 0);

                        _dashState.Value = DashState.ExtraDash;
                        Time.timeScale = 1f;
                        time.text = 1.ToString();
                        Dash();
                    }
                    break;

                case DashState.ExtraDash:
                    _isOnWall = true;
                    currentExtraReadyToDashKeepTime = ExtraReadyToDashKeepTime;
                    model.transform.localEulerAngles = new Vector3(0, 0, 0);
                    _dashState.Value = DashState.None;
                    break;
            }
        }

        public void Dash()
        {
            dashQuanity += 1;
            if (!canDash) return;
            canDash = false;

            time.text = 1.ToString();


            _currentDashCD = 0;

            dashPreviewObj.SetActive(false);


            timerSubscription = Observable.EveryFixedUpdate().Subscribe(_ =>
            {
                _isDashing = true;

                controller.Move(transform.forward * (Time.deltaTime * dashSpeed));
            });


            Observable.Timer(TimeSpan.FromSeconds(dashTime)).Subscribe(_ =>
            {
                timerSubscription.Dispose();
                dashWeapon.SetActive(false);
                _isDashing = false;
                if (!_isOnWall)
                    _dashState.Value = Temporary.DashState.None;
            }).AddTo(this);
        }


        #region UI

        public void UpdateDashImage()
        {
            _dashCDImage.fillAmount = _currentDashCD / _dashCD;
        }

        public void UpdateReadyToDashImage()
        {
            _ReadyToDashImage.fillAmount = currentExtraReadyToDashKeepTime / ExtraReadyToDashKeepTime;
        }

        void ResetDashCD()
        {
            if (!canDash)
            {
                _currentDashCD += Time.deltaTime;
            }

            if (_currentDashCD >= _dashCD)
            {
                canDash = true;
            }
        }

        #endregion

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
            else Debug.Log("nothing");

            return hitpoint;
        }

        private void OnTriggerEnter(Collider other)
        {
            // if (_dashState.Value == DashState.ReadyToDash) return;
            if (_dashState.Value == DashState.ExtraReadyToDash) return;
            if (!_isDashing) return;
            if (other.gameObject.CompareTag("DashObject"))
            {
                timerSubscription.Dispose();
                _isOnWall = true;
                _dashState.Value = Temporary.DashState.ExtraReadyToDash;
            }
        }
    }
}