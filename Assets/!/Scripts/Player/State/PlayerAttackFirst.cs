using _.Scripts.Tools;
using UnityEngine;
using UnityHFSM;

namespace _.Scripts.Player.State
{
    public class PlayerAttackFirst : StateBase<PlayerState>
    {
        private Animator _animator;
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;
        private Timer _timer;

        public PlayerAttackFirst(PlayerInput playerInput,
            PlayerController playerController,
            Animator animator,
            bool needsExitTime,
            bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _input = playerInput;
            _controller = playerController;
            _animator = animator;
        }

        public override void OnEnter()
        {
            DebugTools.StateText("AttackFirst");
            _timer = new Timer();
            _animator.CrossFade(Animator.StringToHash("Attack1"), 0.1f);
            _controller.Attack(_animator.GetCurrentAnimatorClipInfo(0).Length);

            Debug.Log(_animator.GetCurrentAnimatorStateInfo(0).length);

        }

        public override void OnLogic()
        {
            if (_timer.Elapsed > _controller.attackTime)
                fsm.StateCanExit();
        }

        public override void OnExit()
        {
            _controller.CancelAttack();
            _animator.CrossFade(Animator.StringToHash("Idle"), 0.1f);

        }
    }
}