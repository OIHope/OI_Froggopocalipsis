using UnityEngine;

namespace BehaviourSystem
{
    [CreateAssetMenu(fileName ="Idle State", menuName =("State Machine/State/Idle State"))]
    public class IdleState : StateSO<PlayerStates, PlayerSubStates>, IGravityAffected<PlayerStateMachine>
    {
        public override void FixedUpdateState(PlayerStateMachine stateMachine)
        {
            HandleGravity(stateMachine);
        }

        public override PlayerStates GetNextState(PlayerStateMachine stateMachine)
        {
            PlayerStates nextStateKey = stateMachine.StateKey;
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
        public override PlayerSubStates GetNextSubState(PlayerStateMachine stateMachine)
        {
            PlayerSubStates nextSubStateKey = stateMachine.SubStateKey;
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

        public void HandleGravity(PlayerStateMachine stateMachine)
        {
            stateMachine.Context.HandleGravity(false);
        }
    }
}