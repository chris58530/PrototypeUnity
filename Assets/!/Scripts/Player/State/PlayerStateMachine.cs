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
        UltimateAttack,
        Chance1,
        Chance2,
        Chance3,
        Fail,
        Hurt,
        Roll,

        WeakHurt,
        WeakIdle,
        WeakWalk,

        NoSwordIdle,
        NoSwordHurt,
        NoSwordRoll,
        NoSwordWalk
    }

    public enum SuperState
    {
        Weak,
        NoSword,
        Normal,
        Dead
    }

    public class PlayerStateMachine : MonoBehaviour
    {
        private PlayerInput _input;
        private PlayerController _controller;
        private PlayerAttackSystem _attackSystem;
        private PlayerBase _playerBase;
        [SerializeField] private Animator animator;

        private StateMachine<SuperState, string> _fsm;
        private StateMachine<SuperState, PlayerState, string> _normalState;
        private StateMachine<SuperState, PlayerState, string> _weakState;
        private StateMachine<SuperState, PlayerState, string> _noSwordState;

        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
            _controller = GetComponent<PlayerController>();
            _attackSystem = GetComponent<PlayerAttackSystem>();
            _playerBase = GetComponent<PlayerBase>();
        }

        private void Start()
        {
            #region Normal state

            _normalState = new StateMachine<SuperState, PlayerState, string>();

            _normalState.AddState(
                PlayerState.Idle, new Idle(
                    _input, _controller, animator,_attackSystem, false));
            _normalState.AddState(
                PlayerState.Walk, new Walk(
                    _input, _controller, animator, false));
            _normalState.AddState(
                PlayerState.Roll, new Roll(
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
            _normalState.AddState(
                PlayerState.UltimateAttack, new UltimateAttack(
                    _input, _controller, animator, _attackSystem, true));
            
            
            //Idle
            _normalState.AddTwoWayTransition(PlayerState.Idle, PlayerState.Walk,
                transition => _input.Move);
            _normalState.AddTwoWayTransition(PlayerState.Idle, PlayerState.Roll,
                transition => _input.IsPressedRoll);
            _normalState.AddTransition(PlayerState.Idle, PlayerState.Attack1,
                transition => _input.IsPressedAttack);
            _normalState.AddTransition(PlayerState.Idle, PlayerState.UltimateAttack,
                transition => _input.IsPressedUltimateAttack && _attackSystem.CanDoUltimate);
            //Walk
            _normalState.AddTwoWayTransition(PlayerState.Walk, PlayerState.Roll,
                transition => _input.IsPressedRoll);
            _normalState.AddTransition(PlayerState.Walk, PlayerState.Attack1,
                transition => _input.IsPressedAttack);
            _normalState.AddTransition(PlayerState.Walk, PlayerState.UltimateAttack,
                transition => _input.IsPressedUltimateAttack && _attackSystem.CanDoUltimate);
            //Attack
            _normalState.AddTransition(PlayerState.Attack1, PlayerState.Chance1);
            _normalState.AddTransition(PlayerState.Attack2, PlayerState.Chance2);
            _normalState.AddTransition(PlayerState.Attack3, PlayerState.Chance3);


            //AttackChance
            _normalState.AddTransition(PlayerState.Chance1, PlayerState.Attack2,
                transition => _input.IsPressedAttack);
            _normalState.AddTransition(PlayerState.Chance1, PlayerState.Roll,
                transition => _input.IsPressedRoll);
            _normalState.AddTransition(PlayerState.Chance1, PlayerState.UltimateAttack,
                transition => _input.IsPressedUltimateAttack && _attackSystem.CanDoUltimate);


            _normalState.AddTransition(PlayerState.Chance2, PlayerState.Attack3,
                transition => _input.IsPressedAttack);
            _normalState.AddTransition(PlayerState.Chance2, PlayerState.Roll,
                transition => _input.IsPressedRoll);
            _normalState.AddTransition(PlayerState.Chance2, PlayerState.UltimateAttack,
                transition => _input.IsPressedUltimateAttack && _attackSystem.CanDoUltimate);


            _normalState.AddTransition(PlayerState.Chance3, PlayerState.Attack1,
                transition => _input.IsPressedAttack);
            _normalState.AddTransition(PlayerState.Chance3, PlayerState.Roll,
                transition => _input.IsPressedRoll);
            _normalState.AddTransition(PlayerState.Chance3, PlayerState.UltimateAttack,
                transition => _input.IsPressedUltimateAttack && _attackSystem.CanDoUltimate);

            #endregion


            //=========================================================================


            #region Weak state

            _weakState = new StateMachine<SuperState, PlayerState, string>();

            _weakState.AddState(
                PlayerState.WeakIdle, new WeakIdle(
                    _input, _controller, animator,_attackSystem, false));
            _weakState.AddState(
                PlayerState.WeakWalk, new WeakWalk(
                    _input, _controller, animator, false));
            _weakState.AddState(
                PlayerState.WeakHurt, new WeakHurt(
                    _input, _controller, animator, _attackSystem, _playerBase, false));

            _weakState.AddTwoWayTransition(PlayerState.WeakIdle, PlayerState.WeakWalk,
                transition => _input.Move);
            _weakState.AddTwoWayTransition(PlayerState.WeakIdle, PlayerState.WeakHurt,
                transition => _playerBase.getHurt);
            _weakState.AddTransition(PlayerState.WeakWalk, PlayerState.WeakHurt,
                transition => _playerBase.getHurt);
            #endregion

            #region No Sword State

            _noSwordState = new StateMachine<SuperState, PlayerState, string>();
            _noSwordState.AddState(
                PlayerState.NoSwordRoll, new NoSwordRoll(
                    _controller, animator, _attackSystem, true));
            _noSwordState.AddState(
                PlayerState.NoSwordIdle, new NoSwordIdle(
                    _input, _controller, animator, false));
            _noSwordState.AddState(
                PlayerState.NoSwordHurt, new NoSwordHurt(
                    _input, _controller, animator,  _playerBase, false));
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
            _fsm.AddState(SuperState.Weak, _weakState);
            _fsm.AddState(SuperState.Dead, new Dead(animator, false));

            _fsm.AddTransition(SuperState.Normal, SuperState.Weak,
                transition => _attackSystem.finishChance || _playerBase.getHurt);
            _fsm.AddTransition(SuperState.Weak, SuperState.Normal,
                transition => !_attackSystem.finishChance);
            _fsm.AddTransition(SuperState.Weak, SuperState.NoSword, 
                transition => _input.IsPressedRoll);
            //for test 
            _fsm.AddTransition(SuperState.NoSword, SuperState.Normal, 
                transitionon => _attackSystem.hasSword);
            
            
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