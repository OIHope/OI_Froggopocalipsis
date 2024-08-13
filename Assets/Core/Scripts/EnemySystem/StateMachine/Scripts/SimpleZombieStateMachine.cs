using System.Collections.Generic;
using EnemySystem;

namespace BehaviourSystem.EnemySystem
{
    public enum EnemyState { Empty, Idle, Roaming, MoveToTarget, Attack, Stun, RunAway, TakeDamage }
    public enum EnemySubState { Empty }

    public class SimpleZombieStateMachine : StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor>
    {
        private State<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> emptyState;
        private State<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> idleState;
        private State<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> roamState;
        private State<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> moveToTargetState;
        private State<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> attackState;
        private State<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stunState;
        private State<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> runAwayState;
        private State<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> takeDamageState;

        private SubState<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> emptySubState;

        public SimpleZombieStateMachine(SimpleZombieControllerDataAccessor dataAccessor)
        {
            _dataAccessor = dataAccessor;

            emptyState = new EmptyState();
            idleState = new IdleState();
            roamState = new RoamState();
            moveToTargetState = new MoveToTargetState();
            attackState = new SimpleAttackState();
            stunState = new StunState();
            runAwayState = new RunAwayState();
            takeDamageState = new TakeDamageState();

            emptySubState = new EmptySubState();

            _states = new Dictionary<EnemyState, State<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor>>
            {
                { EnemyState.Empty, emptyState },
                { EnemyState.Idle, idleState },
                { EnemyState.Roaming, roamState },
                { EnemyState.MoveToTarget, moveToTargetState },
                { EnemyState.Attack, attackState },
                { EnemyState.Stun, stunState },
                { EnemyState.RunAway, runAwayState },
                { EnemyState.TakeDamage, takeDamageState }
            };
            _subStates = new Dictionary<EnemySubState, SubState<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor>>
            {
                { EnemySubState.Empty, emptySubState }
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