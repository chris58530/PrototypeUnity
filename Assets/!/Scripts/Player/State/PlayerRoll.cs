using UnityHFSM;

namespace _.Scripts.Player.State
{
    public class PlayerRoll : StateBase<PlayerState>
    {
        public PlayerRoll(bool needsExitTime, bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
        }

        public override void OnEnter()
        {
        }

        public override void OnLogic()
        {
        }

        public override void OnExit()
        {
        }
    }
}