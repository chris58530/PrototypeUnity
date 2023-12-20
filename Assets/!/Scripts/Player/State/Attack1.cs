using _.Scripts.Tools;
using UnityEngine;
using UnityHFSM;
using _.Scripts.Event;

namespace _.Scripts.Player.State
{
    public class Attack1 : StateBase<PlayerState>
    {
        private Animator _animator;
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;
        private Timer _timer;
        private AttackSystem _attackSystem;
        private float aniTime;

        public Attack1(PlayerInput playerInput,
            PlayerController playerController,
            Animator animator,
            AttackSystem attackSystem,
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
            DebugTools.StateText("AttackFirst");
            _timer = new Timer();
            _animator.Play(Animator.StringToHash("Attack1"));
            aniTime = _animator.GetCurrentAnimatorClipInfo(0).Length;
            _attackSystem.Attack(aniTime);
        }

        public override void OnLogic()
        {
            if (_timer.Elapsed > aniTime)
                fsm.StateCanExit();
        }

        public override void OnExit()
        {

            _attackSystem.CancelAttack();
        }
    }
}