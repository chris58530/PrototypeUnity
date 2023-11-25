using System;
using _.Scripts.Event;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using UnityHFSM;

namespace _.Scripts.Player.State
{
    public enum PlayerState
    {
        Idle,
        Walk,
        AttackFirst,
        AttackSecond,
        AttackThird,
        UltimateAttack,
        SingleDash,
        MultiDash,
        BackDash,

        AttackChanceFirst,
        AttackChanceSecond,
        AttackChanceThird,
        Fail,

        Hurt,
        Roll,
        Dead
    }

    public class PlayerStateMachine : MonoBehaviour
    {
        private StateMachine<PlayerState> _fsm;
        private PlayerInput _input;
        private PlayerController _controller;
        private PlayerAttackSystem _attackSystem;
        private PlayerBase _playerBase;

        [SerializeField] private Animator animator;


        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
            _controller = GetComponent<PlayerController>();
            _attackSystem = GetComponent<PlayerAttackSystem>();
            _playerBase = GetComponent<PlayerBase>();
            // _animator = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            _fsm = new StateMachine<PlayerState>();

            //_fsm Add New State
            _fsm.AddState(
                PlayerState.Idle, new Idle(
                    _input, _controller, animator, false));
            _fsm.AddState(
                PlayerState.Walk, new Walk(
                    _input, _controller, animator, false));

            _fsm.AddState(
                PlayerState.Fail, new Fail(
                    _input, _controller, animator, _attackSystem, true));
            _fsm.AddState(
                PlayerState.Roll, new Roll(
                    _controller, animator, _attackSystem, true));

            _fsm.AddState(
                PlayerState.Hurt, new Hurt(
                    _input, _controller, animator, _attackSystem, _playerBase, false));
            _fsm.AddState(
                PlayerState.AttackFirst, new AttackFirst(
                    _input, _controller, animator, _attackSystem, true));

            _fsm.AddState(
                PlayerState.AttackSecond, new AttackSecond(
                    _input, _controller, animator, _attackSystem, true));

            _fsm.AddState(
                PlayerState.AttackThird, new AttackThird(
                    _input, _controller, animator, _attackSystem, true));

            _fsm.AddState(
                PlayerState.AttackChanceFirst, new AttackChanceFirst(
                    _input, _controller, animator, _attackSystem, false));

            _fsm.AddState(
                PlayerState.AttackChanceSecond, new AttackChanceScecond(
                    _input, _controller, animator, _attackSystem, false));
            _fsm.AddState(
                PlayerState.AttackChanceThird, new AttackChanceThird(
                    _input, _controller, animator, _playerBase, _attackSystem, false));
            _fsm.AddState(
                PlayerState.AttackChanceThird, new AttackChanceThird(
                    _input, _controller, animator, _playerBase, _attackSystem, false));
            _fsm.AddState(
                PlayerState.UltimateAttack, new UltimateAttack(
                    _input, _controller, animator, _attackSystem, true));


            //Transition

            //Idle
            _fsm.AddTwoWayTransition(PlayerState.Idle, PlayerState.Walk,
                transition => _input.Move);
            _fsm.AddTransition(PlayerState.Idle, PlayerState.Roll,
                transition => _input.IsPressedRoll);
            _fsm.AddTransition(PlayerState.Idle, PlayerState.AttackFirst,
                transition => _input.IsPressedAttack);
            _fsm.AddTransition(PlayerState.Idle, PlayerState.UltimateAttack,
                transition => _input.IsPressedUltimateAttack && _attackSystem.CanDoUltimate);
            _fsm.AddTwoWayTransition(PlayerState.Idle, PlayerState.Hurt,
                transition => _playerBase.getHurt);


