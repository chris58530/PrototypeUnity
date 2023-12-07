using System;
using _.Scripts.Event;
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
        UltiAttack,
        UltiChance,
        UltiFinalAttack,
        Chance1,
        Chance2,
        Chance3,
        Hurt,
        Roll,
        RollAttack,
        InsertSword,

        NoSwordIdle,
        NoSwordHurt,
        NoSwordRoll,
        NoSwordWalk,
        ExtractSword
    }

    public enum SuperState
    {
        Ultimate,
        NoSword,
        Normal,
        Dead
    }

    public class PlayerStateMachine : MonoBehaviour
    {
        private PlayerInput _input;
        private PlayerController _controller;
        private AttackSystem _attackSystem;
        private PlayerBase _playerBase;
        private UltimateSystem _ultimateSystem;
        [SerializeField] private Animator animator;

        private StateMachine<SuperState, string> _fsm;
        private StateMachine<SuperState, PlayerState, string> _normalState;
        private StateMachine<SuperState, PlayerState, string> _ultimateState;
        private StateMachine<SuperState, PlayerState, string> _noSwordState;

        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
            _controller = GetComponent<PlayerController>();
            _attackSystem = GetComponent<AttackSystem>();
            _playerBase = GetComponent<PlayerBase>();
            _ultimateSystem = GetComponent<UltimateSystem>();
        }

        private void Start()
        {
            #region Normal state

            _normalState = new StateMachine<SuperState, PlayerState, string>();

            _normalState.AddState(
                PlayerState.Idle, new Idle(
                    _input, _controller, animator, _attackSystem, false));
            _normalState.AddState(
                PlayerState.InsertSword, new InsertSword(
                    _input, _controller, animator, _attackSystem,_playerBase, true));
            _normalState.AddState(
                PlayerState.Walk, new Walk(
                    _input, _controller, animator, false));
            _normalState.AddState(
                PlayerState.Roll, new Roll(
                    _controller, animator, _attackSystem, true));
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
                PlayerState.Chance2, new Chance2(
                    _input, _controller, animator, _attackSystem, false));
            _normalState.AddState(
                PlayerState.Chance3, new Chance3(
                    _input, _controller, animator, _playerBase, _attackSystem, false));


            //Idle
            _normalState.AddTwoWayTransition(PlayerState.Idle, PlayerState.Walk,
                transition => _input.Move);
            _normalState.AddTwoWayTransition(PlayerState.Idle, PlayerState.InsertSword,
                transition => Input.GetKey(KeyCode.Q));
            _normalState.AddTwoWayTransition(PlayerState.Idle, PlayerState.Hurt,
                transition => _playerBase.getHurt);
            _normalState.AddTwoWayTransition(PlayerState.Idle, PlayerState.Roll,
                transition => _input.IsPressedRoll);
            _normalState.AddTransition(PlayerState.Idle, PlayerState.Attack1,
                transition => _input.IsPressedAttack);
            //Walk
            _normalState.AddTwoWayTransition(PlayerState.Walk, PlayerState.Roll,
                transition => _input.IsPressedRoll);
            _normalState.AddTransition(PlayerState.Walk, PlayerState.Attack1,
                transition => _input.IsPressedAttack);
            _normalState.AddTransition(PlayerState.Walk, PlayerState.Hurt,
                transition => _playerBase.getHurt);
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

            //AttackChance
            _normalState.AddTransition(PlayerState.Chance1, PlayerState.Attack2,
                transition => _input.IsPressedAttack);
            _normalState.AddTransition(PlayerState.Chance1, PlayerState.Roll,
                transition => _input.IsPressedRoll);
            _normalState.AddTransition(PlayerState.Chance1, PlayerState.Hurt,
                transition => _playerBase.getHurt);

            _normalState.AddTransition(PlayerState.Chance2, PlayerState.Attack3,
                transition => _input.IsPressedAttack);
            _normalState.AddTransition(PlayerState.Chance2, PlayerState.Roll,
                transition => _input.IsPressedRoll);
            _normalState.AddTransition(PlayerState.Chance2, PlayerState.Hurt,
                transition => _playerBase.getHurt);


            _normalState.AddTransition(PlayerState.Chance3, PlayerState.Attack1,
                transition => _input.IsPressedAttack);
            _normalState.AddTransition(PlayerState.Chance3, PlayerState.Roll,
                transition => _input.IsPressedRoll);
            _normalState.AddTransition(PlayerState.Chance3, PlayerState.Hurt,
                transition => _playerBase.getHurt);

            #endregion


            //=========================================================================


            #region Ultimate state

            _ultimateState = new StateMachine<SuperState, PlayerState, string>();


            _ultimateState.AddState(
                PlayerState.UltiAttack, new UltiAttack(
                    _input, _controller, animator, _ultimateSystem, true));
            _ultimateState.AddState(
                PlayerState.UltiFinalAttack, new UltiFinalAttack(
                    _input, _controller, animator, _ultimateSystem, true));
            _ultimateState.AddState(
                PlayerState.UltiChance, new UltiChance(
                    _input, _controller, animator, _ultimateSystem, false));

            _ultimateState.AddTransition(PlayerState.UltiAttack, PlayerState.UltiChance);
            _ultimateState.AddTransition(PlayerState.UltiChance, PlayerState.UltiAttack,
                transition => _ultimateSystem.ultimateCount <= 5 && _input.IsPressedUltimateAttack);
            _ultimateState.AddTransition(PlayerState.UltiChance, PlayerState.UltiFinalAttack,
                transition => _ultimateSystem.ultimateCount > 5 && _input.IsPressedUltimateAttack);

            #endregion

            #region No Sword State

            _noSwordState = new StateMachine<SuperState, PlayerState, string>();
            _noSwordState.AddState(
                PlayerState.NoSwordIdle, new NoSwordIdle(
                    _input, _controller, animator, false));
            _noSwordState.AddState(
                PlayerState.NoSwordRoll, new NoSwordRoll(
                    _controller, animator, _attackSystem, true));
            // _noSwordState.AddState(
            //     PlayerState.ExtractSword, new ExtractSword(
            //         _controller, animator, _attackSystem, true));

            _noSwordState.AddState(
                PlayerState.NoSwordHurt, new NoSwordHurt(
                    _input, _controller, animator, _playerBase, false));
            _noSwordState.AddState(
                PlayerState.NoSwordWalk, new NoSwordWalk(
                    _input, _controller, animator, false));

            _noSwordState.AddTwoWayTransition(PlayerState.NoSwordIdle, PlayerState.NoSwordWalk,
                transition => _input.Move);
            _noSwordState.AddTwoWayTransition(PlayerState.NoSwordIdle, PlayerState.NoSwordHurt,
                transition => _playerBase.getHurt);
            _noSwordState.AddTwoWayTransition(PlayerState.NoSwordIdle, PlayerState.NoSwordRoll,
                transition => _input.IsPressedRoll);

            _noSwordState.AddTransition(PlayerState.NoSwordWalk, PlayerState.NoSwordRoll,
                transition => _input.IsPressedRoll);
            _noSwordState.AddTransition(PlayerState.NoSwordWalk, PlayerState.NoSwordHurt,
                transition => _playerBase.getHurt);

            #endregion

            //=========================================================================
            //Initialize fsm
            _fsm = new StateMachine<SuperState, string>();
            _fsm.AddState(SuperState.Normal, _normalState);
            _fsm.AddState(SuperState.NoSword, _noSwordState);
            _fsm.AddState(SuperState.Ultimate, _ultimateState);
            _fsm.AddState(SuperState.Dead, new Dead(animator, false));

            _fsm.AddTransition(SuperState.Normal, SuperState.Ultimate,
                transition => _input.IsPressedUltimateAttack && _ultimateSystem.CanDoUltimate);
            _fsm.AddTransition(SuperState.Ultimate, SuperState.Normal,
                transition => _ultimateSystem.finishUltimate);

            _fsm.AddTwoWayTransition(SuperState.Normal, SuperState.NoSword,
                transitionon => !_attackSystem.hasSword);


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