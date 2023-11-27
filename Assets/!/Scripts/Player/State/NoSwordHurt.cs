using _.Scripts.Event;
using _.Scripts.Tools;
using UnityEngine;
using UnityHFSM;

namespace @_.Scripts.Player.State
{
    public class NoSwordHurt : StateBase<PlayerState>
    {
        private readonly PlayerController _controller;
        private readonly PlayerInput _input;

        private Animator _animator;
        private Timer _timer;
        private PlayerAttackSystem _attackSystem;
        private PlayerBase _playerBase;

        public NoSwordHurt(PlayerInput playerInput,
            PlayerController playerController,
            Animator animator, PlayerAttackSystem attackSystem
            , PlayerBase playerBase,
            bool needsExitTime, bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _controller = playerController;
            _animator = animator;
            _attackSystem = attackSystem;
            _playerBase = playerBase;
            _input = playerInput;
        }

        public override void OnEnter()
        {
            DebugTools.StateText("NoSwordHurt");
            PlayerActions.onPlayerHurt?.Invoke();
            _playerBase.getHurt = false;
        }

        public override void OnLogic()
        {
            if (_input.Move)
                _controller.Move(_input);
            _controller.Fall();
        }

        public override void OnExit()
        {
        }
    }
}