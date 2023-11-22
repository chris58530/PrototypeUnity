using System;
using _.Scripts.Tools;
using TMPro;
using UniRx;
using UnityEngine;
using UnityHFSM;

namespace _.Scripts.Player.State
{
    public class PlayerMultiDash : StateBase<PlayerState>
    {
        private readonly PlayerController _controller;
        private Timer _timer;
        private PlayerCombo _combo;
        private Animator _animator;
        public PlayerMultiDash(PlayerController controller,
            Animator animator, PlayerCombo combo,
            bool needsExitTime, bool isGhostState = false) : base(
            needsExitTime, isGhostState)
        {
            _controller = controller;
            _animator = animator;
            _combo = combo;
        }

        public override void OnEnter()
        {
            //debug
            DebugTools.StateText("MultiDash");

            PlayerWeapon.weaponType = WeaponType.Multi;

            _timer = new Timer();
            
            
            //Action
            _controller.MultiDash();

            // AudioManager.Instance.PlaySFX("Dash");
            Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(_controller.dashTime / 2)).First().Subscribe(_ =>
            {
                // AudioManager.Instance.PlaySFX("DashChance");
            });
            _controller.Fall();

        }

        public override void OnLogic()
        {
            if (_timer.Elapsed > _controller.MultiDashTime/1.5f)
                fsm.StateCanExit();
        }


        public override void OnExit()
        {
        }
    }
}