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
            DebugTools.StateText("Roll");
            _timer = new Timer();
            
            _animator.Play(Animator.StringToHash("Dash"));

            _controller.Roll();
            _attackSystem.ResetChance();

        }

        public override void OnLogic()
        {
            if (_timer.Elapsed > _controller.rollTime )
                fsm.StateCanExit();
            _controller.Fall();

        }

        public override void OnExit()
        {
            
        }
    }
}