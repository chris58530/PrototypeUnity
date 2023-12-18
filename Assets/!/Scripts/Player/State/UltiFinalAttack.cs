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
        private UltimateSystem _ultimateSystem;

        public UltiFinalAttack(PlayerInput playerInput,
            PlayerController playerController,
            Animator animator, UltimateSystem ultimateSystem,
            bool needsExitTime,
            bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _input = playerInput;
            _controller = playerController;
            _animator = animator;
            _ultimateSystem = ultimateSystem;
        }

        public override void OnEnter()
        {
            DebugTools.StateText("UltiFinalAttack");
            _timer = new Timer();
            _ultimateSystem.UseFinalUltimate();
            _animator.Play("UltimateFinalAttack");
            _ultimateSystem.AttackChancePreview(Color.white);
            _controller.Roll();
        }

        public override void OnLogic()
        {
            if (_timer.Elapsed > _animator.GetCurrentAnimatorClipInfo(0).Length)
            {
                _ultimateSystem.finishUltimate = true;
                fsm.StateCanExit();
            }
        }

        public override void OnExit()
        {
            _ultimateSystem.CancelUltimate();

            _ultimateSystem.finishUltimate = false;

            _animator.CrossFade(Animator.StringToHash("Idle"), 0.1f);
        }
    }
}