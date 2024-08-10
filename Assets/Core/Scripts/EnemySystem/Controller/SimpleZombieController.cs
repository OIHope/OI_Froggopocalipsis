using BehaviourSystem.EnemySystem;
using Components;
using PlayerSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem
{
    public class SimpleZombieController : MonoBehaviour
    {
        [Header("Managers")]
        [Space]
        [SerializeField] private SimpleZombieStateMachine _stateMachine;
        [Space]
        [Header("Components")]
        [Space]
        [SerializeField] private EnemyDataSO _enemyData;
        [SerializeField] private ProgressBarComponent _healthBar;
        [SerializeField] private NavMeshAgent _agent;

        private SimpleZombieControllerDataAccessor _dataAccessor;

        private List<ComponentBase> _components;
        private HealthComponent _healthComponent;
        private CooldownComponent _attackCooldownComponent;

        public EnemyDataSO EnemyData => _enemyData;
        public NavMeshAgent Agent { get => _agent; set => _agent = value; }

        private void Awake()
        {
            _dataAccessor = new(this);
            InitComponents();

            _stateMachine.SetupStateMachine(_dataAccessor);
        }
        private void InitComponents()
        {
            _healthComponent = new(_enemyData.HealthData, _healthBar);
            _attackCooldownComponent = new();

            _components = new()
            {
                _healthComponent,
                _attackCooldownComponent,
            };
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
    }
}