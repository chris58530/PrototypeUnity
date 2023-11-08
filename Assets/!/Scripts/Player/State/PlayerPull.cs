using _.Scripts.UI;
using UnityEngine;
using UnityHFSM;

namespace _.Scripts.Player.State
{
    public class PlayerPull : StateBase<PlayerState>
    {
        private Animator _animator;
        private readonly PlayerMapInput _input;
        private readonly PlayerController _controller;

        public PlayerPull(PlayerMapInput playerMapInput,
            PlayerController playerController, Animator animator,
            bool needsExitTime,
            bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _input = playerMapInput;
            _controller = playerController;
            _animator = animator;

        }

        public override void OnEnter()
        {
            if (ContextPresenter.Instance.GetAilityCount() <= 0)
            {
                fsm.StateCanExit();
            }
            ContextPresenter.Instance.UseAbility();
            _animator.Play("Pull");
        }

        public override void OnLogic()
        {
            _controller.SetPullTarget();
        }

        public override void OnExit()
        {
            _controller.PullTarget();
        }
    }
}