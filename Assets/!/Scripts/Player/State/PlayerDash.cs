using System;
using _.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityHFSM;

namespace _.Scripts.Player.State
{
    public class PlayerDash : StateBase<PlayerState>
    {
        private readonly PlayerController _controller;
        private Timer _timer;
        private PlayerCombo _combo;
        private Animator _animator;

        public PlayerDash(PlayerController controller,
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
            //debug
            TMP_Text t = GameObject.Find("StateText").GetComponent<TMP_Text>();
            t.text = "Dash";
            _animator.Play("Dash");

            _combo.combo += 1;
            _timer = new Timer();
            _controller.Dash();
        }

        public override void OnLogic()
        {
            if (_timer.Elapsed > _controller.dashTime)
                fsm.StateCanExit();
        }


        public override void OnExit()
        {
        }
    }
}