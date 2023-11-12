using TMPro;
using UnityEngine;
using UnityHFSM;

namespace _.Scripts.Player.State
{
    public class PlayerDashFail : StateBase<PlayerState>
    {
        private Animator _animator;
        private readonly PlayerMapInput _input;
        private readonly PlayerController _controller;
        private Timer _timer;

        public PlayerDashFail(PlayerMapInput playerMapInput,
            PlayerController playerController,
            Animator animator,
            bool needsExitTime,
            bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _input = playerMapInput;
            _controller = playerController;
            _animator = animator;
        }
        public override void OnEnter()
        {
            _timer = new Timer();
            //debug
            TMP_Text t = GameObject.Find("StateText").GetComponent<TMP_Text>();
            t.text = "DashFail";
        }

        public override void OnLogic()
        {
            if (_timer.Elapsed > _controller.dashFailTime)
                fsm.StateCanExit();
        }

        public override void OnExit()
        {
        }
    }
}