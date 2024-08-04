using UnityEngine;
using BehaviourSystem;

namespace PlayerSystem
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Managers")]
        [Space]
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private PlayerStateMachine _stateManager;
        [SerializeField] private PlayerCooldownManager _cooldownManager;
        [SerializeField] private DamageDealerManager _damageDealerManager;
        [SerializeField] private DamageColliderManager _damageColliderManager;
        [SerializeField] private MovementManager _movementManager;
        [Space]
        [Header("Components")]
        [Space]
        [SerializeField] private CharacterController _controller;
        [SerializeField] private PlayerDataSO _playerDataSO;
        [Space]
        [Header("Game Objects")]
        [Space]
        [SerializeField] private GameObject _navigationArrow;

        private PlayerControllerDataAccessor _controllerDataAccessor;

        public InputManager InputManager => _inputManager;
        public PlayerCooldownManager CooldownManager => _cooldownManager;
        public PlayerStateMachine StateManager => _stateManager;
        public DamageColliderManager DamageColliderManager => _damageColliderManager;
        public DamageDealerManager DamageDealerManager => _damageDealerManager;
        public MovementManager MovementManager => _movementManager;
        public CharacterController Controller => _controller;
        public PlayerDataSO PlayerData => _playerDataSO;
        public GameObject NavigationArrow { get => _navigationArrow; set => _navigationArrow = value; }


        private void Awake()
        {
            _controllerDataAccessor = new PlayerControllerDataAccessor(this);
            _inputManager.InitializeInputControls();
            _stateManager.SetupStateManager(_controllerDataAccessor);
        }
    }
}