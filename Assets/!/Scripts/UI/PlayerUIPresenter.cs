using System;
using _.Scripts.Event;
using _.Scripts.Player;
using _.Scripts.Tools;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _.Scripts.UI
{
    public class PlayerUIPresenter : Singleton<PlayerUIPresenter>
    {
        private PlayerBase _player;
        private PlayerUIView _view;

        protected override void Awake()
        {
            base.Awake();
            _player = FindObjectOfType<PlayerBase>();
            _view = FindObjectOfType<PlayerUIView>();
        }

        private void Start()
        {
            _player.currentHpValue.Subscribe(_ =>
            {
                DebugTools.HpText(_player.currentHpValue.Value);
                _view.UpdateHp(_player.currentHpValue.Value, _player.maxHpValue);
            }).AddTo(this);

            _player.currentSwordLevelValue.Subscribe(_ =>
            {
                DebugTools.LevelText(_player.currentSwordLevelValue.Value);
                _view.UpdateLevel(_player.currentSwordLevelValue.Value, _player.maxSwordLevelValue);
            }).AddTo(this);
            _player.currentShieldValue.Subscribe(_ =>
            {
                DebugTools.ShieldText(_player.currentShieldValue.Value);
                _view.UpdateSheld(_player.currentShieldValue.Value);

            }).AddTo(this);
       
        }
    }
}