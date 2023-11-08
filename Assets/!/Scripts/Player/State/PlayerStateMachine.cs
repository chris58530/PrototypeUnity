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
        Pull,
        Dash,
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

        [SerializeField] private Animator animator;

        //Coldwater//
        [SerializeField] private Transform _positionToSpawn;
        [SerializeField] private GameObject _dashModel;


        private void Awake()
        {
            _input = GetComponent<PlayerMapInput>();
            _controller = GetComponent<PlayerController>();
            _playerHp = GetComponent<PlayerHp>();
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
                PlayerState.Attack, new PlayerAttack(
                    _input, _controller, animator, true));
            _fsm.AddState(
                PlayerState.Pull, new PlayerPull(
                    _input, _controller, animator, false));
            _fsm.AddState(
                PlayerState.Dash, new PlayerDash(
                    _controller, animator, _positionToSpawn, _dashModel, true));
            _fsm.AddState(
                PlayerState.Hurt, new PlayerHurt(
                    _controller, animator, _playerHp, true));
            _fsm.AddState(
                PlayerState.Roll, new PlayerRoll(
                    false));
            _fsm.AddState(
                PlayerState.Dead, new PlayerDead(
                    animator, _playerHp,false));

            //_fsm Transition

            //Idle
            _fsm.AddTwoWayTransition(PlayerState.Idle, PlayerState.Walk,
                transition => _input.Move);
            _fsm.AddTransition(PlayerState.Idle, PlayerState.Dash,
                transition => _input.IsReleasedDash);
            _fsm.AddTransition(PlayerState.Idle, PlayerState.Pull,
                transition => _input.IsPressedPull);
            _fsm.AddTransition(PlayerState.Idle, PlayerState.Attack,
                transition => _input.IsPressedAttack);
            _fsm.AddTransition(PlayerState.Idle, PlayerState.Hurt,
                transition => _playerHp.getAttack);
            _fsm.AddTransition(PlayerState.Idle, PlayerState.Dead,
                transition => _playerHp.Dead);

            //Walk
            _fsm.AddTransition(PlayerState.Walk, PlayerState.Dash,
                transition => _input.IsReleasedDash);
            _fsm.AddTransition(PlayerState.Walk, PlayerState.Pull,
                transition => _input.IsPressedPull);
            _fsm.AddTransition(PlayerState.Walk, PlayerState.Attack,
                transition => _input.IsPressedAttack);
            _fsm.AddTransition(PlayerState.Walk, PlayerState.Hurt,
                transition => _playerHp.getAttack);
            _fsm.AddTransition(PlayerState.Walk, PlayerState.Dead,
                transition => _playerHp.Dead);
            //Dash
            _fsm.AddTransition(PlayerState.Dash, PlayerState.Idle);

            //Pull
            _fsm.AddTransition(PlayerState.Pull, PlayerState.Idle,
                transition => _input.IsReleasedPull);

            //Attack
            _fsm.AddTransition(PlayerState.Attack, PlayerState.Idle);
            _fsm.AddTransition(PlayerState.Attack, PlayerState.Hurt);
            
            //Hurt
            _fsm.AddTransition(PlayerState.Hurt, PlayerState.Idle);
            _fsm.AddTransition(PlayerState.Hurt, PlayerState.Dead,
                transition => _playerHp.Dead);
            //Initialize
            _fsm.Init();
        }

        private void Update()
        {
            _fsm.OnLogic();
        }
    }
}