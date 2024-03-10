using System;
using _.Scripts.Event;
using _.Scripts.Player.Props;
using UniRx;
using UnityEngine;
using UnityHFSM;

namespace _.Scripts.Player.State
{
    public enum PlayerState
    {
        Idle,
        Walk,
        Attack1,
        Attack2,
        Attack3,
        Fail,

        InsertSword,
        Chance1,
        Chance1ToIdle,
        Chance2,
        Chance3,
        Hurt,
        Roll,
        RollAttack,

        KeyAbility,
    }

    public enum SuperState
    {
        Normal,
        Hammer,
        Dead
    }

    public class PlayerStateMachine : MonoBehaviour
    {
        private PlayerInput _input;
        private PlayerController _controller;
        private AttackSystem _attackSystem;
        private AbilitySystem _abilitySystem;
        private PlayerBase _playerBase;
        [SerializeField] private Animator animator;

        private StateMachine<SuperState, string> _fsm;
        private StateMachine<SuperState, PlayerState, string> _normalState;


        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
            _controller = GetComponent<PlayerController>();
            _attackSystem = GetComponent<AttackSystem>();
            _abilitySystem = GetComponent<AbilitySystem>();
            _playerBase = GetComponent<PlayerBase>();
        }

        private void Start()
        {
            #region Normal state

            _normalState = new StateMachine<SuperState, PlayerState, string>();
            _normalState.AddState(
                PlayerState.Idle, new Idle(
                    _input, _controller, animator, _attackSystem, false));

            _normalState.AddState(
                PlayerState.Fail, new Fail(
                    _input, _controller, animator, _attackSystem, false));

            _normalState.AddState(
                PlayerState.Walk, new Walk(
                    _input, _controller, animator, _attackSystem, false));
            _normalState.AddState(
                PlayerState.Roll, new Roll(
                    _input, _controller, animator, _attackSystem, false));
            _normalState.AddState(
                PlayerState.Hurt, new Hurt(
                    _input, _controller, animator, _attackSystem, _playerBase, false));
            _normalState.AddState(
                PlayerState.RollAttack, new RollAttack(
                    _controller, animator, _attackSystem, true));
            _normalState.AddState(
                PlayerState.Attack1, new Attack1(
                    _input, _controller, animator, _attackSystem, true));

            _normalState.AddState(
                PlayerState.Attack2, new Attack2(
                    _input, _controller, animator, _attackSystem, true));

            _normalState.AddState(
                PlayerState.Attack3, new Attack3(
                    _input, _controller, animator, _attackSystem, true));

            _normalState.AddState(
                PlayerState.Chance1, new Chance1(
                    _input, _controller, animator, _attackSystem, false));
            _normalState.AddState(
                PlayerState.Chance1ToIdle, new Chance1ToIdle(
                    _input, _controller, animator, _attackSystem, true));

            _normalState.AddState(
                PlayerState.Chance2, new Chance2(
                    _input, _controller, animator, _attackSystem, false));
            _normalState.AddState(
                PlayerState.Chance3, new Chance3(
                    _input, _controller, animator, _playerBase, _attackSystem, true));
            _normalState.AddState(
                PlayerState.InsertSword, new InsertSword(
                    _input, _controller, animator, _attackSystem, _abilitySystem, _playerBase, true));
            _normalState.AddState(
                PlayerState.KeyAbility, new KeyAbility(
                    _input, _controller, animator, _attackSystem, _abilitySystem, _playerBase, true));

            //Idle
            _normalState.AddTwoWayTransition(PlayerState.Idle, PlayerState.Walk,
                transition => _input.Move);
            _normalState.AddTwoWayTransition(PlayerState.Idle, PlayerState.Hurt,
                transition => _playerBase.getHurt);
            _normalState.AddTransition(PlayerState.Idle, PlayerState.Roll,
                transition => _input.IsPressedRoll && !_controller.blockRoll);
            _normalState.AddTransition(PlayerState.Idle, PlayerState.Attack1,
                transition => _input.IsPressedAttack && _attackSystem.attackCount == 0);
            _normalState.AddTransition(PlayerState.Idle, PlayerState.Attack2,
                transition => _input.IsPressedAttack && _attackSystem.attackCount == 1);
            _normalState.AddTransition(PlayerState.Idle, PlayerState.Attack3,
                transition => _input.IsPressedAttack && _attackSystem.attackCount == 2);
            
            _normalState.AddTransition(PlayerState.Idle, PlayerState.InsertSword,
                transition => _input.IsPressedAbility &&
                              _abilitySystem.GetCurrentAbility == AbilityWeapon.AbilityType.None);
            _normalState.AddTransition(PlayerState.Idle, PlayerState.KeyAbility,
                transition => _input.IsReleasedAbility &&
                              _abilitySystem.GetCurrentAbility == AbilityWeapon.AbilityType.Key);

            _normalState.AddTransition(PlayerState.InsertSword, PlayerState.Idle);
            _normalState.AddTransition(PlayerState.KeyAbility, PlayerState.Idle);
            //Walk
            _normalState.AddTransition(PlayerState.Walk, PlayerState.Roll,
                transition => _input.IsPressedRoll && !_controller.blockRoll);
            _normalState.AddTransition(PlayerState.Walk, PlayerState.InsertSword,
                transition => _input.IsPressedAbility);


            _normalState.AddTransition(PlayerState.Roll, PlayerState.Fail,
                transition => _controller.finsihRoll);
            //RollAttack
            _normalState.AddTransition(PlayerState.RollAttack, PlayerState.Idle);

            _normalState.AddTransition(PlayerState.Walk, PlayerState.Hurt,
                transition => _playerBase.getHurt);
            _normalState.AddTransition(PlayerState.Walk, PlayerState.Attack1,
                transition => _input.IsPressedAttack && _attackSystem.attackCount == 0);
            _normalState.AddTransition(PlayerState.Walk, PlayerState.Attack2,
                transition => _input.IsPressedAttack && _attackSystem.attackCount == 1);
            _normalState.AddTransition(PlayerState.Walk, PlayerState.Attack3,
                transition => _input.IsPressedAttack && _attackSystem.attackCount == 2);
            //Attack
            _normalState.AddTransition(PlayerState.Attack1, PlayerState.Chance1);
            _normalState.AddTransition(PlayerState.Attack1, PlayerState.Hurt,
                transition => _playerBase.getHurt);
            _normalState.AddTransition(PlayerState.Attack2, PlayerState.Chance2);
            _normalState.AddTransition(PlayerState.Attack2, PlayerState.Hurt,
                transition => _playerBase.getHurt);
            _normalState.AddTransition(PlayerState.Attack3, PlayerState.Chance3);
            _normalState.AddTransition(PlayerState.Attack3, PlayerState.Hurt,
                transition => _playerBase.getHurt);
            //extra
            // _normalState.AddTransition(PlayerState.Chance1ToIdle, PlayerState.Fail);

            //AttackChance
            _normalState.AddTransition(PlayerState.Chance1, PlayerState.Fail,
                transition => _attackSystem.finishAttack);
            _normalState.AddTransition(PlayerState.Chance1, PlayerState.Attack2,
                transition => _input.IsPressedAttack);
            _normalState.AddTransition(PlayerState.Chance1, PlayerState.Roll,
                transition => _input.IsPressedRoll && !_controller.blockRoll);
            _normalState.AddTransition(PlayerState.Chance1, PlayerState.Hurt,
                transition => _playerBase.getHurt);
            _normalState.AddTransition(PlayerState.Chance1, PlayerState.Walk,
                transition => _input.Move);
            _normalState.AddTransition(PlayerState.Chance1, PlayerState.InsertSword,
                transition => _input.IsPressedAbility);

            //
            //
            _normalState.AddTransition(PlayerState.Chance2, PlayerState.Attack3,
                transition => _input.IsPressedAttack);

            _normalState.AddTransition(PlayerState.Chance2, PlayerState.Fail,
                transition => _attackSystem.finishAttack);
            _normalState.AddTransition(PlayerState.Chance2, PlayerState.Roll,
                transition => _input.IsPressedRoll && !_controller.blockRoll);
            _normalState.AddTransition(PlayerState.Chance2, PlayerState.Hurt,
                transition => _playerBase.getHurt);

            _normalState.AddTransition(PlayerState.Chance2, PlayerState.Walk,
                transition => _input.Move);
            _normalState.AddTransition(PlayerState.Chance2, PlayerState.InsertSword,
                transition => _input.IsPressedAbility);

            _normalState.AddTransition(PlayerState.Chance3, PlayerState.Attack1,
                transition => _input.IsPressedAttack);
            _normalState.AddTransition(PlayerState.Chance3, PlayerState.Fail,
                transition => _attackSystem.finishAttack);
            _normalState.AddTransition(PlayerState.Chance3, PlayerState.Roll,
                transition => _input.IsPressedRoll && !_controller.blockRoll);
            _normalState.AddTransition(PlayerState.Chance3, PlayerState.Hurt,
                transition => _playerBase.getHurt);

            _normalState.AddTransition(PlayerState.Chance3, PlayerState.Walk,
                transition => _input.Move);
            _normalState.AddTransition(PlayerState.Chance3, PlayerState.InsertSword,
                transition => _input.IsPressedAbility);

            //Faill
            _normalState.AddTransition(PlayerState.Fail, PlayerState.Idle,
                transition => _attackSystem.finsihFail);
            // _normalState.AddTransition(PlayerState.Fail, PlayerState.Chance1ToIdle,
            //     transition => _attackSystem.finsihFail && _attackSystem.attackCount == 1);

            #endregion


            //=========================================================================
            //Initialize fsm
            _fsm = new StateMachine<SuperState, string>();
            _fsm.AddState(SuperState.Normal, _normalState);

            _fsm.AddState(SuperState.Dead, new Dead(animator, false));
            _fsm.AddTwoWayTransition(SuperState.Normal, SuperState.Dead,
                transition => Input.GetKey(KeyCode.P));


            //_fsm.AddTwoWayTransition(SuperState.Normal, SuperState.NoSword,
            //transitionon => !_attackSystem.hasSword);


            // _fsm.AddTriggerTransition("Award", new Transition<SuperState>(SuperState.Weak, SuperState.Normal));
            // _fsm.AddTriggerTransition("NoSword", new Transition<SuperState>(SuperState.NoSword, SuperState.Weak));
            _fsm.Init();
        }

        private void Update()
        {
            _fsm.OnLogic();
        }
    }
}