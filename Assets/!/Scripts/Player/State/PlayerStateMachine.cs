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
        AttackFirst,
        AttackSecond,
        AttackThird,
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
                PlayerState.Fail, new PlayerFail(
                    _input, _controller, animator, _combo, true));
            _fsm.AddState(
                PlayerState.Roll, new PlayerRoll(
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

            _fsm.AddState(
                PlayerState.AttackFirst, new PlayerAttackFirst(
                    _input, _controller, animator, true));

            _fsm.AddState(
                PlayerState.AttackSecond, new PlayerAttackSecond(
                    _input, _controller, animator, true));

            _fsm.AddState(
                PlayerState.AttackThird, new PlayerAttackThird(
                    _input, _controller, animator, true));

            _fsm.AddState(
                PlayerState.AttackChanceFirst, new PlayerAttackChanceFirst(
                    _input, _controller, animator, false));

            _fsm.AddState(
                PlayerState.AttackChanceSecond, new PlayerAttackChanceScecond(
                    _input, _controller, animator, false));
            _fsm.AddState(
                PlayerState.AttackChanceThird, new PlayerAttackChanceThird(
                    _input, _controller, animator, false));

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
            _fsm.AddTransition(PlayerState.Idle, PlayerState.Roll,
                transition => _input.IsPressedRoll);
            _fsm.AddTransition(PlayerState.Idle, PlayerState.AttackFirst,
                transition => _input.IsPressedAttack);
            // _fsm.AddTransition(PlayerState.Idle, PlayerState.SingleDash,
            //     transition => Input.GetKeyDown(KeyCode.Q));
            // _fsm.AddTransition(PlayerState.Idle, PlayerState.MultiDash,
            //     transition => Input.GetKeyDown(KeyCode.W));
            // _fsm.AddTransition(PlayerState.Idle, PlayerState.BackDash,
            //     transition => Input.GetKeyDown(KeyCode.E));
            //Walk
            _fsm.AddTransition(PlayerState.Walk, PlayerState.Roll,
                transition => _input.IsPressedRoll);
            _fsm.AddTransition(PlayerState.Walk, PlayerState.SingleDash,
                transition => Input.GetKeyDown(KeyCode.Q));
            _fsm.AddTransition(PlayerState.Walk, PlayerState.AttackFirst,
                transition => _input.IsPressedAttack);
            //Roll
            _fsm.AddTransition(PlayerState.Roll, PlayerState.Idle);
            _fsm.AddTransition(PlayerState.Roll, PlayerState.Walk,
                transition => _input.Move);

            //Attack
            _fsm.AddTransition(PlayerState.AttackFirst, PlayerState.AttackChanceFirst);
            _fsm.AddTransition(PlayerState.AttackSecond, PlayerState.AttackChanceSecond);
            _fsm.AddTransition(PlayerState.AttackThird, PlayerState.AttackChanceThird);

            //AttackChance
            _fsm.AddTransition(PlayerState.AttackChanceFirst, PlayerState.AttackSecond,
                transition => _input.IsPressedAttack);
            _fsm.AddTransition(PlayerState.AttackChanceFirst, PlayerState.Walk,
                transition => _input.Move);
            _fsm.AddTransition(PlayerState.AttackChanceSecond, PlayerState.AttackThird,
                transition => _input.IsPressedAttack);
            _fsm.AddTransition(PlayerState.AttackChanceSecond, PlayerState.Walk,
                transition => _input.Move);
            _fsm.AddTransition(PlayerState.AttackChanceThird, PlayerState.AttackFirst,
                transition => _input.IsPressedAttack);
            _fsm.AddTransition(PlayerState.AttackChanceThird, PlayerState.Walk,
                transition => _input.Move);


            // //SingleDash
            // _fsm.AddTransition(PlayerState.SingleDash, PlayerState.DashChance);
            //
            //
            // //MultiDash
            // _fsm.AddTransition(PlayerState.MultiDash, PlayerState.DashChance);
            //
            // //BackDash
            // _fsm.AddTransition(PlayerState.BackDash, PlayerState.DashChance);


            //DashChance
            _fsm.AddTransition(PlayerState.AttackChanceFirst, PlayerState.Roll,
                transition => Input.GetKeyDown(KeyCode.LeftShift));
            // _fsm.AddTransition(PlayerState.DashChance, PlayerState.SingleDash,
            //     transition => Input.GetKeyDown(KeyCode.Q));
            // _fsm.AddTransition(PlayerState.DashChance, PlayerState.MultiDash,
            //     transition => Input.GetKeyDown(KeyCode.W));
            // _fsm.AddTransition(PlayerState.DashChance, PlayerState.BackDash,
            //     transition => Input.GetKeyDown(KeyCode.E));
            _fsm.AddTransition(PlayerState.AttackChanceFirst, PlayerState.Fail,
                transition => _controller.finishChance);

            //DashFail
            _fsm.AddTransition(PlayerState.Fail, PlayerState.Idle);

            //Initialize
            _fsm.Init();
        }

        private void Update()
        {
            _fsm.OnLogic();
        }
    }
}