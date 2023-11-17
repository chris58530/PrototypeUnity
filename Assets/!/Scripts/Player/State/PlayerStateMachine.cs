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

        PureDash,
        SingleDash,
        MultiDash,
        BackDash,
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
                PlayerState.DashChance, new PlayerDashChance(
                    _input, _controller, animator, false));
            _fsm.AddState(
                PlayerState.DashFail, new PlayerDashFail(
                    _input, _controller, animator, _combo, true));
            _fsm.AddState(
                PlayerState.PureDash, new PlayerPureDash(
                    _controller, animator, _combo, true));
            _fsm.AddState(
                PlayerState.SingleDash, new PlayerSingleDash(
                    _controller, animator, _combo, true));
            _fsm.AddState(
                PlayerState.MultiDash, new PlayerMultiDash(
                    _controller, animator, _combo, true));
            _fsm.AddState(
                PlayerState.BackDash, new PlayerBackDash(
                    _controller, animator, _combo, true));

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


            //Transition

            //Idle
            _fsm.AddTwoWayTransition(PlayerState.Idle, PlayerState.Walk,
                transition => _input.Move);
            _fsm.AddTransition(PlayerState.Idle, PlayerState.PureDash,
                transition => Input.GetKeyDown(KeyCode.Q));
            _fsm.AddTransition(PlayerState.Idle, PlayerState.SingleDash,
                transition => Input.GetKeyDown(KeyCode.W));
            _fsm.AddTransition(PlayerState.Idle, PlayerState.MultiDash,
                transition => Input.GetKeyDown(KeyCode.E));
            _fsm.AddTransition(PlayerState.Idle, PlayerState.BackDash,
                transition => Input.GetKeyDown(KeyCode.R));
            //Walk
            _fsm.AddTransition(PlayerState.Walk, PlayerState.PureDash,
                transition => Input.GetKeyDown(KeyCode.Q));
            _fsm.AddTransition(PlayerState.Walk, PlayerState.SingleDash,
                transition => Input.GetKeyDown(KeyCode.W));
        

            //PureDash
            _fsm.AddTransition(PlayerState.PureDash, PlayerState.DashChance);

            //SingleDash
            _fsm.AddTransition(PlayerState.SingleDash, PlayerState.DashChance);

            
            //MultiDash
            _fsm.AddTransition(PlayerState.MultiDash, PlayerState.DashChance);
            
            //BackDash
            _fsm.AddTransition(PlayerState.BackDash, PlayerState.DashChance);


            //DashChance
            _fsm.AddTransition(PlayerState.DashChance, PlayerState.PureDash,
                transition => Input.GetKeyDown(KeyCode.Q));
            _fsm.AddTransition(PlayerState.DashChance, PlayerState.SingleDash,
                transition => Input.GetKeyDown(KeyCode.W));
            _fsm.AddTransition(PlayerState.DashChance, PlayerState.MultiDash,
                transition => Input.GetKeyDown(KeyCode.E));
            _fsm.AddTransition(PlayerState.DashChance, PlayerState.BackDash,
                transition => Input.GetKeyDown(KeyCode.R));
            _fsm.AddTransition(PlayerState.DashChance, PlayerState.DashFail,
                transition=>_controller.finishChance);

            //DashFail
            _fsm.AddTransition(PlayerState.DashFail, PlayerState.Idle);

            //Initialize
            _fsm.Init();
        }

        private void Update()
        {
            _fsm.OnLogic();
        }
    }
}