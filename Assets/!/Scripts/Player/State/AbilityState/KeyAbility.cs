using _.Scripts.Player.Props;
using _.Scripts.Tools;
using UnityEngine;
using UnityHFSM;

namespace @_.Scripts.Player.State.AbilityState
{
    public class KeyAbility : StateBase<PlayerState>
    {
        private Animator _animator;
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;
        private AttackSystem _attackSystem;
        private PlayerBase _playerBase;
        private float _insertTime;
        private AbilitySystem _abilitySystem;        private readonly AbilityWeapon _abilityWeapon;

        public KeyAbility(PlayerInput playerInput,
            PlayerController playerController, Animator animator, AttackSystem attackSystem
            ,AbilitySystem abilitySystem,PlayerBase playerBase, AbilityWeapon abilityWeapon,
            bool needsExitTime,
            bool isGhostState = false) : base(needsExitTime,
            isGhostState)
        {
            _input = playerInput;
            _controller = playerController;
            _animator = animator;
            _attackSystem = attackSystem;
            _abilitySystem = abilitySystem;
            _playerBase = playerBase;
            _abilityWeapon = abilityWeapon;

        }

       
        public override void OnEnter()
        {
            //debug
            DebugTools.StateText("KeyAbility");
            
            
            _animator.Play("KeyAbility");
            

            _insertTime = 0;
            _attackSystem.UseAbilityAttack(AbilityWeapon.AbilityType.Key);
            _attackSystem.AutoDetect();

        }

        public override void OnLogic()
        {
            _insertTime += Time.deltaTime;
            if (_insertTime > _abilitySystem.insertTime)
            {
                fsm.StateCanExit();
            }
       
            _controller.Fall();
        }

        public override void OnExit()
        {
            _attackSystem.CancelAttack();

        }
    }
}