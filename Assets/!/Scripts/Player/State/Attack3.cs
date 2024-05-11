using _.Scripts.Event;
using _.Scripts.Player.Props;
using _.Scripts.Tools;
using _.Scripts.UI;
using UnityEngine;
using UnityHFSM;

namespace @_.Scripts.Player.State
{
    public class Attack3 : StateBase<PlayerState>
    {
        private Animator _animator;
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;
        private Timer _timer;
        private AttackSystem _attackSystem;

        public Attack3(PlayerInput playerInput,
            PlayerController playerController,
            Animator animator, AttackSystem attackSystem,
            bool needsExitTime,
            bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _input = playerInput;
            _controller = playerController;
            _animator = animator;
            _attackSystem = attackSystem;
        }

        public override void OnEnter()
        {            
            EnemyActions.setCanDamagedEnemy?.Invoke(true);
            EnemyActions.setCanBreakBossB?.Invoke(true);

            DebugTools.StateText("AttackThird");


            _timer = new Timer();
            // _animator.CrossFade(Animator.StringToHash("Q3"), 0.1f);
            _attackSystem.UseQ3Attack();
            _attackSystem.AutoDetect();

            _animator.Play("Q3");

            // if (_input.Move)
            //     _controller.FaceInputDireaction(_input);
        }

        public override void OnLogic()
        {
            if (_timer.Elapsed > _attackSystem.AttackTime(2))
                fsm.StateCanExit();
        }

        public override void OnExit()
        {
            // _animator.CrossFade(Animator.StringToHash("Idle"), 0.8f);
            // _controller.transform.localEulerAngles = new Vector3(0
            //     , _controller.transform.rotation.y, _controller.transform.rotation.z);
     
            _attackSystem.CancelAttack();
        }
    }
}