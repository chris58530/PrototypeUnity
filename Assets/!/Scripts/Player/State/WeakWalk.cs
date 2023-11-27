using _.Scripts.Tools;
using TMPro;
using UnityEngine;
using UnityHFSM;

namespace _.Scripts.Player.State
{
    public class WeakWalk : StateBase<PlayerState>
    {
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;
        private readonly Animator _animator;

        public WeakWalk(
            PlayerInput playerInput,
            PlayerController controller, Animator animator,
            bool needsExitTime,
            bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _input = playerInput;
            _controller = controller;
            _animator = animator;
        }

        public override void OnEnter()
        {
            DebugTools.StateText("WeakWalk");
        }

        public override void OnLogic()
        {
            if (_input.Move)
                _controller.WeakMove(_input);

            _controller.Fall();
        }

        public override void OnExit()
        {
        }
    }
}