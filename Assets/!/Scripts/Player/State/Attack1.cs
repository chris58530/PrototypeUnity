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
            _animator.CrossFade(Animator.StringToHash("Attack1"), 0.1f);

            _attackSystem.Attack();
        }

        public override void OnLogic()
        {
            if (_timer.Elapsed >  _attackSystem.AttackTime(0))
                fsm.StateCanExit();
        }

        public override void OnExit()
        {

            _attackSystem.CancelAttack();
        }
    }
}