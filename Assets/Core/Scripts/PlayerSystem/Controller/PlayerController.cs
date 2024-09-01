using UnityEngine;
using Components;
using BehaviourSystem.PlayerSystem;
using Data;
using PlayerSystem;

namespace Entity.PlayerSystem
{
    public class PlayerController : Creature, ISimpleAttacker, IDasher, IAttackableTarget, IHaveMovementComponent
    {
        [Header("Managers")]
        [Space]
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private DamageDealer _simpleDamageDealer;
        [Space]
        [Header("Components")]
        [Space]
        [SerializeField] private Transform _instanceTransform;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;
        [SerializeField] private Animator _animator;
        [Space]
        [SerializeField] private ProgressBarComponent _healthBar;
        [SerializeField] private ProgressBarComponent _attackCooldownBar;
        [SerializeField] private ProgressBarComponent _dashCooldownBar;
        [Space]
        [SerializeField] private PlayerDataSO _playerDataSO;
        [SerializeField] private LayersDataSO _layersDataSO;
        [Space]
        [Header("Game Objects")]
        [Space]
        [SerializeField] private GameObject _navigationArrow;

        private PlayerControllerDataAccessor _dataAccessor;
        protected PlayerStateMachine _stateMachine;

        private MovementComponent _movementComponent;
        private ColliderSwitchComponent _colliderSwitchComponent;
        private CooldownComponent _attackCooldownComponent;
        private CooldownComponent _dashCooldownComponent;
        private AnimationComponent _animationComponent;

        public InputManager InputManager => _inputManager;
        public PlayerDataSO PlayerData => _playerDataSO;
        public LayersDataSO LayersData => _layersDataSO;
        public GameObject NavigationArrow { get => _navigationArrow; set => _navigationArrow = value; }
        public Transform InstanceTransform { get => _instanceTransform; set => _instanceTransform = value; }
        public DamageDealer SimpleDamageDealerComponent => _simpleDamageDealer;
        public MovementComponent MovementComponent => _movementComponent;
        public CooldownComponent SimpleAttackCooldown => _attackCooldownComponent;
        public CooldownComponent DashCooldown => _dashCooldownComponent;
        public ColliderSwitchComponent ColliderSwitch => _colliderSwitchComponent;
        public AnimationComponent Animation => _animationComponent;


        protected override void Awake()
        {
            base.Awake();
            _inputManager.InitializeInputControls();
        }

        private void Update()
        {
            _stateMachine.UpdateStateMachine();
            foreach (var component in _components) component.UpdateComponent();
        }
        private void FixedUpdate()
        {
            _stateMachine.FixedUpdateStateMachine();
        }
        protected override void InitComponents()
        {
            _healthComponent = new(_playerDataSO.HealthData, _healthBar);
            _movementComponent = new(_rigidbody);
            _colliderSwitchComponent = new(_collider, _layersDataSO.EnemyLayer);
            _attackCooldownComponent = new(_attackCooldownBar);
            _dashCooldownComponent = new(_dashCooldownBar);
            _animationComponent = new(_animator);

            _components = new()
            {
                _healthComponent,
                _movementComponent,
                _colliderSwitchComponent,
                _attackCooldownComponent,
                _dashCooldownComponent,
                _animationComponent,
            };
        }
        protected override void InitStateMachine()
        {
            _dataAccessor = new PlayerControllerDataAccessor(this);
            _stateMachine = new(_dataAccessor);
        }
        public override void ApplyImpulseOnCreature(Vector3 impulseDirection, float inpulsePower)
        {
            _rigidbody.AddForce(impulseDirection * inpulsePower, ForceMode.Impulse);
        }
        public override void TakeDamage(AttackDataSO attackData, Vector3 attackVector)
        {
            base.TakeDamage(attackData, attackVector);
            
        }
        protected override void CreatureDeath(HealthComponent healthComponent)
        {
            _isAlive = false;

            _healthComponent.OnDeath -= CreatureDeath;

            _stateMachine.SwitchState(PlayerStates.Empty);
            _stateMachine.SwitchSubState(PlayerSubStates.Empty);

            _dataAccessor.PlayAnimation(PlayerRequestedAnimation.Die);
        }
    }
}