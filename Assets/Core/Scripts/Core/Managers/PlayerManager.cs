using Entity.PlayerSystem;
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
        private Transform _player;

        private void Awake()
        {
            SingletonMethod();

            _controller = _playerPrefab.GetComponent<PlayerController>();
            _controllerData = _controller.ControllerData;
            _player = _controller.transform;

            OnPlayerChangeLevelStage += ForcePlayerChangePosition;
        }

        private void ForcePlayerChangePosition(Vector3 position)
        {
            _player.position = position;
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