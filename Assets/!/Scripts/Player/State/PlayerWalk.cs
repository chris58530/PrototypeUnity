using UnityEngine;
using UnityHFSM;
using System;
using TMPro;
using UniRx;

namespace _.Scripts.Player.State
{
    public class PlayerWalk : StateBase<PlayerState>
    {
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;
        private readonly Animator _animator;

        public PlayerWalk(
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
            //debug
            TMP_Text t = GameObject.Find("StateText").GetComponent<TMP_Text>();
            t.text = "Walk";
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
            _controller.ShowDashDirection(false);            
        }
    }
}