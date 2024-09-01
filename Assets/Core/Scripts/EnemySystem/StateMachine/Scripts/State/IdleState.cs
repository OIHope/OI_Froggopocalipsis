using EnemySystem;
using UnityEngine;

namespace BehaviourSystem.EnemySystem
{
    public class IdleState : State<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor>
    {
        private float _elapsedTime = 0f;

        public override void EnterState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            base.EnterState(stateMachine);

            _elapsedTime = 0f;
            stateMachine.Context.Agent.isStopped = true;
        }
        public override void FixedUpdateState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            stateMachine.Context.PlayAnimation(EnemyRequestedAnimation.Idle);

            if (stateMachine.Context.IsStatic) return;
            _elapsedTime += Time.deltaTime;
            _isComplete = _elapsedTime >= stateMachine.Context.WaitTillRoam;
        }

        public override EnemyState GetNextState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            if (_isComplete)
            {
                return stateMachine.Context.IsStatic ? EnemyState.Idle : EnemyState.Roaming;
            }
            return EnemyState.Idle;
        }

        public override EnemySubState GetNextSubState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            return EnemySubState.Empty;
        }
    }
}