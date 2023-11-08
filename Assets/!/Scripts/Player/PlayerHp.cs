using System;
using _.Scripts.Event;
using _.Scripts.UI;
using UnityEngine;

namespace _.Scripts.Player
{
    public class PlayerHp : MonoBehaviour, IDamageable
    {
        public bool getAttack;
        public bool Dead;


        private void OnEnable()
        {
            PlayerActions.onPlayerDead += OnDied;
        }

        private void OnDisable()
        {
            PlayerActions.onPlayerDead -= OnDied;
        }

        public void OnTakeDamage(int value)
        {
            ContextPresenter.Instance.GetHurt(value);
            getAttack = true;
        }

        public void OnDied()
        {
            Dead = true;
            Invoke(nameof(ChangeScene), 5);
        }

        void ChangeScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}