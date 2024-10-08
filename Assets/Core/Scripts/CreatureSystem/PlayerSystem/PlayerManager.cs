using Core.Progression;
using Core.System;
using Entity.PlayerSystem;
using Level.Stage;
using PlayerSystem;
using System;
using System.Collections;
using UnityEngine;

namespace Core
{
    public enum PlayerInputMode
    {
        Disabled, Dialogue, Main, UI, Minigame
    }
    public class PlayerManager : Manager
    {
        public static PlayerManager Instance { get; private set; }
        public Action<Vector3> OnPlayerChangeLevelStage;
        public Action<PlayerInputMode> OnRequestSwitchInputMode;
        public Action OnPlayerRestoreRequest;

        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private InputManager _inputManager;

        private PlayerController _controller;
        private PlayerControllerDataAccessor _controllerData;
        private GameObject _player;

        private PlayerInputMode _currentInputMode = PlayerInputMode.Main;

        public override IEnumerator InitManager()
        {
            _controller = _playerPrefab.GetComponent<PlayerController>();
            _controllerData = _controller.ControllerData;
            _player = _controller.transform.gameObject;

            OnPlayerChangeLevelStage += ForcePlayerChangePosition;
            OnRequestSwitchInputMode += SwitchInput;
            OnPlayerRestoreRequest += () => _controller.RestoreThisCreature();
            yield return null;
        }

        public override IEnumerator SetupManager()
        {
            GameEventsBase.OnPlayerIsStuckAndNeedsHelp += () => ForcePlayerChangePosition(Vector3.zero);
            TransitionManager.Instance.OnRoomSwitchStart += DisableInput;
            TransitionManager.Instance.OnRoomSwitchEnd += EnableInput;
            TransitionManager.Instance.OnRoomSwitchEnd += (() => SwitchInput(PlayerInputMode.Main));
            PlayerProgressionManager.Instance.OnPlayerLavelUP += ((_) => _controller.RestoreThisCreature());

            _controller.Init();

            yield return null;
        }

        private void Awake()
        {
            SingletonMethod();
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
                //DontDestroyOnLoad(gameObject);
            }
        }
    }
}