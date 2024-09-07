using System.Collections.Generic;
using EnemySystem;
using UnityEngine;

namespace BehaviourSystem.EnemySystem
{
    public enum EnemyState { Empty, Idle, Roaming, MoveToTarget, ChargeAttack, Attack, Stun, RunAway, TakeDamage, Spawn, Death }
    public enum EnemySubState { Empty, Invincible }

    public class EnemyStateMachine : StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor>
    {
        private State<EnemyState, EnemySubState, EnemyControllerDataAccessor> emptyState;
        private State<EnemyState, EnemySubState, EnemyControllerDataAccessor> idleState;
        private State<EnemyState, EnemySubState, EnemyControllerDataAccessor> roamState;
        private State<EnemyState, EnemySubState, EnemyControllerDataAccessor> moveToTargetState;
        private State<EnemyState, EnemySubState, EnemyControllerDataAccessor> chargeAttackState;
        private State<EnemyState, EnemySubState, EnemyControllerDataAccessor> attackState;
        private State<EnemyState, EnemySubState, EnemyControllerDataAccessor> stunState;
        private State<EnemyState, EnemySubState, EnemyControllerDataAccessor> runAwayState;
        private State<EnemyState, EnemySubState, EnemyControllerDataAccessor> takeDamageState;
        private State<EnemyState, EnemySubState, EnemyControllerDataAccessor> spawnState;
        private State<EnemyState, EnemySubState, EnemyControllerDataAccessor> deathState;

        private SubState<EnemyState, EnemySubState, EnemyControllerDataAccessor> emptySubState;
        private SubState<EnemyState, EnemySubState, EnemyControllerDataAccessor> invincibleSubState;

        public EnemyStateMachine(EnemyControllerDataAccessor dataAccessor)
        {
            _dataAccessor = dataAccessor;
            _stateMachineData = dataAccessor.StateMachineData;

            if (!_stateMachineData.ForEnemyStateMachine)
            {
                Debug.Log("<color=red>Wrong StateMachineData!</color>");
            }

            emptyState = new EmptyState();
            idleState = new IdleState();
            roamState = new RoamState();
            moveToTargetState = new MoveToTargetState();
            chargeAttackState = new ChargeAttackState();
            attackState = new AttackState();
            stunState = new StunState();
            runAwayState = new RunAwayState();
            takeDamageState = new TakeDamageState();
            spawnState = new SpawnState();
            deathState = new DeathState();

            emptySubState = new EmptySubState();
            invincibleSubState = new InvincibleSubState();

            _states = new Dictionary<EnemyState, State<EnemyState, EnemySubState, EnemyControllerDataAccessor>>
            {
                { EnemyState.Empty, emptyState },
                { EnemyState.Idle, idleState },
                { EnemyState.Roaming, roamState },
                { EnemyState.MoveToTarget, moveToTargetState },
                { EnemyState.ChargeAttack, chargeAttackState },
                { EnemyState.Attack, attackState },
                { EnemyState.Stun, stunState },
                { EnemyState.RunAway, runAwayState },
                { EnemyState.TakeDamage, takeDamageState },
                { EnemyState.Spawn, spawnState },
                { EnemyState.Death, deathState }
            };
            _subStates = new Dictionary<EnemySubState, SubState<EnemyState, EnemySubState, EnemyControllerDataAccessor>>
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