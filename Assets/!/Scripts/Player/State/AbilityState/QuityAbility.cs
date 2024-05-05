using _.Scripts.Player.Props;
using _.Scripts.Tools;
using UnityEngine;
using UnityHFSM;

namespace @_.Scripts.Player.State.AbilityState
{
    public class QuityAbility : StateBase<PlayerState>
    {
        private Animator _animator;
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;
        private AttackSystem _attackSystem;
        private PlayerBase _playerBase;
        private float _insertTime;
        private readonly AbilityWeapon _abilityWeapon;
        private Timer _timer;

        public QuityAbility(PlayerInput playerInput,
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
            _abilityWeapon.ChangeAbility(AbilityWeapon.AbilityType.None);
            AbilityWeaponAnimator.Instance?.PlayAnimation(AbilityWeaponAnimator.AnimationName.Swallow);

            // _animator.Play("Eat");
        }

        public override void OnLogic()
        {
            if (_timer.Elapsed > 1.4f)
            {
                fsm.StateCanExit();
            }
            if (_input.Move)
                _controller.Move(_input);
        }

        public override void OnExit()
        {
        }
    }
}