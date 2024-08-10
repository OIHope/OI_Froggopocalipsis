using BehaviourSystem.EnemySystem;
using Components;
using Data;
using EnemySystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Entity.EnemySystem
{
    public class SimpleZombieController : Creature, IAttacker
    {
        [Header("Data")]
        [Space]
        [SerializeField] private EnemyDataSO _enemyData;
        [SerializeField] private DamageDealer _damageDealer;
        [Space]
        [Header("Components")]
        [Space]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private ProgressBarComponent _healthBar;
        [SerializeField] private NavMeshAgent _agent;

        private SimpleZombieControllerDataAccessor _dataAccessor;
        private SimpleZombieStateMachine _stateMachine;

        private CooldownComponent _attackCooldownComponent;

        public EnemyDataSO EnemyData => _enemyData;
        public NavMeshAgent Agent { get => _agent; set => _agent = value; }
        public DamageDealer DamageDealerComponent => _damageDealer;
        public CooldownComponent AttackCooldown => _attackCooldownComponent;

        protected override void Awake()
        {
            base.Awake();
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

            _components = new()
            {
                _healthComponent,
                _attackCooldownComponent,
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
    }
}