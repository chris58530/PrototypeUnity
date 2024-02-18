using System;
using System.Collections;
using _.Scripts.Event;
using _.Scripts.Tools;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace _.Scripts.Player
{
    public class PlayerBase : MonoBehaviour, IDamageable, IKnockable
    {
        public float maxHpValue;
        [HideInInspector] public ReactiveProperty<float> currentHpValue = new ReactiveProperty<float>();


        public bool getHurt;
        [SerializeField] private float hurtCD;
        private IDisposable _hurtTimer;
        [SerializeField] private float knockTime;
        private CharacterController _controller;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Start()
        {
            Initialize();
            currentHpValue.Skip(1).Subscribe(_ => { getHurt = true; }).AddTo(this);
        }

        void Initialize()
        {
            currentHpValue.Value = maxHpValue;
        }


        public void OnTakeDamage(int value)
        {
            _hurtTimer?.Dispose();
            _hurtTimer = Observable.EveryUpdate().First()
                .Delay(TimeSpan.FromSeconds(hurtCD)).Subscribe(
                    _ => { transform.gameObject.layer = LayerMask.NameToLayer("Player");}
                ).AddTo(this);

            currentHpValue.Value -= value;
            if (currentHpValue.Value >= maxHpValue)
                currentHpValue.Value = maxHpValue;
            if (currentHpValue.Value <= 0)
            {
                OnDied();
            }

            transform.gameObject.layer = LayerMask.NameToLayer("UnDamageable");
            
            PlayerActions.onPlayerHurt?.Invoke();
        }


        public void OnKnock(Transform trans)
        {
            Vector3 dir = (transform.position - trans.position).normalized;
            StartCoroutine(Knock(dir));
        }

        IEnumerator Knock(Vector3 trans)
        {
            float elapsedTime = 0f;

            while (elapsedTime < knockTime)
            {
                _controller.Move(trans);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            getHurt = false;
        }


        public void OnDied()
        {
            Destroy(gameObject);
            Observable.EveryUpdate().First().Delay(TimeSpan.FromSeconds(1)).Subscribe(_ =>
            {
                string currentSceneName = SceneManager.GetActiveScene().name;

                // 使用當前場景的名稱重新載入場景
                SceneManager.LoadScene(currentSceneName);
            });
        }

        private void OnEnable()
        {
        }


        private void OnDisable()
        {
        }
    }
}