using System;
using _.Scripts.Event;
using _.Scripts.Player.Props;
using _.Scripts.Player.State.AbilityState;
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

        InsertSword,
        Chance1,
        Chance1ToIdle,
        Chance2,
        Chance3,
        Hurt,
        Roll,
        RollAttack,

        QuityAbility,
        StrengthAbility,
        BreakWallAbility,
        KeyAbility,
        FireAbility,
        DashAbility,
        GunAbility,
        
        Dead
    }

    public enum SuperState
    {
        Normal,
        Hammer,
    }

    public class PlayerStateMachine : MonoBehaviour
    {
        private PlayerInput _input;
        private PlayerController _controller;
        private AttackSystem _attackSystem;
        private AbilitySystem _abilitySystem;
        private PlayerBase _playerBase;
        private AbilityWeapon _abilityWeapon;

        private StateMachine<SuperState, string> _fsm;
        private StateMachine<SuperState, PlayerState, string> _normalState;
        private StateMachine<SuperState, PlayerState, string> _hammerState;

        [SerializeField] private Animator animator;
        [SerializeField] private bool isHammer;


        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
            _controller = GetComponent<PlayerController>();
            _attackSystem = GetComponent<AttackSystem>();
            _abilitySystem = GetComponent<AbilitySystem>();
            _playerBase = GetComponent<PlayerBase>();
            _abilityWeapon = GetComponentInChildren<AbilityWeapon>();
        }

        private void Start()
        {
            NormalState();
            HammerState();

            //=========================================================================
            //Initialize fsm
            _fsm = new StateMachine<SuperState, string>();
            _fsm.AddState(SuperState.Normal, _normalState);
            _fsm.AddState(SuperState.Hammer, _hammerState);

            _fsm.AddTwoWayTransition(SuperState.Normal, SuperState.Hammer,
                transition => isHammer);
    
  

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

        private void NormalState()
        {
            // if (isHammer) return;

            _normalState = new StateMachine<SuperState, PlayerState, string>();
            _normalState.AddState(
                PlayerState.Idle, new Idle(
                    _input, _controller, animator, _attackSystem, false));
            _normalState.AddState(
                PlayerState.Dead, new Dead(
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
                PlayerState.Chance2, new Chance2(
                    _input, _controller, animator, _attackSystem, false));
            _normalState.AddState(
                PlayerState.Chance3, new Chance3(
                    _input, _controller, animator, _playerBase, _attackSystem, false));
            _normalState.AddState(
                PlayerState.InsertSword, new InsertSword(
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
                transition => _input.IsPressedAbility && !_abilitySystem.isBlockInsert);
       
            _normalState.AddTwoWayTransition(PlayerState.Idle, PlayerState.Dead,
                transition =>_playerBase.isDead);
            

            //Walk
            _normalState.AddTransition(PlayerState.Walk, PlayerState.Roll,
                transition => _input.IsPressedRoll && !_controller.blockRoll);
            _normalState.AddTransition(PlayerState.Walk, PlayerState.InsertSword,
                transition => _input.IsPressedAbility&& !_abilitySystem.isBlockInsert);
            _normalState.AddTransition(PlayerState.Walk, PlayerState.Dead,
                transition =>_playerBase.isDead);

            _normalState.AddTransition(PlayerState.Roll, PlayerState.Idle,
                transition => _controller.finsihRoll);
            _normalState.AddTransition(PlayerState.Roll, PlayerState.Dead,
                transition =>_playerBase.isDead);
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
            _normalState.AddTransition(PlayerState.Attack3, PlayerState.Dead,
                transition =>_playerBase.isDead);
            //extra
            // _normalState.AddTransition(PlayerState.Chance1ToIdle, PlayerState.Fail);

            //AttackChance
            _normalState.AddTransition(PlayerState.Chance1, PlayerState.Idle,
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
                transition => _input.IsPressedAbility&& !_abilitySystem.isBlockInsert);
            _normalState.AddTransition(PlayerState.Chance1, PlayerState.Dead,
                transition =>_playerBase.isDead);
            //
            //
            _normalState.AddTransition(PlayerState.Chance2, PlayerState.Attack3,
                transition => _input.IsPressedAttack);

            _normalState.AddTransition(PlayerState.Chance2, PlayerState.Idle,
                transition => _attackSystem.finishAttack);
            _normalState.AddTransition(PlayerState.Chance2, PlayerState.Roll,
                transition => _input.IsPressedRoll && !_controller.blockRoll);
            _normalState.AddTransition(PlayerState.Chance2, PlayerState.Hurt,
                transition => _playerBase.getHurt);

            _normalState.AddTransition(PlayerState.Chance2, PlayerState.Walk,
                transition => _input.Move);
            _normalState.AddTransition(PlayerState.Chance2, PlayerState.InsertSword,
                transition => _input.IsPressedAbility&& !_abilitySystem.isBlockInsert);
            _normalState.AddTransition(PlayerState.Chance2, PlayerState.Dead,
                transition =>_playerBase.isDead);

            _normalState.AddTransition(PlayerState.Chance3, PlayerState.Attack1,
                transition => _input.IsPressedAttack);
            _normalState.AddTransition(PlayerState.Chance3, PlayerState.Idle,
                transition => _attackSystem.finishAttack);
            _normalState.AddTransition(PlayerState.Chance3, PlayerState.Roll,
                transition => _input.IsPressedRoll && !_controller.blockRoll);
            _normalState.AddTransition(PlayerState.Chance3, PlayerState.Hurt,
                transition => _playerBase.getHurt);

            _normalState.AddTransition(PlayerState.Chance3, PlayerState.Walk,
                transition => _input.Move);
            _normalState.AddTransition(PlayerState.Chance3, PlayerState.InsertSword,
                transition => _input.IsPressedAbility&& !_abilitySystem.isBlockInsert);
            _normalState.AddTransition(PlayerState.Chance3, PlayerState.Dead,
                transition =>_playerBase.isDead);


            // _normalState.AddTransition(PlayerState.Fail, PlayerState.Chance1ToIdle,
            //     transition => _attackSystem.finsihFail && _attackSystem.attackCount == 1);
            
            
            //insert 
    
            _normalState.AddTransition(PlayerState.InsertSword, PlayerState.Attack1,
                transition => _input.IsPressedAttack);
            _normalState.AddTransition(PlayerState.InsertSword, PlayerState.Idle,
                transition => _input.IsReleasedAbility);
        }

        private void HammerState()
        {
            // if (!isHammer) return;

            _hammerState = new StateMachine<SuperState, PlayerState, string>();
            _hammerState.AddState(
                PlayerState.Idle, new Idle(
                    _input, _controller, animator, _attackSystem, false));

       

            _hammerState.AddState(
                PlayerState.Walk, new Walk(
                    _input, _controller, animator, _attackSystem, false));
            _hammerState.AddState(
                PlayerState.Roll, new Roll(
                    _input, _controller, animator, _attackSystem, false));
            _hammerState.AddState(
                PlayerState.Hurt, new Hurt(
                    _input, _controller, animator, _attackSystem, _playerBase, false));
            _hammerState.AddState(
                PlayerState.RollAttack, new RollAttack(
                    _controller, animator, _attackSystem, true));
            _hammerState.AddState(
                PlayerState.Attack1, new Attack1(
                    _input, _controller, animator, _attackSystem, true));

            _hammerState.AddState(
                PlayerState.Attack2, new Attack2(
                    _input, _controller, animator, _attackSystem, true));

            _hammerState.AddState(
                PlayerState.Attack3, new Attack3(
                    _input, _controller, animator, _attackSystem, true));

            _hammerState.AddState(
                PlayerState.Chance1, new Chance1(
                    _input, _controller, animator, _attackSystem, false));
           

            _hammerState.AddState(
                PlayerState.Chance2, new Chance2(
                    _input, _controller, animator, _attackSystem, false));
            _hammerState.AddState(
                PlayerState.Chance3, new Chance3(
                    _input, _controller, animator, _playerBase, _attackSystem, false));
            _hammerState.AddState(
                PlayerState.InsertSword, new InsertSword(
                    _input, _controller, animator, _attackSystem, _abilitySystem, _playerBase, true));
            _hammerState.AddState(
                PlayerState.KeyAbility, new KeyAbility(
                    _input, _controller, animator, _attackSystem, _abilitySystem, _playerBase, true));
            _hammerState.AddState(
                PlayerState.BreakWallAbility, new BreakWallAbility(
                    _input, _controller, animator, _attackSystem, _abilitySystem, _playerBase, true));

            _hammerState.AddState(
                PlayerState.StrengthAbility, new StrengthAbility(
                    _input, _controller, animator, _attackSystem, _abilitySystem, _playerBase, true));
            _hammerState.AddState(
                PlayerState.QuityAbility, new QuityAbility(
                    _input, _controller, animator, _attackSystem, _abilityWeapon, _playerBase, true));

            _hammerState.AddState(
                PlayerState.FireAbility, new FireAbility(
                    _input, _controller, animator, _attackSystem, _abilityWeapon, _playerBase, true));
            _hammerState.AddState(
                PlayerState.DashAbility, new DashAbility(
                    _input, _controller, animator, _attackSystem, _abilityWeapon, _playerBase, true));
            _hammerState.AddState(
                PlayerState.GunAbility, new GunAbility(
                    _input, _controller, animator, _attackSystem, _abilityWeapon, _playerBase, true));


            //Idle
            _hammerState.AddTwoWayTransition(PlayerState.Idle, PlayerState.Walk,
                transition => _input.Move);
            _hammerState.AddTwoWayTransition(PlayerState.Idle, PlayerState.Hurt,
                transition => _playerBase.getHurt);
            _hammerState.AddTransition(PlayerState.Idle, PlayerState.Roll,
                transition => _input.IsPressedRoll && !_controller.blockRoll);

            _hammerState.AddTransition(PlayerState.Idle, PlayerState.Attack1,
                transition => _input.IsPressedAttack && _attackSystem.attackCount == 0 &&
                              _abilitySystem.GetCurrentAbility == AbilityWeapon.AbilityType.None);
            _hammerState.AddTransition(PlayerState.Idle, PlayerState.Attack2,
                transition => _input.IsPressedAttack && _attackSystem.attackCount == 1 &&
                              _abilitySystem.GetCurrentAbility == AbilityWeapon.AbilityType.None);
            _hammerState.AddTransition(PlayerState.Idle, PlayerState.Attack3,
                transition => _input.IsPressedAttack && _attackSystem.attackCount == 2 &&
                              _abilitySystem.GetCurrentAbility == AbilityWeapon.AbilityType.None);


            _hammerState.AddTransition(PlayerState.Idle, PlayerState.KeyAbility,
                transition => _input.IsPressedAttack &&
                              _abilitySystem.GetCurrentAbility == AbilityWeapon.AbilityType.Key);


            _hammerState.AddTransition(PlayerState.Idle, PlayerState.InsertSword,
                transition => _input.IsPressedAbility &&
                              _abilitySystem.GetCurrentAbility == AbilityWeapon.AbilityType.None);

            _hammerState.AddTransition(PlayerState.Idle, PlayerState.QuityAbility,
                transition => _input.IsPressedAbility &&
                              _abilitySystem.GetCurrentAbility != AbilityWeapon.AbilityType.None);

            _hammerState.AddTransition(PlayerState.Idle, PlayerState.DashAbility,
                transition => _input.IsPressedAttack &&
                              _abilitySystem.GetCurrentAbility == AbilityWeapon.AbilityType.Dash);
            _hammerState.AddTransition(PlayerState.Idle, PlayerState.GunAbility,
                transition => _input.IsPressedAttack &&
                              _abilitySystem.GetCurrentAbility == AbilityWeapon.AbilityType.Gun);

            _hammerState.AddTransition(PlayerState.InsertSword, PlayerState.Idle);
            _hammerState.AddTransition(PlayerState.KeyAbility, PlayerState.Idle);
            _hammerState.AddTransition(PlayerState.QuityAbility, PlayerState.Idle);
            _hammerState.AddTransition(PlayerState.DashAbility, PlayerState.Idle);
            _hammerState.AddTransition(PlayerState.GunAbility, PlayerState.Idle);


            //Walk
            _hammerState.AddTransition(PlayerState.Walk, PlayerState.DashAbility,
                transition => _input.IsPressedAttack &&
                              _abilitySystem.GetCurrentAbility == AbilityWeapon.AbilityType.Dash);


            _hammerState.AddTransition(PlayerState.Walk, PlayerState.Roll,
                transition => _input.IsPressedRoll && !_controller.blockRoll);


            _hammerState.AddTransition(PlayerState.Roll, PlayerState.Idle,
                transition => _controller.finsihRoll);
            //RollAttack
            _hammerState.AddTransition(PlayerState.RollAttack, PlayerState.Idle);

            _hammerState.AddTransition(PlayerState.Walk, PlayerState.Hurt,
                transition => _playerBase.getHurt);
            _hammerState.AddTransition(PlayerState.Walk, PlayerState.Attack1,
                transition => _input.IsPressedAttack && _attackSystem.attackCount == 0);
            _hammerState.AddTransition(PlayerState.Walk, PlayerState.Attack2,
                transition => _input.IsPressedAttack && _attackSystem.attackCount == 1);
            _hammerState.AddTransition(PlayerState.Walk, PlayerState.Attack3,
                transition => _input.IsPressedAttack && _attackSystem.attackCount == 2);
            //Attack
            _hammerState.AddTransition(PlayerState.Attack1, PlayerState.Chance1);
            _hammerState.AddTransition(PlayerState.Attack1, PlayerState.Hurt,
                transition => _playerBase.getHurt);
            _hammerState.AddTransition(PlayerState.Attack2, PlayerState.Chance2);
            _hammerState.AddTransition(PlayerState.Attack2, PlayerState.Hurt,
                transition => _playerBase.getHurt);
            _hammerState.AddTransition(PlayerState.Attack3, PlayerState.Chance3);
            _hammerState.AddTransition(PlayerState.Attack3, PlayerState.Hurt,
                transition => _playerBase.getHurt);


            //AttackChance
            _hammerState.AddTransition(PlayerState.Chance1, PlayerState.Idle,
                transition => _attackSystem.finishAttack);
            _hammerState.AddTransition(PlayerState.Chance1, PlayerState.Attack2,
                transition => _input.IsPressedAttack);
            _hammerState.AddTransition(PlayerState.Chance1, PlayerState.Roll,
                transition => _input.IsPressedRoll && !_controller.blockRoll);
            _hammerState.AddTransition(PlayerState.Chance1, PlayerState.Hurt,
                transition => _playerBase.getHurt);
            _hammerState.AddTransition(PlayerState.Chance1, PlayerState.Walk,
                transition => _input.Move);
            _hammerState.AddTransition(PlayerState.Chance1, PlayerState.InsertSword,
                transition => _input.IsPressedAbility);


            _hammerState.AddTransition(PlayerState.Chance2, PlayerState.Attack3,
                transition => _input.IsPressedAttack);

            _hammerState.AddTransition(PlayerState.Chance2, PlayerState.Idle,
                transition => _attackSystem.finishAttack);
            _hammerState.AddTransition(PlayerState.Chance2, PlayerState.Roll,
                transition => _input.IsPressedRoll && !_controller.blockRoll);
            _hammerState.AddTransition(PlayerState.Chance2, PlayerState.Hurt,
                transition => _playerBase.getHurt);

            _hammerState.AddTransition(PlayerState.Chance2, PlayerState.Walk,
                transition => _input.Move);
            _hammerState.AddTransition(PlayerState.Chance2, PlayerState.InsertSword,
                transition => _input.IsPressedAbility);

            _hammerState.AddTransition(PlayerState.Chance3, PlayerState.Attack1,
                transition => _input.IsPressedAttack);
            _hammerState.AddTransition(PlayerState.Chance3, PlayerState.Idle,
                transition => _attackSystem.finishAttack);
            _hammerState.AddTransition(PlayerState.Chance3, PlayerState.Roll,
                transition => _input.IsPressedRoll && !_controller.blockRoll);
            _hammerState.AddTransition(PlayerState.Chance3, PlayerState.Hurt,
                transition => _playerBase.getHurt);

            _hammerState.AddTransition(PlayerState.Chance3, PlayerState.Walk,
                transition => _input.Move);
            _hammerState.AddTransition(PlayerState.Chance3, PlayerState.InsertSword,
                transition => _input.IsPressedAbility);


            // _normalState.AddTransition(PlayerState.Fail, PlayerState.Chance1ToIdle,
            //     transition => _attackSystem.finsihFail && _attackSystem.attackCount == 1);
        }
    }
}