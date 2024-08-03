using UnityEngine;

namespace BehaviourSystem
{
    [CreateAssetMenu(fileName ="Idle State", menuName =("State Machine/State/Idle State"))]
    public class IdleState : StateSO<PlayerStates, PlayerSubStates>, IGravityAffected<PlayerStateManager>
    {
        public override void FixedUpdateState(PlayerStateManager stateMachine)
        {
            HandleGravity(stateMachine);
        }

        public override PlayerStates GetNextState(PlayerStateManager stateMachine)
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
        public override PlayerSubStates GetNextSubState(PlayerStateManager stateMachine)
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

        public void HandleGravity(PlayerStateManager stateMachine)
        {
            float gravity = -9.5f;
            float groundedGravity = -0.5f;
            float usedGravityValue = stateMachine.Context.Controller.isGrounded ? groundedGravity : gravity;
            stateMachine.Context.Controller.Move(Vector3.up * usedGravityValue * Time.deltaTime);
        }
    }
}