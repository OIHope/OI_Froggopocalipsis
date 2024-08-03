using UnityEngine;

namespace BehaviourSystem
{
    [CreateAssetMenu(fileName = "Move State", menuName = ("State Machine/State/Move State"))]
    public class MoveState : StateSO<PlayerStates, PlayerSubStates>, IGravityAffected<PlayerStateManager>
    {
        public override void FixedUpdateState(PlayerStateManager stateMachine)
        {
            PerformMove(stateMachine);
            HandleGravity(stateMachine);
        }
        private void PerformMove(PlayerStateManager stateMachine)
        {
            float speed = stateMachine.Context.MoveSpeed;

            Vector2 inputDirection = stateMachine.Context.MoveInput;
            Vector3 moveDirection = new(inputDirection.x, 0f, inputDirection.y);

            stateMachine.Context.Controller.Move(speed * Time.deltaTime * moveDirection);
        }

        public override PlayerStates GetNextState(PlayerStateManager stateMachine)
        {
            PlayerStates nextStateKey = stateMachine.StateKey;
            if (!stateMachine.Context.IsMoving)
            {
                nextStateKey = PlayerStates.Idle;
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