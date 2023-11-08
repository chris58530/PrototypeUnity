using UnityEngine;
using UnityHFSM;
using System;
using UniRx;

namespace _.Scripts.Player.State
{
    public class PlayerWalk : StateBase<PlayerState>
    {
        private readonly PlayerMapInput _input;
        private readonly PlayerController _controller;
        private Animator _animator;

        public PlayerWalk(
            PlayerMapInput playerMapInput,
            PlayerController controller, Animator animator,
            bool needsExitTime,
            bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _input = playerMapInput;
            _controller = controller;
            _animator = animator;
        }

        public override void OnEnter()
        {
            _animator.Play("Walk");
            AudioManager.Instance.PlaySFX2("Walk");
        }

        public override void OnLogic()
        {

            Vector2 getInput = _input.MoveVector;
            Vector3 dir = new Vector3(getInput.x, 0, getInput.y);
            _controller.Move(dir);

            if (_input.IsPressedDash)
                _controller.ShowDashDirection(true);

            _controller.Fall();
        }

        public override void OnExit()
        {
            AudioManager.Instance.StopPlaySFX2();
            _controller.ShowDashDirection(false);            
        }
    }
}