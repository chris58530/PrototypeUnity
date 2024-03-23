using _.Scripts.Player.Props;
using UnityEngine;
using UnityHFSM;

namespace @_.Scripts.Player.State.AbilityState
{
    public class DashAbility : StateBase<PlayerState>
    {
        private Animator _animator;
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;
        private AttackSystem _attackSystem;
        private PlayerBase _playerBase;
        private float _insertTime;
        private readonly AbilityWeapon _abilityWeapon;
        private Timer _timer;

        public DashAbility(PlayerInput playerInput,
            PlayerController playerController, Animator animator, AttackSystem attackSystem
            , AbilityWeapon abilityWeapon, PlayerBase playerBase,
            bool needsExitTime,
            bool isGhostState = false) : base(needsExitTime,
            isGhostState)
        {
            _input = playerInput;
            _controller = playerController;
            _animator = animator;
            _attackSystem = attackSystem;
            _abilityWeapon = abilityWeapon;
            _playerBase = playerBase;
        }


        public override void OnEnter()
        {
            _timer = new Timer();

            _attackSystem.FaceMouseInputPosition();
            _controller.Dash(15);
            _attackSystem.Attack();
        }

        public override void OnLogic()
        {
            if (_timer.Elapsed > 0.5f)
            {
                fsm.StateCanExit();
            }
        }

        public override void OnExit()
        {
        }
    }
}