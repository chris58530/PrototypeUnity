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
        Chance,
        ExtraDash
    }

    public class TDash : PlayerBehaviourSimple
    {
        [Header("DebugUI")] [SerializeField] private TMP_Text state;
        [SerializeField] private TMP_Text combo;
        [SerializeField] private TMP_Text time;

        [Header("Dash UI")] [SerializeField] private Image _dashCDImage;


        [Header("Dash Setting")] [SerializeField]
        private float dashSpeed;

//衝刺持續時間一定要小於beat
        [SerializeField] public float dashTime;
        [SerializeField] public float currentDashTime;
        [SerializeField] private float dashCD = 4;
        [SerializeField] private GameObject dashPreviewObj;
        [SerializeField] private GameObject shadowModel;
        [SerializeField] private int dashCombo;


        private bool hasAdd;
        [SerializeField] private bool canDash;
        private Vector3 _dashDir;
        private ReactiveProperty<DashState> _dashState = new ReactiveProperty<DashState>();
        private IDisposable dashCoolTime;
        private IDisposable dashingTime;

        private void Start()
        {
            currentDashTime = dashTime;
            dashCombo = 0;
            canDash = true;
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
            UpdateDashImg();
            UpdateCombo();
        }

        void SwitchDashState()
        {
            switch (_dashState.Value)
            {
                case DashState.None:

                    base.Update();
                    ResetCombo();
                    InDashCoolTime();
                    if (input.IsPressedDash && canDash)
                    {
                        _dashState.Value = DashState.Dash;

                        DashShadow shadow = Instantiate(
                                shadowModel, transform.position, transform.rotation)
                            .GetComponent<DashShadow>();
                        shadow.Init(1f, transform);
                    }

                    break;

                case DashState.Dash:

                    if (!hasAdd) AddCombo();

                    controller.Move(transform.forward * (Time.deltaTime * dashSpeed));


                    dashingTime = Observable.EveryUpdate()
                        .First()
                        .Sample(TimeSpan.FromSeconds(dashTime))
                        .Subscribe(_ =>
                        {
                            canDash = false;
                            RestDash();
                            _dashState.Value = DashState.Chance;
                        }).AddTo(this);


                    break;
                case DashState.Chance:
                    base.Update();
                    if (BeatManager.missBeat)
                    {
                        _dashState.Value = DashState.None;
                        BeatManager.missBeat = false;
                    }

                    if (BeatManager.onBeat && input.IsPressedDash)
                    {
                        dashingTime.Dispose();
                        hasAdd = false;
                        _dashState.Value = DashState.Dash;
                    }

                    break;
            }
        }

        void RestDash()
        {
            dashCoolTime?.Dispose();
            dashingTime?.Dispose();
        }

        void AddCombo()
        {
            dashCombo += 1;
            hasAdd = true;
        }

        void ResetCombo()
        {
            dashCombo = 0;
            hasAdd = false;
        }

        void InDashCoolTime()
        {
            dashCoolTime = Observable.EveryUpdate()
                .First()
                .Delay(TimeSpan.FromSeconds(dashCD))
                .Subscribe(_ => { canDash = true; }).AddTo(this);
        }


        #region UI

        public void UpdateCombo()
        {
            combo.text = dashCombo.ToString();
        }

        void UpdateDashImg()
        {
            if (canDash) _dashCDImage.enabled = true;
            else _dashCDImage.enabled = false;
        }

        #endregion

        #region MouseDash

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

        #endregion
    }
}