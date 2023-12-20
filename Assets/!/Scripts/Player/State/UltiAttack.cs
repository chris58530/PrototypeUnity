using _.Scripts.Tools;
using UnityEngine;
using UnityHFSM;

namespace _.Scripts.Player.State
{
    public class UltiAttack : StateBase<PlayerState>
    {
        private readonly Animator _animator;
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;
        private Timer _timer;
        private UltimateSystem _ultimateSystem;


        public UltiAttack(PlayerInput playerInput,
            PlayerController playerController,
            Animator animator, UltimateSystem ultimateSystem,
            bool needsExitTime,
            bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _input = playerInput;
            _controller = playerController;
            _animator = animator;
            _ultimateSystem = ultimateSystem;
        }

        public override void OnEnter()
        {
            DebugTools.StateText("UltimateAttack");
            _timer = new Timer();
            _ultimateSystem.UseUltimate();
            _ultimateSystem.UltimateTimer(true);
            _controller.Roll();
            _animator.Play("UltimateAttack");
            _ultimateSystem.AttackChancePreview(Color.white);
        }

        public override void OnLogic()
        {
        
         
        }

        public override void OnExit()
        {
            _ultimateSystem.finishUltiAttack = false;
            _ultimateSystem.CancelUltimate();
        }
    }
}