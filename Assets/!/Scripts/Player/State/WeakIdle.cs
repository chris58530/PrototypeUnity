using _.Scripts.Tools;
using UnityEngine;
using UnityHFSM;

namespace _.Scripts.Player.State
{
    public class WeakIdle : StateBase<PlayerState>
    {
        private Animator _animator;
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;
        private PlayerAttackSystem _attackSystem;

        public WeakIdle(PlayerInput playerInput,
            PlayerController playerController, Animator animator,
            PlayerAttackSystem attackSystem,
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
            Debug.Log("WeakIdle");

            DebugTools.StateText("WeakIdle");
            _attackSystem.SetWeakTime();
            _animator.CrossFade(Animator.StringToHash("Idle"), 2f);
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