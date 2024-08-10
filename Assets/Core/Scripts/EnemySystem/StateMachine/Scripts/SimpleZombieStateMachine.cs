using UnityEngine;
using System.Collections.Generic;
using EnemySystem;

namespace BehaviourSystem.EnemySystem
{
    public enum EnemyState { Empty, Idle, Roaming, MoveToTarget, Attack, Stun, RunAway }
    public enum EnemySubState { Empty, TakeDamage }

    public class SimpleZombieStateMachine : StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor>
    {
        [Header("Main States")]
        [Space]
        [SerializeField] private StateSO<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> emptyState;
        [SerializeField] private StateSO<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> idleState;
        [SerializeField] private StateSO<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> roamState;
        [SerializeField] private StateSO<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> moveToTargetState;
        [SerializeField] private StateSO<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> attackState;
        [SerializeField] private StateSO<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stunState;
        [SerializeField] private StateSO<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> runAwayState;
        [Space]
        [Header("Sub States")]
        [Space]
        [SerializeField] private SubStateSO<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> emptySubState;
        [SerializeField] private SubStateSO<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> takeDamageSubState;

        public override void SetupStateMachine(SimpleZombieControllerDataAccessor dataAccessor)
        {
            _dataAccessor = dataAccessor;

            _states = new Dictionary<EnemyState, StateSO<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor>>
            {
                { EnemyState.Empty, emptyState },
                { EnemyState.Idle, idleState },
                { EnemyState.Roaming, roamState },
                { EnemyState.MoveToTarget, moveToTargetState },
                { EnemyState.Attack, attackState },
                { EnemyState.Stun, stunState },
                { EnemyState.RunAway, runAwayState },
            };
            _subStates = new Dictionary<EnemySubState, SubStateSO<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor>>
            {
                { EnemySubState.Empty, emptySubState },
                { EnemySubState.TakeDamage, takeDamageSubState }
            };

            _stateKey = EnemyState.Idle;
            CurrentState = _states[_stateKey];
            CurrentState.EnterState(this);

            _subStateKey = EnemySubState.Empty;
            CurrentSubState = _subStates[_subStateKey];
            CurrentSubState.EnterSubState(this);
        }
    }
}