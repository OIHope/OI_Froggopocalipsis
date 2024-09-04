using BehaviourSystem.EnemySystem;
using Components;
using Data;
using EnemySystem;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Entity.EnemySystem
{
    public class MeleeZombieController : Creature, ISimpleAttacker, ISearchForTarget, IHaveMovementComponent, IInvincibility, IMoveInteractor
    {
        [Header("Data")]
        [Space]
        [SerializeField] private EnemyDataSO _enemyData;
        [SerializeField] private ZombieAnimationNameDataSO _zombieAnimationNameData;
        [SerializeField] private DamageDealer _damageDealer;
        [SerializeField] private Animator _animator;
        [Space]
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private LayerMask _playerLayer;
        [Space]
        [Header("Components")]
        [Space]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;
        [SerializeField] private ProgressBarComponent _healthBar;
        [SerializeField] private DetectTargetComponent _targetDetector;
        [SerializeField] private NavMeshAgent _agent;

        private bool _isInvincible = false;

        private MeleeZombieControllerDataAccessor _dataAccessor;
        private ZombieStateMachine _stateMachine;

        private CooldownComponent _attackCooldownComponent;
        private MovementComponent _movementComponent;
        private AnimationComponent _animationComponent;

        public bool Invincible { get => _isInvincible; set => _isInvincible = value; }

        public ZombieStateMachine StateMachine => _stateMachine;
        public EnemyDataSO EnemyData => _enemyData;
        public ZombieAnimationNameDataSO AnimationNameData => _zombieAnimationNameData;
        public NavMeshAgent Agent { get => _agent; set => _agent = value; }
        public DamageDealer SimpleDamageDealerComponent => _damageDealer;
        public CooldownComponent SimpleAttackCooldown => _attackCooldownComponent;
        public MovementComponent MovementComponent => _movementComponent;
        public DetectTargetComponent TargetDetector => _targetDetector;
        public AnimationComponent Animation => _animationComponent;

        public bool InstanceInMove => _agent.hasPath && _isAlive;

        protected override void Awake()
        {
            base.Awake();
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
            _rigidbody.AddForce(impulseDirection * (inpulsePower * 3), ForceMode.Impulse);
        }
        public override void TakeDamage(AttackDataSO attackData, Vector3 attackVector)
        {
            if (Invincible) return;
            base.TakeDamage(attackData, attackVector);
            if (_isAlive)
            {
                _stateMachine.SwitchState(EnemyState.TakeDamage);
            }
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

            _dataAccessor.TargetTransform = null;
        }
        public void StopSearching(Transform targetTransform)
        {
            if (!CheckTargetIsClose(targetTransform, _enemyData.EnemyVisionData.SightDistance)) return;
            _dataAccessor.TargetTransform = targetTransform;

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
                //_collider.includeLayers -= _playerLayer;
                //_collider.includeLayers -= _enemyLayer;

                _collider.excludeLayers += _playerLayer;
                _collider.excludeLayers += _enemyLayer;
            }
            else
            {
                //_collider.includeLayers += _playerLayer;
                //_collider.includeLayers += _enemyLayer;

                _collider.excludeLayers -= _playerLayer;
                _collider.excludeLayers -= _enemyLayer;
            }
        }

        protected override void CreatureDeath(HealthComponent healthComponent)
        {
            _isAlive = false;

            ToggleColliders(false);

            _healthComponent.OnDeath -= CreatureDeath;

            _stateMachine.SwitchState(EnemyState.Death);
        }
    }
}