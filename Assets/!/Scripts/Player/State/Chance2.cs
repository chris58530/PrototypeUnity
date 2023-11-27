using _.Scripts.Tools;
using UnityEngine;
using UnityHFSM;

namespace @_.Scripts.Player.State
{
    public class Chance2 : StateBase<PlayerState>
    {
        private Animator _animator;
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;
        private PlayerAttackSystem _attackSystem;

        public Chance2(PlayerInput playerInput,
            PlayerController playerController,
            Animator animator, PlayerAttackSystem attackSystem,
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
            //debug
            DebugTools.StateText("ChanceScecond");
            _attackSystem.AttackChancePreview(Color.yellow);
        }

        public override void OnLogic()
        {
            if (_input.Move)
            _controller.Move(_input);

            _controller.Fall();
        }

        public override void OnExit()
        {
            _attackSystem.AttackChancePreview(Color.white);
        }
    }
}