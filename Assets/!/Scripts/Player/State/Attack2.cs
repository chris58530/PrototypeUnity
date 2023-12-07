using _.Scripts.Tools;
using UnityEngine;
using UnityHFSM;

namespace _.Scripts.Player.State
{
    public class Attack2 : StateBase<PlayerState>
    {
        private Animator _animator;
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;
        private Timer _timer;
        private AttackSystem _attackSystem;

        public Attack2(PlayerInput playerInput,
            PlayerController playerController,
            Animator animator, AttackSystem attackSystem,
            bool needsExitTime,
            bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _input = playerInput;
            _controller = playerController;
            _animator = animator;
            _attackSystem = attackSystem;
        }

        public override void OnEnter()
        {
            DebugTools.StateText("AttackSecond");

            _timer = new Timer();
            _animator.CrossFade(Animator.StringToHash("Attack2"), 0.1f);
            _attackSystem.Attack(_animator.GetCurrentAnimatorClipInfo(0).Length);

            Debug.Log(_animator.GetCurrentAnimatorStateInfo(0).length);
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