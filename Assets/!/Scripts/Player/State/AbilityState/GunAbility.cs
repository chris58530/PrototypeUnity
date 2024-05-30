using System;
using _.Scripts.Player.Props;
using UniRx;
using UnityEngine;
using UnityHFSM;

namespace @_.Scripts.Player.State.AbilityState
{
    public class GunAbility : StateBase<PlayerState>
    {
        private Animator _animator;
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;
        private AttackSystem _attackSystem;
        private PlayerBase _playerBase;
        private float _insertTime;
        private readonly AbilityWeapon _abilityWeapon;
        private Timer _timer;
        private IDisposable _shootDisposable;        private AbilitySystem _abilitySystem;

        public GunAbility(PlayerInput playerInput,
            PlayerController playerController, Animator animator, AttackSystem attackSystem
            , AbilityWeapon abilityWeapon, PlayerBase playerBase,AbilitySystem abilitySystem,
            bool needsExitTime,
            bool isGhostState = false) : base(needsExitTime,
            isGhostState)
        {
            _input = playerInput;
            _controller = playerController;
            _animator = animator;
            _attackSystem = attackSystem;
            _abilityWeapon = abilityWeapon;
            _playerBase = playerBase;            _abilitySystem = abilitySystem;

        }


        public override void OnEnter()
        {
            _timer = new Timer();
            _timer.Reset();

            _shootDisposable?.Dispose();
            _animator.Play("Shoot_1");
            _attackSystem.OpenWeaponCollider(true);

            // _animator.Play("Eat");
        }

        public override void OnLogic()
        {
            if (_input.Move)
                _controller.FaceInputDireactionSlow(_input);

            if (!_input.IsPressingAttack && _timer.Elapsed > 0.3f)
            {
                _animator.Play("Shoot_2");
                _shootDisposable = Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(0.5f)).First().Subscribe(_ =>
                {
                    fsm.StateCanExit();
                    _shootDisposable?.Dispose();

                });
            }
        }

        public override void OnExit()
        {
            _attackSystem.OpenWeaponCollider(false);



            _abilityWeapon.ExecuteAblilty();
        }
    }
}