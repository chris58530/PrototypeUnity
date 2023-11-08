using System;
using _.Scripts.UI;
using UnityEngine;
using UnityHFSM;

namespace _.Scripts.Player.State
{
    public class PlayerDash : StateBase<PlayerState>
    {
        private readonly PlayerController _controller;
        private Timer _timer;

        private Animator _animator;

        //Coldwater//
        public Transform _positionToSpawn;
        public GameObject _dashModel;
        private bool isGeneratingDashModel = false;
        private float timeSinceLastGenerate = 0f;
        private float generateInterval = 0.05f;

        public PlayerDash(PlayerController controller,
            Animator animator, Transform positionToSpawn, GameObject dashModel,
            bool needsExitTime, bool isGhostState = false) : base(
            needsExitTime, isGhostState)
        {
            _controller = controller;
            _animator = animator;
            _positionToSpawn = positionToSpawn;
            _dashModel = dashModel;
        }

        public override void OnEnter()
        {
            AudioManager.Instance.PlaySFX("Dash");
            if (ContextPresenter.Instance.GetAilityCount() <= 0)
            {
                fsm.StateCanExit();
            }
            ContextPresenter.Instance.UseAbility();
            _timer = new Timer();
            _animator.Play("Dash");
            _controller.Dash();            
        }

        public override void OnLogic()
        {
            timeSinceLastGenerate += Time.deltaTime;
            if (!isGeneratingDashModel && timeSinceLastGenerate >= generateInterval)
            {
                isGeneratingDashModel = true;
                Spawn();
                timeSinceLastGenerate = 0f;
            }

            if (_timer.Elapsed > _controller.dashTime)
                fsm.StateCanExit();
        }

        public void Spawn()
        {
            Quaternion spawnRotation = _positionToSpawn.rotation;
            GameObject dashModelInstance = GameObject.Instantiate(_dashModel, _positionToSpawn.position, spawnRotation);
            GameObject.Destroy(dashModelInstance, 0.5f);
            isGeneratingDashModel = false;
        }


        public override void OnExit()
        {
            Debug.Log("exit dash");
        }
    }
}