using Entity.PlayerSystem;
using Level.Stage;
using PlayerSystem;
using UnityEngine;

namespace Core
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { get; private set; }
        public System.Action<Vector3> OnPlayerChangeLevelStage;

        [SerializeField] private GameObject _playerPrefab;

        private PlayerController _controller;
        private PlayerControllerDataAccessor _controllerData;
        private GameObject _player;

        private void Awake()
        {
            SingletonMethod();

            _controller = _playerPrefab.GetComponent<PlayerController>();
            _controllerData = _controller.ControllerData;
            _player = _controller.transform.gameObject;

            OnPlayerChangeLevelStage += ForcePlayerChangePosition;
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
        private void EnableInput()
        {
            _controller.InputManager.EnableControls();
        }
        private void DisableInput()
        {
            _controller.InputManager.DisableControls();
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