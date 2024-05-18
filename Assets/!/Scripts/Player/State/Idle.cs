using _.Scripts.Player.Props;
using _.Scripts.Tools;
using UnityEngine;
using UnityHFSM;

namespace @_.Scripts.Player.State
{
    public class Idle : StateBase<PlayerState>
    {
        private Animator _animator;
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;
        private AttackSystem _attackSystem;
        private readonly AbilityWeapon _abilityWeapon;


        public Idle(PlayerInput playerInput,
            PlayerController playerController, Animator animator,
            AttackSystem attackSystem, AbilityWeapon abilityWeapon,
            bool needsExitTime,
            bool isGhostState = false) : base(needsExitTime,
            isGhostState)
        {
            _input = playerInput;
            _controller = playerController;
            _animator = animator;
            _attackSystem = attackSystem;
            _abilityWeapon = abilityWeapon;
        }

        public override void OnEnter()
        {
            //debug
            _attackSystem.Reset();


            DebugTools.StateText("Idle");
            // _animator.Play("Q1ToIdle");

            _animator.CrossFade(Animator.StringToHash("Idle"), 0.2f);
        }

        public override void OnLogic()
        {
            _controller.Fall();
            if (_abilityWeapon.currentAbility == AbilityWeapon.AbilityType.None)
                AbilityWeaponAnimator.Instance?.PlayAnimation(AbilityWeaponAnimator.AnimationName.Idle);
            else
                AbilityWeaponAnimator.Instance?.PlayAnimation(AbilityWeaponAnimator.AnimationName.Head_Shake);
        }

        public override void OnExit()
        {
        }
    }
}