using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityHFSM;

namespace _.Scripts.Player.State
{
    public enum PlayerState
    {
        Idle,
        Walk,
        Attack,

        Dash,
        DashChance,
        DashFail,
        Hurt,
        Roll,
        Dead
    }

    public class PlayerStateMachine : MonoBehaviour
    {
        private StateMachine<PlayerState> _fsm;
        private PlayerMapInput _input;
        private PlayerController _controller;
        private PlayerHp _playerHp;
        private PlayerCombo _combo;
        [SerializeField] private Animator animator;


        private void Awake()
        {
            _input = GetComponent<PlayerMapInput>();
            _controller = GetComponent<PlayerController>();
            _playerHp = GetComponent<PlayerHp>();
            _combo = GetComponent<PlayerCombo>();
            // _animator = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            _fsm = new StateMachine<PlayerState>();

            //_fsm Add New State
            _fsm.AddState(
                PlayerState.Idle, new PlayerIdle(
                    _input, _controller, animator, false));
            _fsm.AddState(
                PlayerState.Walk, new PlayerWalk(
                    _input, _controller, animator, false));
            _fsm.AddState(
                PlayerState.Dash, new PlayerDash(
                    _controller, animator,_combo, true));
            _fsm.AddState(
                PlayerState.DashChance, new PlayerDashChance(
                    _input, _controller, animator, false));
            _fsm.AddState(
                PlayerState.DashFail, new PlayerDashFail(
                    _input, _controller, animator,_combo, true));

            // _fsm.AddState(
            //     PlayerState.Attack, new PlayerAttack(
            //         _input, _controller, animator, true));
            //
            // _fsm.AddState(
            //     PlayerState.Hurt, new PlayerHurt(
            //         _controller, animator, _playerHp, true));
            // _fsm.AddState(
            //     PlayerState.Roll, new PlayerRoll(
            //         false));
            // _fsm.AddState(
            //     PlayerState.Dead, new PlayerDead(
            //         animator, _playerHp,false));

            //_fsm Transition

            //Idle
            _fsm.AddTwoWayTransition(PlayerState.Idle, PlayerState.Walk,
                transition => _input.Move);
            _fsm.AddTransition(PlayerState.Idle, PlayerState.Dash,
                transition => _input.IsPressedDash);
            //Walk
            _fsm.AddTransition(PlayerState.Walk, PlayerState.Dash,
                transition => _input.IsPressedDash);
            //Dash
            _fsm.AddTransition(PlayerState.Dash, PlayerState.DashChance);

            //DashChance
            _fsm.AddTransition(PlayerState.DashChance, PlayerState.Dash,
                transition => _input.IsPressedDash);
            _fsm.AddTransition(PlayerState.DashChance, PlayerState.DashFail,
                transition => _controller.finishChance);

            //DashFail
            _fsm.AddTransition(PlayerState.DashFail, PlayerState.Idle);

            // //Attack
            // _fsm.AddTransition(PlayerState.Attack, PlayerState.Idle);
            // _fsm.AddTransition(PlayerState.Attack, PlayerState.Hurt);
            //
            // //Hurt
            // _fsm.AddTransition(PlayerState.Hurt, PlayerState.Idle);
            // _fsm.AddTransition(PlayerState.Hurt, PlayerState.Dead,
            //     transition => _playerHp.Dead);

            //Initialize
            _fsm.Init();
        }

        private void Update()
        {
            _fsm.OnLogic();
        }
    }
}