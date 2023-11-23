using _.Scripts.Tools;
using TMPro;
using UnityEngine;
using UnityHFSM;
using UniRx;

namespace _.Scripts.Player.State
{
    public class PlayerAttackChanceFirst : StateBase<PlayerState>
    {
        private Animator _animator;
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;

        public PlayerAttackChanceFirst(PlayerInput playerInput,
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
            //debug
            DebugTools.StateText("ChanceFirst");
            _controller.AttackChancePreview(Color.yellow);
        }

        public override void OnLogic()
        {     Vector2 getInput = _input.MoveVector;
                     Vector3 dir = new Vector3(getInput.x, 0, getInput.y);
                     _controller.Move(dir);

            _controller.Fall();
        }

        public override void OnExit()
        {
            _controller.AttackChancePreview(Color.white);

        }
    }
}