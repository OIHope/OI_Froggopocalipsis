using UnityEngine.InputSystem;
using UnityEngine;

namespace PlayerSystem
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private bool _usingGamepad = false;
        private InputControls _inputControls;

        public bool IsUsingGamepad => _usingGamepad;
        public InputControls Input => _inputControls;

        private void Update()
        {
            CheckInputDeviceChange();
        }

        private void CheckInputDeviceChange()
        {
            if (IsUsingGamepad && (Keyboard.current.anyKey.wasPressedThisFrame || Mouse.current.delta.ReadValue() != Vector2.zero))
            {
                SwitchToKeyboard();
            }
            else if (!IsUsingGamepad && IsAnyGamepadInputDetected(Gamepad.current))
            {
                SwitchToGamepad();
            }
        }

        private bool IsAnyGamepadInputDetected(Gamepad gamepad)
        {
            if (gamepad == null) return false;
            return gamepad.buttonSouth.wasPressedThisFrame ||
                   gamepad.buttonNorth.wasPressedThisFrame ||
                   gamepad.buttonEast.wasPressedThisFrame ||
                   gamepad.buttonWest.wasPressedThisFrame ||
                   gamepad.leftTrigger.wasPressedThisFrame ||
                   gamepad.rightTrigger.wasPressedThisFrame ||
                   gamepad.leftShoulder.wasPressedThisFrame ||
                   gamepad.rightShoulder.wasPressedThisFrame ||
                   gamepad.selectButton.wasPressedThisFrame ||
                   gamepad.startButton.wasPressedThisFrame ||
                   gamepad.leftStickButton.wasPressedThisFrame ||
                   gamepad.rightStickButton.wasPressedThisFrame ||
                   gamepad.dpad.up.wasPressedThisFrame ||
                   gamepad.dpad.down.wasPressedThisFrame ||
                   gamepad.dpad.left.wasPressedThisFrame ||
                   gamepad.dpad.right.wasPressedThisFrame ||
                   gamepad.leftStick.ReadValue() != Vector2.zero ||
                   gamepad.rightStick.ReadValue() != Vector2.zero;
        }

        private void SwitchToKeyboard()
        {
            _usingGamepad = false;
        }
        private void SwitchToGamepad()
        {
            _usingGamepad = true;
        }

        public void InitializeInputControls()
        {
            _inputControls = new InputControls();
            EnableControls();
        }
        public void DeinitializeControls()
        {
            DisableControls();
            _inputControls = null;
        }

        public void EnableControls()
        {
            _inputControls.Enable();
        }
        public void DisableControls()
        {
            _inputControls.Disable();
        }
    }
}