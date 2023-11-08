using UnityEngine;
using UnityHFSM;

namespace _.Scripts.Player.State
{
    public class PlayerDead : StateBase<PlayerState>
    {
        private Animator _animator;
        private PlayerHp _playerHp;

        public PlayerDead(
            Animator animator,
            PlayerHp playerHp,
            bool needsExitTime, bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _animator = animator;
            _playerHp = playerHp;
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
            _playerHp.Dead = false;
        }
    }
}