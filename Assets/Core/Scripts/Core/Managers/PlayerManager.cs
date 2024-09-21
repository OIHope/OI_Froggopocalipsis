using Entity.PlayerSystem;
using Level.Stage;
using PlayerSystem;
using UnityEngine;

namespace Core
{
    public enum PlayerInputMode
    {
        Disabled, Dialogue, Main, UI, Minigame
    }
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { get; private set; }
        public System.Action<Vector3> OnPlayerChangeLevelStage;
        public System.Action<PlayerInputMode> OnRequestSwitchInputMode;

        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private InputManager _inputManager;

        private PlayerController _controller;
        private PlayerControllerDataAccessor _controllerData;
        private GameObject _player;

        private PlayerInputMode _currentInputMode = PlayerInputMode.Main;

        private void Awake()
        {
            SingletonMethod();

            _controller = _playerPrefab.GetComponent<PlayerController>();
            _controllerData = _controller.ControllerData;
            _player = _controller.transform.gameObject;

            OnPlayerChangeLevelStage += ForcePlayerChangePosition;
            OnRequestSwitchInputMode += SwitchInput;
        }
        private void Start()
        {
            TransitionManager.Instance.OnRoomSwitchStart += DisableInput;
            TransitionManager.Instance.OnRoomSwitchEnd += EnableInput;
        }

        private void ForcePlayerChangePosition(Vector3 position)
        {
            _player.transform.position = position;
        }
        private void SwitchInput(PlayerInputMode inputMode)
        {
            if (_currentInputMode == inputMode) return;
            _currentInputMode = inputMode;
            _inputManager.SwitchInputScheme(_currentInputMode);
        }
        private void EnableInput()
        {
            _inputManager.EnableControls();
        }
        private void DisableInput()
        {
            _inputManager.DisableControls();
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