using _.Scripts.Tools;
using TMPro;
using UnityEngine;
using UnityHFSM;

namespace _.Scripts.Player.State
{
    public class PlayerFail : StateBase<PlayerState>
    {
        private Animator _animator;
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;
        private Timer _timer;
        private PlayerCombo _combo;

        public PlayerFail(PlayerInput playerInput,
            PlayerController playerController,
            Animator animator, PlayerCombo combo,
            bool needsExitTime,
            bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _input = playerInput;
            _controller = playerController;
            _animator = animator;
            _combo = combo;
        }

        public override void OnEnter()
        {
            _timer = new Timer();
            _combo.combo = 0;
            _controller.AttackChancePreview(Color.red);

            DebugTools.StateText("DashFail");
        }

        public override void OnLogic()
        {
            if (_timer.Elapsed > _controller.dashFailTime)
                fsm.StateCanExit();
            _controller.Fall();
        }

        public override void OnExit()
        {
            _controller.AttackChancePreview(Color.white);

        }
    }
}