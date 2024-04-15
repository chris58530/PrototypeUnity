using _.Scripts.Player.Props;
using _.Scripts.Tools;
using TMPro;
using UnityEngine;
using UnityHFSM;
using UniRx;

namespace _.Scripts.Player.State
{
    public class Chance1 : StateBase<PlayerState>
    {
        private Animator _animator;
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;
        private AttackSystem _attackSystem;

        public Chance1(PlayerInput playerInput,
            PlayerController playerController,
            Animator animator, AttackSystem attackSystem,
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
            DebugTools.StateText("ChanceFirst");
        }

        public override void OnLogic()
        {
         
            if (_input.Move)
                _controller.Move(_input);

            _controller.Fall();
        }

        public override void OnExit()
        {
            // _animator.CrossFade(Animator.StringToHash("Q1ToIdle"),0f);
            _animator.Play("Q1ToIdle");
        }
    }
}