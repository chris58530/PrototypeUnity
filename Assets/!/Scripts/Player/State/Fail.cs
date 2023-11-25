using _.Scripts.Tools;
using TMPro;
using UnityEngine;
using UnityHFSM;

namespace _.Scripts.Player.State
{
    public class Fail : StateBase<PlayerState>
    {
        private Animator _animator;
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;
        private Timer _timer;
        private readonly PlayerAttackSystem _attackSystem;

        public Fail(PlayerInput playerInput,
            PlayerController playerController,
            Animator animator, PlayerAttackSystem attackSystem,
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
            _timer = new Timer();
            _attackSystem.AttackChancePreview(Color.red);
            _attackSystem.ResetSkill();
            DebugTools.StateText("DashFail");
        }

        public override void OnLogic()
        {
            if (_timer.Elapsed > _attackSystem.failTime)
                fsm.StateCanExit();
            _controller.Fall();
        }

        public override void OnExit()
        {
            _attackSystem.AttackChancePreview(Color.white);
        }
    }
}