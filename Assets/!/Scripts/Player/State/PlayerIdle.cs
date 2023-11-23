using System;
using _.Scripts.Tools;
using TMPro;
using UnityEngine;
using UnityHFSM;
using UniRx;

namespace _.Scripts.Player.State
{
    public class PlayerIdle : StateBase<PlayerState>
    {
        private Animator _animator;
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;

        public PlayerIdle(PlayerInput playerInput,
            PlayerController playerController, Animator animator,
            bool needsExitTime,
            bool isGhostState = false) : base(needsExitTime,
            isGhostState)
        {
            _input = playerInput;
            _controller = playerController;
            _animator = animator;
        }

        public override void OnEnter()
        {
            //debug
            DebugTools.StateText("Idle");

            _animator.CrossFade(Animator.StringToHash("Idle"), 2f);
        }

        public override void OnLogic()
        {
            _controller.Fall();
        }

        public override void OnExit()
        {
            _controller.ShowDashDirection(false);
        }
    }
}