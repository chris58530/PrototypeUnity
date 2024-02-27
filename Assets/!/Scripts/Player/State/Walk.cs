using UnityEngine;
using UnityHFSM;
using System;
using TMPro;
using UniRx;

namespace _.Scripts.Player.State
{
    public class Walk : StateBase<PlayerState>
    {
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;
        private readonly Animator _animator;
        private AttackSystem _attackSystem;

        public Walk(
            PlayerInput playerInput,
            PlayerController controller, Animator animator, AttackSystem attackSystem,
            bool needsExitTime,
            bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _input = playerInput;
            _controller = controller;
            _animator = animator;
            _attackSystem = attackSystem;
        }

        public override void OnEnter()
        {
            //debug
            TMP_Text t = GameObject.Find("StateText").GetComponent<TMP_Text>();
            t.text = "Walk";
            _animator.CrossFade(Animator.StringToHash("Walk"), 0.2f);
            AudioManager.Instance.PlaySFX2("Walk");

        }

        public override void OnLogic()
        {
            if (_input.Move)
                _controller.Move(_input);


            _controller.Fall();
        }

        public override void OnExit()
        {
            AudioManager.Instance.StopPlaySFX2();

        }
    }
}