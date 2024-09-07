using EnemySystem;

namespace BehaviourSystem.EnemySystem
{
    public class InvincibleSubState : SubState<EnemyState, EnemySubState, EnemyControllerDataAccessor>
    {
        public override void EnterSubState(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            base.EnterSubState(stateMachine);
            stateMachine.Context.MakeInvincible(true);
        }
        public override void ExitSubState(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            base.ExitSubState(stateMachine);
            stateMachine.Context.MakeInvincible(false);
        }
    }
}