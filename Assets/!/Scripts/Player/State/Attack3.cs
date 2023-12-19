using _.Scripts.Tools;
using _.Scripts.UI;
using UnityEngine;
using UnityHFSM;

namespace @_.Scripts.Player.State
{
    public class Attack3 : StateBase<PlayerState>
    {
        private Animator _animator;
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;
        private Timer _timer;
        private AttackSystem _attackSystem;
        private float aniTime;

        public Attack3(PlayerInput playerInput,
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
            DebugTools.StateText("AttackThird");


            _timer = new Timer();
            _animator.CrossFade(Animator.StringToHash("Attack3"), 0.1f);
            _attackSystem.Attack(_animator.GetCurrentAnimatorClipInfo(0).Length);
            _attackSystem.IncreaseSwordLevel();
            aniTime = _animator.GetCurrentAnimatorClipInfo(0).Length;

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