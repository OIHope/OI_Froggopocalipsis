using UnityEngine.InputSystem;
using UnityEngine;
using Core;

namespace PlayerSystem
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

        [SerializeField] private bool _usingGamepad = false;
        private InputControls _inputControls;

        public bool IsUsingGamepad => _usingGamepad;
        public InputControls Input => _inputControls;

        private void Awake()
        {
            SingletonMethod();
        }
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
            GameEventsBase.IsUsingKeybord?.Invoke();
            _usingGamepad = false;
            Cursor.visible = true;
        }
        private void SwitchToGamepad()
        {
            GameEventsBase.IsUsingGamepad?.Invoke();
            _usingGamepad = true;
            Cursor.visible = false;
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

        public void SwitchInputScheme(PlayerInputMode mode)
        {
            switch (mode)
            {
                case PlayerInputMode.Disabled:
                    DisableAll(); break;
                case PlayerInputMode.Dialogue:
                    DisableAll();
                    _inputControls.DialogueInputMap.Enable();
                    break;
                case PlayerInputMode.Main:
                    DisableAll();
                    _inputControls.MainInputMap.Enable();
                    break;
                case PlayerInputMode.UI:
                    DisableAll();
                    _inputControls.UIInputMap.Enable();
                    break;
                default:
                    DisableAll();
                    _inputControls.MainInputMap.Enable();
                    break;
            }
        }
        private void DisableAll()
        {
            _inputControls.MainInputMap.Disable();
            _inputControls.UIInputMap.Disable();
            _inputControls.DialogueInputMap.Disable();
            _inputControls.MinigameInputMap.Disable();
        }

        private void SingletonMethod()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}