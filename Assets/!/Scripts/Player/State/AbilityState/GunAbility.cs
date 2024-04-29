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

        public GunAbility(PlayerInput playerInput,
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

            _animator.Play("Shoot_1");
            _controller.FaceToMousePos();

            // _animator.Play("Eat");
        }

        public override void OnLogic()
        {
            _controller.FaceToMousePos();

            if (!_input.IsPressingAttack && _timer.Elapsed > 0.3f)
            {
                _animator.Play("Shoot_2");
                Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(0.5f)).First().Subscribe(_ =>
                {
                    fsm.StateCanExit();

                }).AddTo(this._controller);
            }
        }

        public override void OnExit()
        {
            _abilityWeapon.ExecuteAblilty();
        }
    }
}