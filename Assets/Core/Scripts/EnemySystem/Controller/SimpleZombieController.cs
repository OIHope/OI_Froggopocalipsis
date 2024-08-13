using BehaviourSystem.EnemySystem;
using Components;
using Data;
using EnemySystem;
using UnityEngine;
using UnityEngine.AI;

namespace Entity.EnemySystem
{
    public class SimpleZombieController : Creature, ISimpleAttacker, ISearchForTarget, IHaveMovementComponent
    {
        [Header("Data")]
        [Space]
        [SerializeField] private EnemyDataSO _enemyData;
        [SerializeField] private DamageDealer _damageDealer;
        [SerializeField] private Animator _animator;
        [Space]
        [Header("Components")]
        [Space]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private ProgressBarComponent _healthBar;
        [SerializeField] private DetectTargetComponent _targetDetector;
        [SerializeField] private NavMeshAgent _agent;

        private SimpleZombieControllerDataAccessor _dataAccessor;
        private SimpleZombieStateMachine _stateMachine;

        private CooldownComponent _attackCooldownComponent;
        private MovementComponent _movementComponent;
        private AnimationComponent _animationComponent;

        public EnemyDataSO EnemyData => _enemyData;
        public NavMeshAgent Agent { get => _agent; set => _agent = value; }
        public DamageDealer SimpleDamageDealerComponent => _damageDealer;
        public CooldownComponent SimpleAttackCooldown => _attackCooldownComponent;
        public MovementComponent MovementComponent => _movementComponent;
        public DetectTargetComponent TargetDetector => _targetDetector;
        public AnimationComponent Animation => _animationComponent;

        protected override void Awake()
        {
            base.Awake();
            StartSearching();
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
        protected override void InitStateMachine()
        {
            _dataAccessor = new(this);
            _stateMachine = new(_dataAccessor);
        }
        protected override void InitComponents()
        {
            _healthComponent = new(_enemyData.HealthData, _healthBar);
            _attackCooldownComponent = new();
            _movementComponent = new(_rigidbody);
            _animationComponent = new(_animator);

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
            _rigidbody.AddForce(impulseDirection * inpulsePower, ForceMode.Impulse);
        }
        public override void TakeDamage(AttackDataSO attackData, Vector3 attackVector)
        {
            base.TakeDamage(attackData, attackVector);
            _stateMachine.SwitchState(EnemyState.TakeDamage);
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

            _dataAccessor.TargetTransform = null;
        }
        public void StopSearching(Transform targetTransform)
        {
            if (!CheckTargetIsClose(targetTransform, _enemyData.EnemyVisionData.SightDistance)) return;
            _dataAccessor.TargetTransform = targetTransform;

            TargetDetector.DisableTargetDetection();
            TargetDetector.OnTargetDetected -= StopSearching;

            _stateMachine.SwitchState(EnemyState.MoveToTarget);
        }
    }
}