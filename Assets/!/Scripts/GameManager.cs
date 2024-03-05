using System;
using _.Scripts.Event;
using UnityEngine;
using _.Scripts.Tools;
using UniRx;
using UnityEngine.SceneManagement;

namespace _.Scripts
{
    public class GameManager : Singleton<GameManager>
    {
        private void Start()
        {
            SystemActions.onSceneStart?.Invoke();
        }

        public void SwitchScene(int num)
        {
            SystemActions.onSwitchScene?.Invoke();
            Observable.EveryUpdate().First().Delay(TimeSpan.FromSeconds(2)).Subscribe(_ =>
            {
                SceneManager.LoadScene(num);
            }).AddTo(this);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.RightControl) && Input.GetKeyDown(KeyCode.P))
            {
                SystemActions.onSwitchScene?.Invoke();
                Observable.EveryUpdate().First().Delay(TimeSpan.FromSeconds(2)).Subscribe(_ =>
                {
                    int currentSceneName = SceneManager.GetActiveScene().buildIndex;
                    SceneManager.LoadScene(currentSceneName);
                }).AddTo(this);
            }
        }
    }
}