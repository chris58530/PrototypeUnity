using TMPro;
using UnityEngine;
using UnityHFSM;
using UniRx;

namespace _.Scripts.Player.State
{
    public class PlayerDashChance : StateBase<PlayerState>
    {
        private Animator _animator;
        private readonly PlayerMapInput _input;
        private readonly PlayerController _controller;

        public PlayerDashChance(PlayerMapInput playerMapInput,
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
            //debug
            TMP_Text t = GameObject.Find("StateText").GetComponent<TMP_Text>();
            t.text = "DashChance";
        }

        public override void OnLogic()
        {
            if (_input.Move)
            {
                Vector2 getInput = _input.MoveVector;
                Vector3 dir = new Vector3(getInput.x, 0, getInput.y);
                _controller.Move(dir);
            }
            _controller.Fall();

        }

        public override void OnExit()
        {
        }
    }
}