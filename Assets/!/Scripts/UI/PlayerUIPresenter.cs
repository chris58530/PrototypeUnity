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
        private PlayerUI _view;

        protected override void Awake()
        {
            base.Awake();
            _player = FindObjectOfType<PlayerBase>();
            _view = FindObjectOfType<PlayerUI>();
        }

        private void Start()
        {
            _player.currentHpValue.Subscribe(_ =>
            {
                DebugTools.HpText(_player.currentHpValue.Value);
                _view.UpdateHp(_player.currentHpValue.Value, _player.maxHpValue);
            }).AddTo(this);

         
       
        }
    }
}