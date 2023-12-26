using System;
using _.Scripts.Tools;
using TMPro;
using UnityEngine;
using UnityHFSM;
using UniRx;

namespace _.Scripts.Player.State
{
    public class Idle : StateBase<PlayerState>
    {
        private Animator _animator;
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;
        private AttackSystem _attackSystem;
        private UltimateSystem _ultimateSystem;

        public Idle(PlayerInput playerInput,
            PlayerController playerController, Animator animator,
            AttackSystem attackSystem, UltimateSystem ultimateSystem,
            bool needsExitTime,
            bool isGhostState = false) : base(needsExitTime,
            isGhostState)
        {
            _input = playerInput;
            _controller = playerController;
            _animator = animator;
            _attackSystem = attackSystem;
            _ultimateSystem = ultimateSystem;

        }

        public override void OnEnter()
        {
            //debug
            DebugTools.StateText("Idle");
            _attackSystem.finishAttack = false;   
            _ultimateSystem.finishUltimate = false;

            _animator.CrossFade(Animator.StringToHash("Idle"), 0.3f);
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