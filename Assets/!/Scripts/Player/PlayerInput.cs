using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.XInput;

namespace _.Scripts.Player
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerCustomInput _input;
        public Vector2 MoveVector => _input.Player.Movement.ReadValue<Vector2>();

        public bool IsPressedRoll => _input.Player.Roll.WasPressedThisFrame();

        public bool IsPressedAttack => _input.Player.Attack.WasPressedThisFrame();
        public bool IsReleasedAttack => _input.Player.Attack.WasReleasedThisFrame();
        public bool IsPressedAbility => _input.Player.Ability.WasPressedThisFrame();
        public bool IsReleasedAbility => _input.Player.Ability.WasReleasedThisFrame();
        public bool Move => MoveVector.x != 0 || MoveVector.y != 0;


        private void Awake()
        {
            _input = new PlayerCustomInput();
            
            OpenInput();
        }

        private void OnEnable()
        {
            TimeLineManager.onPlayTimelLine += CloseInput;
            TimeLineManager.onQuitTimelLine += OpenInput;
        }


        private void OnDisable()
        {
            TimeLineManager.onPlayTimelLine -= CloseInput;
            TimeLineManager.onQuitTimelLine -= OpenInput;

            CloseInput();
        }


        private void Update()
        {
            Debug.Log(Gamepad.current);
            Debug.Log(Keyboard.current);
            Debug.Log(Mouse.current);
        }

        private void OpenInput()
        {
            _input.Enable();
        }

        private void CloseInput()
        {
            _input.Disable();
        }
    }
}