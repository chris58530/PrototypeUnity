using System;
using _.Scripts.Event;
using _.Scripts.Tools;
using TMPro;
using UniRx;
using UnityEngine;
using UnityHFSM;

namespace _.Scripts.Player.State
{
    public class PlayerSingleDash : StateBase<PlayerState>
    {
        private readonly PlayerController _controller;
        private Timer _timer;
        private PlayerCombo _combo;
        private Animator _animator;

        public PlayerSingleDash(PlayerController controller,
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
            DebugTools.StateText("SingleDash");

            PlayerWeapon.weaponType = WeaponType.Single;
            PlayerActions.onPlayerAttack?.Invoke(_controller.SingleDashTime);
            // _combo.combo += 1;
            _timer = new Timer();
            
            /*Performance*/
            _controller.SingleDash();
            _animator.CrossFade(Animator.StringToHash("Attack") ,0);

            // AudioManager.Instance.PlaySFX("Dash");
            Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(_controller.dashTime / 2)).First().Subscribe(_ =>
            {
                // AudioManager.Instance.PlaySFX("DashChance");
            });
        }

        public override void OnLogic()
        {
            if (_timer.Elapsed > _controller.SingleDashTime/1.5f)
                fsm.StateCanExit();
            _controller.Fall();
        }


        public override void OnExit()
        {
            _animator.CrossFade(Animator.StringToHash("Idle") ,2f);
        }
    }
}