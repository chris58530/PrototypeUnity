using _.Scripts.Tools;
using UnityEngine;
using UnityHFSM;

namespace _.Scripts.Player.State
{
    public class PlayerAttackSecond : StateBase<PlayerState>
    {
        private Animator _animator;
        private readonly PlayerMapInput _input;
        private readonly PlayerController _controller;
        private Timer _timer;

        public PlayerAttackSecond(PlayerMapInput playerMapInput,
            PlayerController playerController,
            Animator animator,
            bool needsExitTime,
            bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _input = playerMapInput;
            _controller = playerController;
            _animator = animator;
        }

        public override void OnEnter()
        {
            DebugTools.StateText("AttackSecond");            _controller.Attack();

            _timer = new Timer();
            _animator.CrossFade(Animator.StringToHash("Attack2"), 0.1f);

        }

        public override void OnLogic()
        {
            if (_timer.Elapsed > _controller.attackTime)
                fsm.StateCanExit();
        }

        public override void OnExit()
        {
            _animator.CrossFade(Animator.StringToHash("Idle"), 0.1f);

        }
    }
}