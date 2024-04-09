using _.Scripts.Tools;
using UnityEngine;
using UnityHFSM;

namespace @_.Scripts.Player.State
{
    public class Idle : StateBase<PlayerState>
    {
        private Animator _animator;
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;
        private AttackSystem _attackSystem;

        public Idle(PlayerInput playerInput,
            PlayerController playerController, Animator animator,
            AttackSystem attackSystem,
            bool needsExitTime,
            bool isGhostState = false) : base(needsExitTime,
            isGhostState)
        {
            _input = playerInput;
            _controller = playerController;
            _animator = animator;
            _attackSystem = attackSystem;
        }

        public override void OnEnter()
        {
            //debug
            _attackSystem.finishAttack = false;
         

            DebugTools.StateText("Idle");
            _animator.CrossFade(Animator.StringToHash("Idle"), 0.2f);
        }

        public override void OnLogic()
        {
            _controller.Fall();
        }

        public override void OnExit()
        {
        }
    }
}