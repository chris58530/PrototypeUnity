using UnityEngine;
using UnityHFSM;

namespace _.Scripts.Player.State
{
    public class PlayerHurt : StateBase<PlayerState>
    {
        private readonly PlayerController _controller;
        private Animator _animator;
        private Timer _timer;
        private float _aniTime;

        public PlayerHurt(
            PlayerController playerController,
            Animator animator,
            bool
                needsExitTime, bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _controller = playerController;
            _animator = animator;
        }

        public override void OnEnter()
        {
            _timer = new Timer();

            AudioManager.Instance.PlaySFX("Injured");
            _animator.Play("Hurt");
            _aniTime = _animator.GetCurrentAnimatorStateInfo(0).length;
        }

        public override void OnLogic()
        {
            if (_timer.Elapsed > _aniTime / 2)
                fsm.StateCanExit();
        }

        public override void OnExit()
        {
        }
    }
}