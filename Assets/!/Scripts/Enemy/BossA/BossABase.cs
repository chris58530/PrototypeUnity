using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Interface;
using _.Scripts.Player.Ability;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.Serialization;

namespace _.Scripts.Enemy.BossA
{
    [RequireComponent(typeof(BossAController))]
    public class BossABase : Enemy, IDamageable, IShieldable
    {
        [SerializeField] private Animator ani;
        public Image hpImage;
        public Image hardHpImage;

        [SerializeField] private float maxHp;
        private ReactiveProperty<float> _currentHp = new ReactiveProperty<float>();
        [Tooltip(" behaviour tree control")] public bool isShielded = true;

        [Header("Shield Setting")] //.
        [SerializeField]
        private int shieldValue;
    
        [SerializeField] private Material bodydMaterial;
        [SerializeField] private Material elseMaterial;
        [SerializeField] private Material bombMaterial;
        [SerializeField] private Material bigBombMaterial;

        //big bomb 
        [Tooltip("How many times can throw and set the value ,form large number to small")] [SerializeField]
        private int[] canThrowBigBombHp;

        private int _successTime;
        private ShieldUI _shieldUI;

        protected override void Awake()
        {
            base.Awake();
            _shieldUI = GetComponentInChildren<ShieldUI>();
        }

        private void Start()
        {
            Initialize();

            _currentHp.Subscribe(_ =>
            {
                hpImage.fillAmount = _currentHp.Value / maxHp;
                hardHpImage.fillAmount = _currentHp.Value / maxHp;
            }).AddTo(this);
            
            ResetShield();

        }

        private void Update()
        {
            if (shieldValue <= 0) bt.SendEvent("Remove_Shield_Event");
        }

        void Initialize()
        {
            _currentHp.Value = maxHp;
        }


        public void OnTakeDamage(int value)
        {
            if (_currentHp.Value <= 0)
                return;
            if (isShielded)
            {
                _shieldUI.HitShield(shieldValue);
                _currentHp.Value -= value / 5;

                //Play effect
                StartCoroutine(OnTakeDamageCoroutine());
            }

            else
            {
                _currentHp.Value -= value;
                ///////////////coldwater///////////////////////
                StartCoroutine(OnTakeDamageCoroutine());
            }


            if (_currentHp.Value <= 0)
            {
                Debug.Log("Boss A Die");
                OnDied();
            }
        }

        public void OnTakeShield(int removeValue)
        {
            _shieldUI.BreakShield(shieldValue - 1);
            shieldValue -= removeValue;
        }

        public void ResetShield()
        {
            _shieldUI.ResetShield();

            shieldValue = 3;
        }

        public void OnDied()
        {
            TimeLineManager.Instance.PlayTimeLine(1);
            ani.Play("Die");
            _shieldUI.DisableImage();
            GetComponent<AbilityContainer>().SetCanGetAbility(true);
            bt.enabled = false;
        }

        public void IsShield(bool b)
        {
            float minValue = -5f;
            float maxValue = 5f;
            float transitionDuration = 2f;
            if (b)
            {
                hardHpImage.enabled = true;
                hpImage.enabled = false;


                StartCoroutine(TransitionFloatValue(maxValue, minValue, transitionDuration));
            }
            else
            {
                hardHpImage.enabled = false;
                hpImage.enabled = true;

                StartCoroutine(TransitionFloatValue(minValue, maxValue, transitionDuration));
            }
        }

        public void DetectCanThrowBigBomb()
        {
            if (_currentHp.Value < canThrowBigBombHp[_successTime])
            {
                // bt.SendEvent("Big_Bomb_Event");
                _successTime++;
            }
        }

        public void EndPerformance()
        {
            TimeLineManager.Instance.PlayTimeLine(2);
        }

        public void BigBombTransition(bool isAppeared)
        {
            float minValue = -1f;
            float maxValue = 1f;
            float current = bigBombMaterial.GetFloat("__Surface_Dissolove");
            StopCoroutine(BigBomb_TransitionFloatValue(0, 0, 0));
            if (isAppeared)
            {
                StartCoroutine(BigBomb_TransitionFloatValue(0.3f, minValue, 15));
            }
            else
                StartCoroutine(BigBomb_TransitionFloatValue(current, maxValue, 0.5f));
        }


        private void OnEnable()
        {
            bodydMaterial.SetFloat("_Surface_DiffuseDissolve", -5);
            elseMaterial.SetFloat("_Surface_DiffuseDissolve", -5);
            bigBombMaterial.SetFloat("__Surface_Dissolove", -1);
        }

        private void OnDisable()
        {
            //RESET SHADERs
            bodydMaterial.SetFloat("_Surface_DiffuseDissolve", -5);
            elseMaterial.SetFloat("_Surface_DiffuseDissolve", -5);
            bigBombMaterial.SetFloat("__Surface_Dissolove", -1);
        }

        IEnumerator OnTakeDamageCoroutine()
        {
            bodydMaterial.SetInt("_Surface_EMISSION", 1);
            elseMaterial.SetInt("_Surface_EMISSION", 1);
            bombMaterial.SetInt("_Surface_EMISSION", 1);
            yield return new WaitForSeconds(0.08f); // 等待閃爍持續時間
            bodydMaterial.SetInt("_Surface_EMISSION", 0);
            elseMaterial.SetInt("_Surface_EMISSION", 0);
            bombMaterial.SetInt("_Surface_EMISSION", 0);
        }

        IEnumerator TransitionFloatValue(float startValue, float endValue, float duration)
        {
            float timer = 0.0f;

            while (timer < duration)
            {
                // 線性插值計算當前值
                float currentValue = Mathf.Lerp(startValue, endValue, timer / duration);

                // 將當前值設置到 Shader 的 float 屬性中
                bodydMaterial.SetFloat("_Surface_DiffuseDissolve", currentValue);
                elseMaterial.SetFloat("_Surface_DiffuseDissolve", currentValue);

                // 增加計時器
                timer += Time.deltaTime;

                // 等待下一個更新周期
                yield return null;
            }
        }

        IEnumerator BigBomb_TransitionFloatValue(float startValue, float endValue, float duration)
        {
            Debug.Log("start disslove");
            float timer = 0.0f;

            while (timer < duration)
            {
                // 線性插值計算當前值
                float currentValue = Mathf.Lerp(startValue, endValue, timer / duration);

                // 將當前值設置到 Shader 的 float 屬性中
                bigBombMaterial.SetFloat("__Surface_Dissolove", currentValue);

                // 增加計時器
                timer += Time.deltaTime;

                // 等待下一個更新周期
                yield return null;
            }
        }
    }
}