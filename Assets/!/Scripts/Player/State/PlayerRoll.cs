using System;
using _.Scripts.Event;
using _.Scripts.Tools;
using TMPro;
using UniRx;
using UnityEngine;
using UnityHFSM;

namespace _.Scripts.Player.State
{
    public class PlayerRoll : StateBase<PlayerState>
    {
        private readonly PlayerController _controller;
        private Timer _timer;
        private PlayerCombo _combo;
        private Animator _animator;

        public PlayerRoll(PlayerController controller,
            Animator animator, PlayerCombo combo,
            bool needsExitTime, bool isGhostState = false) : base(
            needsExitTime, isGhostState)
        {
            _controller = controller;
            _animator = animator;
            _combo = combo;
        }

        public override void OnEnter()
        {
            DebugTools.StateText("Roll");
            _timer = new Timer();
            
            _animator.Play(Animator.StringToHash("Dash"));

            _controller.Roll();


        }

        public override void OnLogic()
        {
            if (_timer.Elapsed > _controller.rollTime )
                fsm.StateCanExit();
            _controller.Fall();

        }

        public override void OnExit()
        {
            
        }
    }
}