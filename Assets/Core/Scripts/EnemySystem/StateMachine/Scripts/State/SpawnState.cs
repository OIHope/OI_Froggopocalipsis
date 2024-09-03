using EnemySystem;

namespace BehaviourSystem.EnemySystem
{
    public class SpawnState : State<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor>
    {
        public override void EnterState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            base.EnterState(stateMachine);
            stateMachine.Context.Agent.isStopped = true;
        }

        public override void UpdateState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            PerformSpawn(stateMachine);
        }
        private void PerformSpawn(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            stateMachine.Context.PlayAnimation(EnemyRequestedAnimation.Spawn);
            _isComplete = stateMachine.Context.AnimationComplete(stateMachine.Context.AnimationName(EnemyRequestedAnimation.Spawn));
        }
        public override EnemyState GetNextState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            if (_isComplete) return EnemyState.Idle;
            return EnemyState.Spawn;
        }

        public override EnemySubState GetNextSubState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            return EnemySubState.Invincible;
        }
    }
}