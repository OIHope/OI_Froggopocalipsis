using EnemySystem;
using UnityEngine;

namespace BehaviourSystem.EnemySystem
{
    public class EmptyState : State<EnemyState, EnemySubState, EnemyControllerDataAccessor>
    {
        public override void EnterState(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            base.EnterState(stateMachine);
            stateMachine.Context.Agent.isStopped = true;
        }

        public override EnemyState GetNextState(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            return EnemyState.Empty;
        }

        public override EnemySubState GetNextSubState(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            return EnemySubState.Empty;
        }
    }
}