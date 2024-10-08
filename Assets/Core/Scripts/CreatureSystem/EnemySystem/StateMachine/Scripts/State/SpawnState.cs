using Data;
using EnemySystem;

namespace BehaviourSystem.EnemySystem
{
    public class SpawnState : State<EnemyState, EnemySubState, EnemyControllerDataAccessor>
    {
        public override void EnterState(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            base.EnterState(stateMachine);
            stateMachine.Context.Agent.isStopped = true;
        }

        public override void UpdateState(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            PerformSpawn(stateMachine);
        }
        private void PerformSpawn(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            stateMachine.Context.PlayAnimation(EnemyRequestedAnimation.Spawn);
            _isComplete = stateMachine.Context.AnimationComplete(stateMachine.Context.AnimationName(EnemyRequestedAnimation.Spawn));
        }
        public override EnemyState GetNextState(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            if (!_isComplete) return EnemyState.Spawn;
            return EnemyState.Idle;
        }

        public override EnemySubState GetNextSubState(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            return EnemySubState.Invincible;
        }
    }
}