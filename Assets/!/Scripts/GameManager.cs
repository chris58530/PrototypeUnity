using System;
using _.Scripts.Event;
using _.Scripts.Player;
using UnityEngine;
using _.Scripts.Tools;
using UniRx;
using UnityEngine.SceneManagement;

namespace _.Scripts
{
    public class GameManager : Singleton<GameManager>
    {
        private PlayerInput _playerInput;
        private PlayerBase _playerBase;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            SystemActions.onSceneStart?.Invoke();
        }

        public void TimeScale(int scale)
        {
            Time.timeScale = scale;
        }

        public void LockPlayerInput(bool isEnabled)
        {
            GameObject player = GameObject.FindWithTag("Player");
            _playerInput = player.GetComponent<PlayerInput>();
            _playerInput.enabled = isEnabled;
        }

        public void LockPlayerHp(bool isEnabled)
        {
            GameObject player = GameObject.FindWithTag("Player");
            _playerBase = player.GetComponent<PlayerBase>();
            _playerBase.canHurt = isEnabled;
        }

        public void SwitchScene(int num, float time)
        {
            SystemActions.onSwitchScene?.Invoke(time);
            Observable.EveryUpdate().First().Delay(TimeSpan.FromSeconds(time + 2)).Subscribe(_ =>
            {
                SceneManager.LoadScene(num);
            }).AddTo(this);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                GameObject player = GameObject.FindWithTag("Player");
                _playerBase = player.GetComponent<PlayerBase>();
                _playerBase.isDead = true;
                SystemActions.onSwitchScene?.Invoke(1);
                Observable.EveryUpdate().First().Delay(TimeSpan.FromSeconds(2)).Subscribe(_ =>
                {
                    int currentSceneName = SceneManager.GetActiveScene().buildIndex;
                    SceneManager.LoadScene(currentSceneName);
                }).AddTo(this);
            }
        }
        private void OnEnable()
        {
            TimeScale(1);
        }
        private void OnDisable()
        {
         TimeScale(1);
        }
    }
}