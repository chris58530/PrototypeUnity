using _.Scripts.Tools;
using UnityEngine;
using UnityHFSM;

namespace @_.Scripts.Player.State
{
    public class RollAttack : StateBase<PlayerState>
    {
        private readonly PlayerController _controller;
        private Timer _timer;
        private AttackSystem _attackSystem;
        private Animator _animator;
        private float aniTime;

        public RollAttack(PlayerController controller,
            Animator animator, AttackSystem attackSystem,
            bool needsExitTime, bool isGhostState = false) : base(
            needsExitTime, isGhostState)
        {
            _controller = controller;
            _animator = animator;
            _attackSystem = attackSystem;
        }

        public override void OnEnter()
        {
            DebugTools.StateText("RollAttack");
            _timer = new Timer();
            _animator.CrossFade(Animator.StringToHash("Attack1"), 0.1f);

            aniTime = _animator.GetCurrentAnimatorClipInfo(0).Length;
            _attackSystem.Attack(aniTime);
        }

        public override void OnLogic()
        {
            if (_timer.Elapsed > aniTime)
                fsm.StateCanExit();
            _controller.Fall();
        }

        public override void OnExit()
        {
        }
    }
}