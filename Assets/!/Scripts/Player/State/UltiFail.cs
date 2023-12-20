using _.Scripts.Tools;
using UnityEngine;
using UnityHFSM;

namespace @_.Scripts.Player.State
{
    public class UltiFail : StateBase<PlayerState>
    {
        private readonly Animator _animator;
        private readonly PlayerInput _input;
        private readonly PlayerController _controller;
        private Timer _timer;
        private UltimateSystem _ultimateSystem;
        private AttackSystem _attackSystem;

        public UltiFail(PlayerInput playerInput,
            PlayerController playerController,
            Animator animator, UltimateSystem ultimateSystem,
            AttackSystem attackSystem,
            bool needsExitTime,
            bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _input = playerInput;
            _controller = playerController;
            _animator = animator;
            _ultimateSystem = ultimateSystem;
            _attackSystem = attackSystem;
        }

        public override void OnEnter()
        {
            DebugTools.StateText("UltiFial");
            _timer = new Timer();
            _ultimateSystem.AttackChancePreview(Color.red);
            Debug.Log("--------接大招失敗!--------");
            _ultimateSystem.finishUltimate = true;
        }

        public override void OnLogic()
        {
        }

        public override void OnExit()
        {
            Debug.Log("--------ResetUltimate!--------");
            _ultimateSystem.UltimateTimer(false);
            _ultimateSystem.finishUltimate = false;
            _ultimateSystem.ultimateCount = 0;
            _attackSystem.SetSwordLevel(0);
            _animator.CrossFade(Animator.StringToHash("Idle"), 0.1f);
        }
    }
}