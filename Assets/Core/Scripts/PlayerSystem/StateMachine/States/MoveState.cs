using UnityEngine;

namespace BehaviourSystem
{
    [CreateAssetMenu(fileName = "Move State", menuName = ("State Machine/State/Move State"))]
    public class MoveState : StateSO<PlayerStates, PlayerSubStates>, IGravityAffected<PlayerStateMachine>
    {
        public override void FixedUpdateState(PlayerStateMachine stateMachine)
        {
            PerformMove(stateMachine);
            HandleGravity(stateMachine);
        }
        private void PerformMove(PlayerStateMachine stateMachine)
        {
            float speed = stateMachine.Context.MoveSpeed;

            Vector2 inputDirection = stateMachine.Context.MoveInput;
            Vector3 moveDirection = new(inputDirection.x, 0f, inputDirection.y);
            Vector3 moveVector = speed * Time.deltaTime * moveDirection;

            stateMachine.Context.Move(moveVector);
        }

        public override PlayerStates GetNextState(PlayerStateMachine stateMachine)
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