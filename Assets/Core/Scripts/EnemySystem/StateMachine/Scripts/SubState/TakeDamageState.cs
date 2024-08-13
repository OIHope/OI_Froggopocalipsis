using EnemySystem;

namespace BehaviourSystem.EnemySystem
{
    public class TakeDamageState : State<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor>
    {
        public override void EnterState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            base.EnterState(stateMachine);
            stateMachine.Context.PlayAnimation(EnemyRequestedAnimation.TakeDamage);
        }

        public override EnemyState GetNextState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            bool targetIsAlive = stateMachine.Context.TargetTransform != null;
            if (!targetIsAlive)
            {
                stateMachine.Context.EnableTargetDetector();
                return EnemyState.Idle;
            }
            return EnemyState.MoveToTarget;
        }
        public override EnemySubState GetNextSubState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            return EnemySubState.Empty;
        }
    }
}