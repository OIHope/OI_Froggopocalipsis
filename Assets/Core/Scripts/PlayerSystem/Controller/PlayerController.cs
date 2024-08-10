using UnityEngine;
using BehaviourSystem;
using Components;
using System.Collections.Generic;

namespace PlayerSystem
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Managers")]
        [Space]
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private PlayerStateMachine _stateManachine;
        [SerializeField] private DamageDealerManager _damageDealerManager;
        [SerializeField] private DamageColliderManager _damageColliderManager;
        [SerializeField] private MovementManager _movementManager;
        [Space]
        [Header("Components")]
        [Space]
        [SerializeField] private Transform _instanceTransform;
        [SerializeField] private CharacterController _controller;
        [Space]
        [SerializeField] private ProgressBarComponent _healthBar;
        [SerializeField] private ProgressBarComponent _attackCooldownBar;
        [SerializeField] private ProgressBarComponent _dashCooldownBar;
        [Space]
        [SerializeField] private PlayerDataSO _playerDataSO;
        [Space]
        [Header("Game Objects")]
        [Space]
        [SerializeField] private GameObject _navigationArrow;

        private PlayerControllerDataAccessor _controllerDataAccessor;

        private List<ComponentBase> _components;
        private HealthComponent _healthComponent;
        private CooldownComponent _attackCooldownComponent;
        private CooldownComponent _dashCooldownComponent;

        public InputManager InputManager => _inputManager;
        public PlayerStateMachine StateManachine => _stateManachine;
        public CooldownComponent AttackCooldown => _attackCooldownComponent;
        public CooldownComponent DashCooldown => _dashCooldownComponent;
        public DamageColliderManager DamageColliderManager => _damageColliderManager;
        public DamageDealerManager DamageDealerManager => _damageDealerManager;
        public MovementManager MovementManager => _movementManager;
        public CharacterController Controller => _controller;
        public PlayerDataSO PlayerData => _playerDataSO;
        public GameObject NavigationArrow { get => _navigationArrow; set => _navigationArrow = value; }
        public Transform InstanceTransform { get => _instanceTransform; set => _instanceTransform = value; }


        private void Awake()
        {
            _controllerDataAccessor = new PlayerControllerDataAccessor(this);
            _inputManager.InitializeInputControls();
            InitComponents();

            _stateManachine.SetupStateMachine(_controllerDataAccessor);
        }

        private void InitComponents()
        {
            _healthComponent = new(_playerDataSO.HealthData, _healthBar);
            _attackCooldownComponent = new(_attackCooldownBar);
            _dashCooldownComponent = new(_dashCooldownBar);

            _components = new()
            {
                _healthComponent,
                _attackCooldownComponent,
                _dashCooldownComponent
            };
        }

        private void Update()
        {
            _stateManachine.UpdateStateMachine();
            foreach (var component in _components) component.UpdateComponent();
        }
        private void FixedUpdate()
        {
            _stateManachine.FixedUpdateStateMachine();
        }
    }
}