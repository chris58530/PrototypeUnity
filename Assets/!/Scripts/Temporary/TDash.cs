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
        [SerializeField] private TMP_Text combo;
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


        [SerializeField] private int dashQuanity;
        private bool hasAdd;

        private bool _isDashing = false;
        private bool _isOnWall = false;
        private bool canDash;
        private bool isDashCD;
        private Vector3 _dashDir;
        private ReactiveProperty<DashState> _dashState = new ReactiveProperty<DashState>();

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
            UpdateCombo();
            DoDash();
            if (isDashCD)
            {
                dashQuanity = 0;
                _currentDashCD -= Time.deltaTime;
                if (_currentDashCD <= 0)
                {
                    isDashCD = false;
                }
            }
        }

        void SwitchDashState()
        {
            switch (_dashState.Value)
            {
                case DashState.None:
                    base.Update();

                    model.transform.localEulerAngles = new Vector3(0, 0, 0);

                    dashPreviewObj.SetActive(false);

                    Time.timeScale = 1f;
                    time.text = 1.ToString();

                    if (input.IsPressedDash && !isDashCD)
                    {
                        canDash = true;
                        ShowDashDirection(true);
                        transform.LookAt(_dashDir);
                        _dashState.Value = DashState.Dash;
                    }

                    break;

                case DashState.Dash:

                    break;


                case DashState.ExtraReadyToDash:
                    // currentExtraReadyToDashKeepTime -= Time.deltaTime;
                    canDash = false;
                    Time.timeScale = 0.1f;
                    time.text = 0.1.ToString();

                    model.transform.localEulerAngles = new Vector3(90, 0, 0);

                    // if (currentExtraReadyToDashKeepTime <= 0)
                    // {
                    //     _dashState.Value = DashState.None;
                    //     Time.timeScale = 1f;
                    //     time.text = 1.ToString();
                    // }
                    ShowDashDirection(true);
                    if (input.IsPressedDash)
                    {
                        canDash = true;
                        transform.LookAt(_dashDir);
                        Time.timeScale = 1f;
                        time.text = 1.ToString();
                        _dashState.Value = DashState.ExtraDash;
                    }

                    break;

                case DashState.ExtraDash:
                    _isOnWall = false;
                    model.transform.localEulerAngles = new Vector3(0, 0, 0);
                    break;
            }
        }

        void DoDash()
        {
            if (!canDash || isDashCD)
            {
                UpdateReadyToDashImage(false);

                currentDashTime = dashTime;
                _isDashing = false;
                if (!_isOnWall)
                    _dashState.Value = Temporary.DashState.None;
                return;
            }

            if (!hasAdd)
            {
                dashQuanity += 1;
                hasAdd = true;
            }

            Debug.Log("isdashing");
            Debug.Log(canDash);
            _isDashing = true;
            ShowDashDirection(false);
            controller.Move(transform.forward * (Time.deltaTime * dashSpeed));
            currentDashTime -= Time.deltaTime;

            if (currentDashTime < 0.2)
            {
                UpdateReadyToDashImage(true);
                if (input.IsPressedDash)
                {
                    currentDashTime = dashTime;
                    hasAdd = false;
                    ShowDashDirection(true);
                    transform.LookAt(_dashDir);
                }
            }

            if (currentDashTime <= 0)
            {
                canDash = false;
                isDashCD = true;
                _currentDashCD = _dashCD;
            }
        }


        #region UI

        public void UpdateDashImage()
        {
            _dashCDImage.fillAmount = _currentDashCD / _dashCD;
        }

        public void UpdateReadyToDashImage(bool t)
        {
            _ReadyToDashImage.enabled = t;
        }

        public void UpdateCombo()
        {
            combo.text = dashQuanity.ToString();
        }

        #endregion

        public void ShowDashDirection(bool isShow)
        {
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
                _isOnWall = true;
                _dashState.Value = Temporary.DashState.ExtraReadyToDash;
            }
        }
    }
}