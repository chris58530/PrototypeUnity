using _.Scripts.Tools;
using _.Scripts.UI;
using UnityEngine;
using UnityHFSM;

namespace @_.Scripts.Player.State
{
    public class Chance3 : StateBase<PlayerState>
    {
        private Animator _animator;
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;
        private PlayerBase _playerBase;
        private AttackSystem _attackSystem;        private Timer _timer;


        public Chance3(PlayerInput playerInput,
            PlayerController playerController,
            Animator animator,
            PlayerBase playerBase, AttackSystem attackSystem,
            bool needsExitTime,
            bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _input = playerInput;
            _controller = playerController;
            _animator = animator;
            _playerBase = playerBase;
            _attackSystem = attackSystem;
        }

        public override void OnEnter()
        {
            //debug
            _timer = new Timer();
            DebugTools.StateText("ChanceThird");
            _attackSystem.AttackChancePreview(Color.yellow);
            _animator.CrossFade(Animator.StringToHash("Idle"), 0.8f);
        }

        public override void OnLogic()
        {
            if (_timer.Elapsed > 0.5f) fsm.StateCanExit();

            if (_input.Move)
                _controller.Move(_input);

            _controller.Fall();
        }

        public override void OnExit()
        {
            _attackSystem.AttackChancePreview(Color.white);
        }
    }
}