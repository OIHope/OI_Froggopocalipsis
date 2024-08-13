using Components;
using PlayerSystem;

namespace BehaviourSystem.PlayerSystem
{
    public class DashAttackSubState : SubState<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor>
    {
        public override void EnterSubState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            stateMachine.Context.PerformAttack(AttackType.DashAttack);
            stateMachine.Context.StartAttackCooldown(AttackType.DashAttack);
            stateMachine.Context.PlayAnimation(PlayerRequestedAnimation.DashAttack);
        }

        public override void ExitSubState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            stateMachine.Context.FinishAttack(AttackType.DashAttack);
        }
    }
}