using BehaviourSystem;
using BehaviourSystem.EnemySystem;
using Components;
using Data;
using EnemySystem;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Entity.EnemySystem
{
    public class EnemyController : Creature, ISimpleAttacker, ISearchForTarget, IHaveMovementComponent, IInvincibility, IMoveInteractor
    {
        [Header("Data")]
        [Space]
        [SerializeField] private EnemyDataSO _enemyData;
        [SerializeField] private StateMachineDataSO _stateMachineData;
        [SerializeField] private ZombieAnimationNameDataSO _zombieAnimationNameData;
        [Space]
        [SerializeField] private LayersDataSO _layersDataSO;
        [Space]
        [Header("Components")]
        [Space]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;
        [Space]
        [SerializeField] private Animator _animator;
        [Space]
        [SerializeField] private ProgressBarComponent _healthBar;
        [SerializeField] private GameObject _healthBarObject;
        [Space]
        [SerializeField] private DetectTargetComponent _targetDetector;
        [Space]
        [SerializeField] private NavMeshAgent _agent;
        [Space]
        [Header("Melee Type Components")]
        [Header("Toggle bool to make this creature MELEE TYPE!")]
        [Space]
        [SerializeField] private bool _isMeleeType;
        [SerializeField] private DamageDealer _damageDealer;
        [Space]
        [Header("Ranged Type Components")]
        [Header("Toggle bool to make this creature RANGE TYPE!")]
        [Space]
        [SerializeField] private bool _isRangedType;
        [SerializeField] private GameObject _aimArrow;

        private bool _isInvincible = false;

        private EnemyControllerDataAccessor _dataAccessor;
        private EnemyStateMachine _stateMachine;

        private CooldownComponent _attackCooldownComponent;
        private MovementComponent _movementComponent;
        private AnimationComponent _animationComponent;
        private ColliderSwitchComponent _colliderSwitchComponent;


        public bool Invincible { get => _isInvincible; set => _isInvincible = value; }

        public GameObject AimArrow { get => _aimArrow; set => _aimArrow = value; }
        public StateMachineDataSO StateMachineData => _stateMachineData;
        public ColliderSwitchComponent ColliderSwitch => _colliderSwitchComponent;
        public EnemyStateMachine StateMachine => _stateMachine;
        public EnemyDataSO EnemyData => _enemyData;
        public ZombieAnimationNameDataSO AnimationNameData => _zombieAnimationNameData;
        public NavMeshAgent Agent { get => _agent; set => _agent = value; }
        public DamageDealer SimpleDamageDealerComponent => _damageDealer;
        public CooldownComponent SimpleAttackCooldown => _attackCooldownComponent;
        public MovementComponent MovementComponent => _movementComponent;
        public DetectTargetComponent TargetDetector => _targetDetector;
        public AnimationComponent Animation => _animationComponent;
        public HealthComponent HealthComponent => _healthComponent;

        public bool InstanceInMove => _agent.hasPath && _isAlive;
        public bool RangedType => _isRangedType;

        private void Awake()
        {
            Init();
        }
        public override void Init()
        {
            base.Init();
            StartCoroutine(SpawnEntity());
        }

        private void Update()
        {
            _stateMachine.UpdateStateMachine();

            if (!_isAlive) return;
            foreach (var component in _components) component.UpdateComponent();
        }
        private void FixedUpdate()
        {
            _stateMachine.FixedUpdateStateMachine();
            if (!_isAlive) return;
        }

        private IEnumerator SpawnEntity()
        {
            TargetDetector.DisableTargetDetection();
            yield return new WaitUntil(() => _dataAccessor.AnimationComplete(_dataAccessor.AnimationName(EnemyRequestedAnimation.Spawn)));
            StartSearching();
        }

        protected override void InitStateMachine()
        {
            _dataAccessor = new(this);
            _stateMachine = new(_dataAccessor);
        }
        protected override void InitComponents()
        {
            _agent.enabled = true;

            _healthComponent = new(_enemyData.HealthData, _healthBar);
            _attackCooldownComponent = new();
            _movementComponent = new(_rigidbody);
            _animationComponent = new(_animator);
            _colliderSwitchComponent = new(_collider, _layersDataSO);

            _components = new()
            {
                _healthComponent,
                _attackCooldownComponent,
                _movementComponent,
                _animationComponent,
            };
        }

        public override void ApplyImpulseOnCreature(Vector3 impulseDirection, float inpulsePower)
        {
            int multiplier = _stateMachine.CurrentStateKey.Equals(EnemyState.TakeDamage) ? 8 : 3;
            _rigidbody.AddForce(impulseDirection * (inpulsePower * multiplier), ForceMode.Impulse);
        }
        public override void TakeDamage(AttackDataSO attackData, Vector3 attackVector, IDamagable target)
        {
            if (Invincible) return;
            base.TakeDamage(attackData, attackVector, target);
            if (_isAlive && _stateMachine.StateMachineData.EnTakeDamageState)
            {
                _stateMachine.SwitchState(EnemyState.TakeDamage);
            }
            GameEventsBase.OnEnemyHit?.Invoke();
        }

        public bool CheckTargetIsClose(Transform targetTransform, float triggerDistance)
        {
            float distance = Vector3.Distance(targetTransform.position, transform.position);
            return triggerDistance >= distance;
        }
        public void StartSearching()
        {
            TargetDetector.EnableTargetDetection();
            TargetDetector.OnTargetDetected += StopSearching;
            TargetDetector.OnTargetLost -= StartSearching;

            _dataAccessor.Target = null;
        }
        public void StopSearching(IAttackableTarget target)
        {
            if (!CheckTargetIsClose(target.InstanceTransform, _enemyData.EnemyVisionData.SightDistance)) return;
            _dataAccessor.Target = target;

            TargetDetector.DisableTargetDetection();
            TargetDetector.OnTargetDetected -= StopSearching;
            TargetDetector.OnTargetLost += StartSearching;

            _stateMachine.SwitchState(EnemyState.MoveToTarget);
        }

        public void MakeInvincible(bool value)
        {
            Invincible = value;
        }
        public void ToggleColliders(bool value)
        {
            if (value)
            {
                _colliderSwitchComponent.CollideWithLayers();
            }
            else
            {
                _colliderSwitchComponent.DontCollideWithLayers();
            }
        }

        protected override void CreatureDeath(HealthComponent healthComponent)
        {
            if(healthComponent != _healthComponent) return;

            _isAlive = false;

            ToggleColliders(false);
            _healthBarObject.SetActive(false);
            _targetDetector.DisableTargetDetection();
            _targetDetector.enabled = false;

            _healthComponent.OnDeath -= CreatureDeath;

            _stateMachine.SwitchState(EnemyState.Death);
            _agent.enabled = false;

            GameEventsBase.OnEnemyDeath?.Invoke();
        }
    }
}