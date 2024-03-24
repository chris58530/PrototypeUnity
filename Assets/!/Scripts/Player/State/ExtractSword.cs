using _.Scripts.Tools;
using UnityEngine;
using UnityHFSM;

namespace @_.Scripts.Player.State
{
    public class ExtractSword : StateBase<PlayerState>
    {
        private readonly PlayerController _controller;
        private Timer _timer;
        private AttackSystem _attackSystem;
        private Animator _animator;
        private readonly PlayerInput _input;

        public ExtractSword(PlayerInput playerInput,PlayerController controller,
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
            Debug.Log("ExtractSword");
            DebugTools.StateText("ExtractSword");
            _timer = new Timer();
            if (_input.Move)
                _controller.FaceInputDireaction(_input);
         
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