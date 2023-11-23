using UnityEngine;
using UnityHFSM;

namespace _.Scripts.Player.State
{
    public class PlayerDead : StateBase<PlayerState>
    {
        private Animator _animator;

        public PlayerDead(
            Animator animator,
         
            bool needsExitTime, bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _animator = animator;
           
        }

        public override void OnEnter()
        {
            AudioManager.Instance.PlaySFX("Die");
            _animator.Play("Dead");
            Debug.Log("palyer dead !!");
        }

        public override void OnLogic()
        {
        }

        public override void OnExit()
        {
            
        }
    }
}