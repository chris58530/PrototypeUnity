using _.Scripts.Player.Props;
using _.Scripts.Tools;
using UnityEngine;
using UnityHFSM;

namespace @_.Scripts.Player.State
{
    public class Chance1ToIdle : StateBase<PlayerState>
    {
        private Animator _animator;
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;
        private AttackSystem _attackSystem;
        private Timer _timer;

        public Chance1ToIdle(PlayerInput playerInput,
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
            _timer = new Timer();

            DebugTools.StateText("Q1ToIdle");
            _animator.CrossFade(Animator.StringToHash("Q1ToIdle"),0f);

        }

        public override void OnLogic()
        {
         
            if (_timer.Elapsed >0.2f)
                fsm.StateCanExit();

            _controller.Fall();
        }

        public override void OnExit()
        {

        }
    }
}