using System;
using TMPro;
using UnityEngine;
using UnityHFSM;
using UniRx;

namespace _.Scripts.Player.State
{
    public class PlayerIdle : StateBase<PlayerState>
    {
        private Animator _animator;
        private readonly PlayerMapInput _input;
        private readonly PlayerController _controller;

        public PlayerIdle(PlayerMapInput playerMapInput,
            PlayerController playerController, Animator animator,
            bool needsExitTime,
            bool isGhostState = false) : base(needsExitTime,
            isGhostState)
        {
            _input = playerMapInput;
            _controller = playerController;
            _animator = animator;
        }

        public override void OnEnter()
        {
            //debug
            TMP_Text t = GameObject.Find("StateText").GetComponent<TMP_Text>();
            t.text = "Idle";
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