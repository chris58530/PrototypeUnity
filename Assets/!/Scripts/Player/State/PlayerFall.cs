using UnityEngine;
using UnityHFSM;

namespace _.Scripts.Player.State
{
    public class PlayerFall : StateBase<PlayerState>
    {
        private Animator _animator;
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;

        public PlayerFall(PlayerInput playerInput,
            PlayerController playerController,
            bool needsExitTime,
            bool isGhostState = false) : base(needsExitTime,
            isGhostState)
        {
            _input = playerInput;
            _controller = playerController;
        }

        public override void OnEnter()
        {
        }

        public override void OnLogic()
        {
            if (_input.IsPressedDash)
                _controller.ShowDashDirection(true);
        }

        public override void OnExit()
        {
            _controller.ShowDashDirection(false);
        }
    }
}