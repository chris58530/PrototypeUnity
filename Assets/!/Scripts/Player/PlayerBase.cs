using System;
using System.Collections;
using _.Scripts.Event;
using _.Scripts.Tools;
using _.Scripts.UI;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace _.Scripts.Player
{
    public class PlayerBase : MonoBehaviour, IDamageable, IKnockable
    {
        [Header("CAN BE HURT for test")] [SerializeField]
        private bool canHurt;

        public int maxHpValue;
        [HideInInspector] public ReactiveProperty<int> currentHpValue = new ReactiveProperty<int>();


        public bool getHurt;
        [SerializeField] private float hurtCD;
        private IDisposable _hurtTimer;
        [SerializeField] private float knockTime;
        private CharacterController _controller;

        private HeartTest _view;

        private void Awake()
        {
            _view = FindObjectOfType<HeartTest>();

            _controller = GetComponent<CharacterController>();
        }

        private void Start()
        {
            Initialize();
            currentHpValue.Skip(1).Subscribe(_ => { getHurt = true; }).AddTo(this);
            currentHpValue.Subscribe(_ =>
            {
                DebugTools.HpText(currentHpValue.Value);
                _view.UpdateHearts(currentHpValue.Value);
            }).AddTo(this);
        }

        void Initialize()
        {
            currentHpValue.Value = maxHpValue;
        }


        public void OnTakeDamage(int value)
        {
            if (!canHurt) return;

            _hurtTimer?.Dispose();
            _hurtTimer = Observable.EveryUpdate().First()
                .Delay(TimeSpan.FromSeconds(hurtCD)).Subscribe(
                    _ => { transform.gameObject.layer = LayerMask.NameToLayer("Player"); }
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
            if (!canHurt) return;
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
            // GetComponentInChildren<Animator>().Play("Died");
            GetComponent<Collider>().enabled = false;
            GameManager.Instance.SwitchScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}