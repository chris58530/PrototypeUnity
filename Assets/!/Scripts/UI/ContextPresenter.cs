using System;
using _.Scripts.Event;
using _.Scripts.Tools;
using UniRx;
using UnityEngine;

namespace _.Scripts.UI
{
    public class ContextPresenter : Singleton<ContextPresenter>
    {
        private ContextView _view;
        private ContextModel _model;
        
        protected override void Awake()
        {
            base.Awake();
            _view = GetComponentInChildren<ContextView>();
            _model = GetComponentInChildren<ContextModel>();
            
        }
        private void Start()
        {
            _model.abilityValue.Subscribe(_ =>
                {
                    Debug.Log("abilityValue 改變");
                }).AddTo(this);
            _model.hpValue.Subscribe(_ =>
                {
                    Debug.Log("hpValue 改變");
                }).AddTo(this);
        }
        [ContextMenu("UseAbility")]
        public void UseAbility()
        {
            _model.abilityValue.Value -= 1;
            _view.UpdateAbility(_model.abilityValue.Value, _model.MaxAbility);
        }

        public float GetAilityCount()
        {
            return _model.abilityValue.Value;
        }
        public void GetHurt(float damage)
        {
            _model.hpValue.Value -= damage;
            if (_model.hpValue.Value <= 0)
            {
                PlayerActions.onPlayerDead?.Invoke();
            }
            _view.UpdateHp(_model.hpValue.Value, _model.MaxHp);
        }
      
    }
}