using System;
using _.Scripts.Player.Props;
using _.Scripts.Tools;
using TMPro;
using UnityEngine;
using UnityHFSM;
using UniRx;

namespace _.Scripts.Player.State
{
    public class Dead : StateBase<PlayerState>
    {
        private Animator _animator;
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;
        private AttackSystem _attackSystem;

        public Dead(PlayerInput playerInput,
            PlayerController playerController, Animator animator,
            AttackSystem attackSystem,
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
            //debug
         

            _animator.CrossFade(Animator.StringToHash("Die"), 0.2f);
        }

        public override void OnLogic()
        {
        }

        public override void OnExit()
        {
        }
    }
}