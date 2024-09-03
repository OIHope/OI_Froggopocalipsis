using EnemySystem;

namespace BehaviourSystem.EnemySystem
{
    public class InvincibleSubState : SubState<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor>
    {
        public override void EnterSubState(StateMachine<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> stateMachine)
        {
            base.EnterSubState(stateMachine);
            stateMachine.Context.MakeInvincible(true);
        }
        public override void ExitSubState(StateMachine<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> stateMachine)
        {
            base.ExitSubState(stateMachine);
            stateMachine.Context.MakeInvincible(false);
        }
    }
}