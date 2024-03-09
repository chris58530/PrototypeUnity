using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _.Scripts.Player
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerCustomInput _input;
        public Vector2 MoveVector => _input.Player.Movement.ReadValue<Vector2>();

        public bool IsPressedRoll => _input.Player.Roll.WasPressedThisFrame();

        public bool IsPressedAttack => _input.Player.Attack.WasPressedThisFrame();
        public bool IsPressedAbility => _input.Player.Ability.WasPressedThisFrame();
        public bool Move => MoveVector.x != 0 || MoveVector.y != 0;


        private void Awake()
        {
            _input = new PlayerCustomInput();
        }

        private void OnEnable()
        {
            _input.Enable();
        }


        private void OnDisable()
        {
            _input.Disable();
        }
    }
}