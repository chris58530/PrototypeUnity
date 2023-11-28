using _.Scripts.Tools;
using UnityEngine;
using UnityHFSM;

namespace @_.Scripts.Player.State
{
    public class NoSwordRoll : StateBase<PlayerState>
    {
        private readonly PlayerController _controller;
        private Timer _timer;
        private PlayerAttackSystem _attackSystem;
        private Animator _animator;

        public NoSwordRoll(PlayerController controller,
            Animator animator, PlayerAttackSystem attackSystem,
            bool needsExitTime, bool isGhostState = false) : base(
            needsExitTime, isGhostState)
        {
            _controller = controller;
            _animator = animator;
            _attackSystem = attackSystem;
        }

        public override void OnEnter()
        {
            Debug.Log("NoSwordRoll");
            DebugTools.StateText("NoSwordRoll");
            _timer = new Timer();

            _animator.Play(Animator.StringToHash("Dash"));
            _controller.Roll();
            _attackSystem.AttackChancePreview(Color.white);
            _attackSystem.ResetWeak();
            _attackSystem.NoSwordRoll();
        }

        public override void OnLogic()
        {
            if (_timer.Elapsed > _controller.rollTime)
                fsm.StateCanExit();
            _controller.Fall();
        }

        public override void OnExit()
        {
        }
    }
}