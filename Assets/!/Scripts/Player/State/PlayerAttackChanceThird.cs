using _.Scripts.Tools;
using UnityEngine;
using UnityHFSM;

namespace @_.Scripts.Player.State
{
    public class PlayerAttackChanceThird : StateBase<PlayerState>
    {
        private Animator _animator;
        private readonly PlayerMapInput _input;
        private readonly PlayerController _controller;
        public PlayerAttackChanceThird(PlayerMapInput playerMapInput,
            PlayerController playerController,
            Animator animator,
            bool needsExitTime,
            bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _input = playerMapInput;
            _controller = playerController;
            _animator = animator;
        }

        public override void OnEnter()
        {
            //debug
            DebugTools.StateText("ChanceThird");

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