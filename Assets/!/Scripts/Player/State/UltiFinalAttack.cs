using _.Scripts.Tools;
using UnityEngine;
using UnityHFSM;

namespace @_.Scripts.Player.State
{
    public class UltiFinalAttack : StateBase<PlayerState>
    {
        private readonly Animator _animator;
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;
        private Timer _timer;
        private readonly UltimateSystem _ultimateSystem;
        private readonly AttackSystem _attackSystem;

        public UltiFinalAttack(PlayerInput playerInput,
            PlayerController playerController,
            Animator animator, UltimateSystem ultimateSystem,
            AttackSystem attackSystem,
            bool needsExitTime,
            bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _input = playerInput;
            _controller = playerController;
            _animator = animator;
            _ultimateSystem = ultimateSystem;
            _attackSystem = attackSystem;
        }

        public override void OnEnter()
        {
            DebugTools.StateText("UltiFinalAttack");
            _timer = new Timer();
            _ultimateSystem.UseFinalUltimate();
            _ultimateSystem.AttackChancePreview(Color.white);
            _controller.Roll();
            _animator.Play("UltimateFinalAttack");
        }

        public override void OnLogic()
        {
            if (_timer.Elapsed > 1f)
            {
                Debug.Log("finishUltimate = true");

                _ultimateSystem.finishUltimate = true;
                fsm.StateCanExit();
            }
        }

        public override void OnExit()
        {
            _ultimateSystem.CancelUltimate();


            Debug.Log("--------ResetUltimate!--------");
            _ultimateSystem.UltimateTimer(false);
            _ultimateSystem.finishUltimate = false;
            _ultimateSystem.finishUltiAttack = false;
            _ultimateSystem.ultimateCount = 0;
            _attackSystem.SetSwordLevel(0);
        }
    }
}