            //Walk
            _fsm.AddTransition(PlayerState.Walk, PlayerState.Roll,
                transition => _input.IsPressedRoll);
            _fsm.AddTransition(PlayerState.Walk, PlayerState.AttackFirst,
                transition => _input.IsPressedAttack);
            _fsm.AddTransition(PlayerState.Walk, PlayerState.Hurt,
                transition => _playerBase.getHurt);
            //Roll
            _fsm.AddTransition(PlayerState.Roll, PlayerState.Idle);
            _fsm.AddTransition(PlayerState.Roll, PlayerState.Walk,
                transition => _input.Move);

            //Attack
            _fsm.AddTransition(PlayerState.AttackFirst, PlayerState.AttackChanceFirst);
            // _fsm.AddTransition(PlayerState.AttackFirst, PlayerState.Fail,
            //     transition =>  _input.IsPressedAttack);
            _fsm.AddTransition(PlayerState.AttackSecond, PlayerState.AttackChanceSecond);
            //_fsm.AddTransition(PlayerState.AttackSecond, PlayerState.Fail,
            //     transition =>  _input.IsPressedAttack);
            _fsm.AddTransition(PlayerState.AttackThird, PlayerState.AttackChanceThird);
            // _fsm.AddTransition(PlayerState.AttackThird, PlayerState.Fail,
            //     transition =>  _input.IsPressedAttack);
            _fsm.AddTransition(PlayerState.UltimateAttack, PlayerState.Idle);

            //AttackChance
            _fsm.AddTransition(PlayerState.AttackChanceFirst, PlayerState.AttackSecond,
                transition => _input.IsPressedAttack);
            _fsm.AddTransition(PlayerState.AttackChanceFirst, PlayerState.Roll,
                transition => _input.IsPressedRoll);
            _fsm.AddTransition(PlayerState.AttackChanceFirst, PlayerState.Fail,
                transition => _attackSystem.finishChance);
            _fsm.AddTransition(PlayerState.AttackChanceFirst, PlayerState.UltimateAttack,
                transition => _input.IsPressedUltimateAttack && _attackSystem.CanDoUltimate);
            _fsm.AddTransition(PlayerState.AttackChanceFirst, PlayerState.Hurt,
                transition => _playerBase.getHurt);

            _fsm.AddTransition(PlayerState.AttackChanceSecond, PlayerState.AttackThird,
                transition => _input.IsPressedAttack);
            _fsm.AddTransition(PlayerState.AttackChanceSecond, PlayerState.Roll,
                transition => _input.IsPressedRoll);
            _fsm.AddTransition(PlayerState.AttackChanceSecond, PlayerState.Fail,
                transition => _attackSystem.finishChance);
            _fsm.AddTransition(PlayerState.AttackChanceSecond, PlayerState.UltimateAttack,
                transition => _input.IsPressedUltimateAttack && _attackSystem.CanDoUltimate);
            _fsm.AddTransition(PlayerState.AttackChanceSecond, PlayerState.Hurt,
                transition => _playerBase.getHurt);

            _fsm.AddTransition(PlayerState.AttackChanceThird, PlayerState.AttackFirst,
                transition => _input.IsPressedAttack);
            _fsm.AddTransition(PlayerState.AttackChanceThird, PlayerState.Roll,
                transition => _input.IsPressedRoll);
            _fsm.AddTransition(PlayerState.AttackChanceThird, PlayerState.Fail,
                transition => _attackSystem.finishChance);
            _fsm.AddTransition(PlayerState.AttackChanceThird, PlayerState.UltimateAttack,
                transition => _input.IsPressedUltimateAttack && _attackSystem.CanDoUltimate);
            _fsm.AddTransition(PlayerState.AttackChanceThird, PlayerState.Hurt,
                transition => _playerBase.getHurt);
            //Fail
            _fsm.AddTransition(PlayerState.Fail, PlayerState.Idle);
            _fsm.AddTransition(PlayerState.Fail, PlayerState.Hurt);

            //Initialize
            _fsm.Init();
        }

        private void Update()
        {
            _fsm.OnLogic();
        }
    }
}