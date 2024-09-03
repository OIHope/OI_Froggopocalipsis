using System.Collections.Generic;
using EnemySystem;

namespace BehaviourSystem.EnemySystem
{
    public enum EnemyState { Empty, Idle, Roaming, MoveToTarget, Attack, Stun, RunAway, TakeDamage, Spawn, Death }
    public enum EnemySubState { Empty, Invincible }

    public class ZombieStateMachine : StateMachine<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor>
    {
        private State<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> emptyState;
        private State<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> idleState;
        private State<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> roamState;
        private State<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> moveToTargetState;
        private State<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> attackState;
        private State<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> stunState;
        private State<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> runAwayState;
        private State<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> takeDamageState;
        private State<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> spawnState;
        private State<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> deathState;

        private SubState<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> emptySubState;
        private SubState<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> invincibleSubState;

        public ZombieStateMachine(MeleeZombieControllerDataAccessor dataAccessor)
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
            spawnState = new SpawnState();
            deathState = new DeathState();

            emptySubState = new EmptySubState();
            invincibleSubState = new InvincibleSubState();

            _states = new Dictionary<EnemyState, State<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor>>
            {
                { EnemyState.Empty, emptyState },
                { EnemyState.Idle, idleState },
                { EnemyState.Roaming, roamState },
                { EnemyState.MoveToTarget, moveToTargetState },
                { EnemyState.Attack, attackState },
                { EnemyState.Stun, stunState },
                { EnemyState.RunAway, runAwayState },
                { EnemyState.TakeDamage, takeDamageState },
                { EnemyState.Spawn, spawnState },
                { EnemyState.Death, deathState }
            };
            _subStates = new Dictionary<EnemySubState, SubState<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor>>
            {
                { EnemySubState.Empty, emptySubState },
                { EnemySubState.Invincible, invincibleSubState }
            };

            _stateKey = EnemyState.Spawn;
            CurrentState = _states[_stateKey];
            CurrentState.EnterState(this);

            _subStateKey = EnemySubState.Invincible;
            CurrentSubState = _subStates[_subStateKey];
            CurrentSubState.EnterSubState(this);
        }
    }
}