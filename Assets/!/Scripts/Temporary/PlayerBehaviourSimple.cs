using _.Scripts.Player;
using UnityEngine;

namespace _.Scripts.Temporary
{
    [RequireComponent(typeof(CharacterController), (typeof(PlayerMapInput)))]
    public class PlayerBehaviourSimple : MonoBehaviour
    {
        public bool canMove;
        public bool canFall;
        [SerializeField] private float walkSpeed;
        [SerializeField] private float rotateSpeed;
        [SerializeField] private float gravity;

        protected CharacterController controller;
        protected PlayerMapInput input;
        private bool IsGround => controller.isGrounded;

        private void Awake()
        {
            input = GetComponent<PlayerMapInput>();
            controller = GetComponent<CharacterController>();
        }

        protected virtual void Update()
        {
            Fall();
            Move();
        }

        private void Move()
        {
            if (!canMove) return;
            if(!input.Move)return;
            Vector2 getInput = input.MoveVector;
            Vector3 dir = new Vector3(getInput.x, 0, getInput.y);
            Quaternion toRotation = Quaternion.LookRotation(dir, transform.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
            controller.Move(dir * (walkSpeed * (Time.deltaTime)));
        }

        private void Fall()
        {
            if (!canFall) return;
            if (IsGround) return;
            controller.Move(transform.up * (gravity * Time.deltaTime));
        }
    }
}