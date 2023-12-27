using _.Scripts.Tools;
using UnityEngine;
using UnityHFSM;

namespace @_.Scripts.Player.State
{
    public class UltiChance : StateBase<PlayerState>
    {
        private readonly Animator _animator;
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;
        private Timer _timer;
        private UltimateSystem _ultimateSystem;


        public UltiChance(PlayerInput playerInput,
            PlayerController playerController,
            Animator animator, UltimateSystem ultimateSystem,
            bool needsExitTime,
            bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _input = playerInput;
            _controller = playerController;
            _animator = animator;
            _ultimateSystem = ultimateSystem;
        }

        public override void OnEnter()
        {
            DebugTools.StateText("UltiChance");
            _timer = new Timer();
            _ultimateSystem.UltiChanceTimer();
            _animator.CrossFade(Animator.StringToHash("Idle"), 0.1f);
            _ultimateSystem.AttackChancePreview(Color.green);
        }

        public override void OnLogic()
        {
            if (_input.Move)
                _controller.Move(_input);

            _controller.Fall();
        }

        public override void OnExit()
        {
            _ultimateSystem.finishUltimate = false;
        }
    }
}