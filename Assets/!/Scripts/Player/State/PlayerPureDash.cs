using UniRx;
using UnityEngine;
using UnityHFSM;
using TMPro;
using System;

namespace _.Scripts.Player.State
{
    public class PlayerPureDash : StateBase<PlayerState>
    {
        private readonly PlayerController _controller;
        private Timer _timer;
        private PlayerCombo _combo;
        private Animator _animator;

        public PlayerPureDash(PlayerController controller,
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
            t.text = "PureDash";
            Debug.Log("PureDash");

            _combo.combo += 1;
            _timer = new Timer();
            //Action
            _controller.PureDash();
            _animator.Play("Dash");
            // AudioManager.Instance.PlaySFX("Dash");
            Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(_controller.dashTime / 2)).First().Subscribe(_ =>
            {
                // AudioManager.Instance.PlaySFX("DashChance");
            });
        }

        public override void OnLogic()
        {
            if (_timer.Elapsed > _controller.PureDashTime / 1.5f)
                fsm.StateCanExit();
            _controller.Fall();

        }


        public override void OnExit()
        {
        }
    }
}