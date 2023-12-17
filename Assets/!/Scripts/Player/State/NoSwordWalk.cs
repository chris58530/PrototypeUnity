using _.Scripts.Tools;
using TMPro;
using UnityEngine;
using UnityHFSM;

namespace @_.Scripts.Player.State
{
    public class NoSwordWalk : StateBase<PlayerState>
    {
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;
        private readonly Animator _animator;

        public NoSwordWalk(
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
            _animator.CrossFade(Animator.StringToHash("EmptyRun"), 0.5f);

            DebugTools.StateText("NoSwordWalk");
        }

        public override void OnLogic()
        {

            if (_input.Move)
                _controller.Move(_input);

       

            _controller.Fall();
        }

        public override void OnExit()
        {
        }
    }
}