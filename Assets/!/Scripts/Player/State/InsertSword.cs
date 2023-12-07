using _.Scripts.Tools;
using UnityEngine;
using UnityHFSM;

namespace @_.Scripts.Player.State
{
    public class InsertSword : StateBase<PlayerState>
    {
        private Animator _animator;
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;
        private AttackSystem _attackSystem;
        private PlayerBase _playerBase;
        private float _insertTime;

        public InsertSword(PlayerInput playerInput,
            PlayerController playerController, Animator animator, AttackSystem attackSystem,
            PlayerBase playerBase,
            bool needsExitTime,
            bool isGhostState = false) : base(needsExitTime,
            isGhostState)
        {
            _input = playerInput;
            _controller = playerController;
            _animator = animator;
            _attackSystem = attackSystem;
            _playerBase = playerBase;
        }

        public override void OnEnter()
        {
            //debug
            DebugTools.StateText("InsertSword");
            _insertTime = 0;
        }

        public override void OnLogic()
        {
            _insertTime += Time.deltaTime;
            if (_insertTime > 2)
            {
                fsm.StateCanExit();

                _attackSystem.NoSword();
                _playerBase.currentShieldValue.Value = 3;
            }
            else if (Input.GetKeyUp(KeyCode.Q))
            {
                fsm.StateCanExit();
            }

            _controller.Fall();
        }

        public override void OnExit()
        {
        }
    }
}