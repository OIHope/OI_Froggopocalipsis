using EnemySystem;

namespace BehaviourSystem.EnemySystem
{
    public class DeathState : State<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor>
    {
        public override void EnterState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            base.EnterState(stateMachine);
            stateMachine.Context.Agent.isStopped = true;
            PerformDeath(stateMachine);
        }
        private void PerformDeath(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            stateMachine.Context.PlayAnimation(EnemyRequestedAnimation.Die);
        }
        public override EnemyState GetNextState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            return EnemyState.Death;
        }

        public override EnemySubState GetNextSubState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            return EnemySubState.Invincible;
        }
    }
}