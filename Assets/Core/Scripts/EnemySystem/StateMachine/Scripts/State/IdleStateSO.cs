using EnemySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourSystem.EnemySystem
{
    [CreateAssetMenu(fileName = "Idle State", menuName = ("State Machine/Enemy/State/Idle State"))]
    public class IdleStateSO : StateSO<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor>
    {
        [Header("Defies if will roam around or stand still")]
        [SerializeField] private bool _static = false;
        [Space]
        [Header("Only if is not static")]
        [SerializeField] private float _waitTillRoamTime;

        private float _elapsedTime = 0f;

        public override void EnterState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            base.EnterState(stateMachine);
            _elapsedTime = 0f;
        }
        public override void FixedUpdateState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            if(_static) return;
            _elapsedTime += Time.deltaTime;
            _isComplete = _elapsedTime >= _waitTillRoamTime;
        }

        public override EnemyState GetNextState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            if (_isComplete)
            {
                return _static ? EnemyState.Idle : EnemyState.Roaming;
            }
            return EnemyState.Idle;
        }

        public override EnemySubState GetNextSubState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            return EnemySubState.Empty;
        }
    }
}