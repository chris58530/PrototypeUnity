using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _.Scripts.Player
{
    public class PlayerMapInput : MonoBehaviour
    {
        private PlayerCustomInput _input;
        public Vector2 MoveVector => _input.Player.Movement.ReadValue<Vector2>();

        public bool IsPressedDash => _input.Player.Dash.IsPressed();
        public bool IsReleasedDash => _input.Player.Dash.WasReleasedThisFrame();

        public bool IsPressedPull => _input.Player.Pull.IsPressed();
        public bool IsReleasedPull => _input.Player.Pull.WasReleasedThisFrame();
        public bool IsPressedAttack => _input.Player.Attack.IsPressed();

        public bool Move => MoveVector.x != 0 || MoveVector.y != 0;

        private void Awake()
        {
            _input = new PlayerCustomInput();
        }

        private void OnEnable()
        {
            _input.Enable();
            // _input.Player.Movement.performed += OnMovePerformed;
            // _input.Player.Dash.performed += OnDashPerformed;
            //
            // _input.Player.Movement.canceled += OnMoveCancelled;
            // _input.Player.Dash.canceled += OnDashCancelled;
        }


        private void OnDisable()
        {
            _input.Disable();
            // _input.Player.Movement.performed -= OnMovePerformed;
            // _input.Player.Dash.performed -= OnDashPerformed;
            //
            // _input.Player.Movement.canceled -= OnMoveCancelled;
            // _input.Player.Dash.canceled -= OnDashCancelled;
        }

        private void OnMovePerformed(InputAction.CallbackContext value)
        {
            // MoveVector =
            //     isMove = true;
        }

        private void OnMoveCancelled(InputAction.CallbackContext value)
        {
            // MoveVector = Vector2.zero;
            // isMove = false;
        }

        private void OnDashPerformed(InputAction.CallbackContext value)
        {
            // isDash = true;
        }

        private void OnDashCancelled(InputAction.CallbackContext value)
        {
            // isDash = false;
        }
    }
}