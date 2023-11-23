using System;
using _.Scripts.Event;
using _.Scripts.Player;
using _.Scripts.Tools;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _.Scripts.UI
{
    public class UIPresenter : Singleton<UIPresenter>
    {
        private PlayerBase _player;
        private ContextView _view;

        protected override void Awake()
        {
            base.Awake();
            _player = FindObjectOfType<PlayerBase>();
            _view = FindObjectOfType<ContextView>();
        }

        private void Start()
        {
            _player.currentHpValue.Subscribe(_ =>
            {
                DebugTools.HpText(_player.currentHpValue.Value);
                _view.UpdateHp(_player.currentHpValue.Value, _player.maxHpValue);
            }).AddTo(this);

            _player.currentSkillValue.Subscribe(_ =>
            {
                DebugTools.SkillText(_player.currentSkillValue.Value);
                _view.UpdateSkill(_player.currentSkillValue.Value, _player.maxSkillValue);
            }).AddTo(this);
        }
    }
}