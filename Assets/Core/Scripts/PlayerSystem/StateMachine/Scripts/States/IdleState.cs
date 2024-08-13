using PlayerSystem;

namespace BehaviourSystem.PlayerSystem
{
    public class IdleState : State<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor>
    {
        public override void EnterState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            base.EnterState(stateMachine);
            stateMachine.Context.PlayAnimation(PlayerRequestedAnimation.Idle);
        }
        public override PlayerStates GetNextState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            PlayerStates nextStateKey = stateMachine.GetStateKey;
            if (stateMachine.Context.IsMoving)
            {
                nextStateKey = PlayerStates.Move;
            }
            if (stateMachine.Context.PressedDashInput && stateMachine.Context.CanDash)
            {
                nextStateKey = PlayerStates.Dash;
            }
            if (stateMachine.Context.PressedAttackInput && stateMachine.Context.CanAttack)
            {
                nextStateKey = PlayerStates.Attack;
            }
            return nextStateKey;
        }
        public override PlayerSubStates GetNextSubState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            PlayerSubStates nextSubStateKey = stateMachine.GetSubStateKey;
            if (stateMachine.Context.IsAiming)
            {
                nextSubStateKey = PlayerSubStates.Aim;
            }
            else
            {
                nextSubStateKey = PlayerSubStates.NoAim;
            }
            return nextSubStateKey;
        }
    }
}