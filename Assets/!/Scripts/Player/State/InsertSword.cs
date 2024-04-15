using _.Scripts.Player.Props;
using _.Scripts.Tools;
using UnityEngine;
using UnityHFSM;
using _.Scripts.Event;

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
        private AbilitySystem _abilitySystem;

        public InsertSword(PlayerInput playerInput,
            PlayerController playerController, Animator animator, AttackSystem attackSystem
            , AbilitySystem abilitySystem, PlayerBase playerBase,
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
        }


        public override void OnEnter()
        {
            //debug
            DebugTools.StateText("InsertSword");
            _animator.CrossFade(Animator.StringToHash("UseAbility"), 0.1f);
            _animator.Play("UseAbility");
            AbilityWeaponAnimator.Instance?.PlayAnimation(AbilityWeaponAnimator.AnimationName.Azbsword);

            _insertTime = 0;
            _abilitySystem.Attack();

            //避免接下來連續兩次q1
            // _attackSystem.finishAttack = true;
            // _attackSystem.finishAttack = false;
            // _attackSystem. finsihFail = true;
            // _attackSystem._failTimer?.Dispose();
            _attackSystem.Fail();


            if (_input.Move)
                _controller.FaceInputDireaction(_input);
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
            PlayerActions.endPlayerEatEffect?.Invoke();
            _abilitySystem.CancelAttack();
        }
    }
}