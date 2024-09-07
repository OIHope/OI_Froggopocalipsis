using Data;
using EnemySystem;

namespace BehaviourSystem.EnemySystem
{
    public class DeathState : State<EnemyState, EnemySubState, EnemyControllerDataAccessor>
    {
        public override void EnterState(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            base.EnterState(stateMachine);
            if (stateMachine.Context.Agent.enabled)
            {
                stateMachine.Context.Agent.isStopped = true;
            }
            PerformDeath(stateMachine);
        }
        private void PerformDeath(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            stateMachine.Context.PlayAnimation(EnemyRequestedAnimation.Die);
        }
        public override EnemyState GetNextState(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            return EnemyState.Death;
        }

        public override EnemySubState GetNextSubState(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            return EnemySubState.Invincible;
        }
    }
}