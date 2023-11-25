using _.Scripts.Tools;
using UnityEngine;
using UnityHFSM;

namespace _.Scripts.Player.State
{
    public class UltimateAttack : StateBase<PlayerState>
    {
        private readonly Animator _animator;
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;
        private Timer _timer;
        private PlayerAttackSystem _attackSystem;

        public UltimateAttack(PlayerInput playerInput,
            PlayerController playerController,
            Animator animator, PlayerAttackSystem attackSystem,
            bool needsExitTime,
            bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _input = playerInput;
            _controller = playerController;
            _animator = animator;            _attackSystem = attackSystem;

        }

        public override void OnEnter()
        {
            DebugTools.StateText("UltimateAttack");
            _timer = new Timer();
            _attackSystem.UseUltimate();
            _animator.Play("UltimateAttack");

        }

        public override void OnLogic()
        {
            if (_timer.Elapsed > _attackSystem.attackTime)
                fsm.StateCanExit();
        }

        public override void OnExit()
        {
            _attackSystem.CancelAttack();
            _animator.CrossFade(Animator.StringToHash("Idle"), 0.1f);
        }
    }
}