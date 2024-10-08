using PlayerSystem;
using UnityEngine;

namespace BehaviourSystem.PlayerSystem
{
    public class MoveState : State<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor>
    {
        public override void EnterState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            base.EnterState(stateMachine);
            stateMachine.Context.PlayAnimation(PlayerRequestedAnimation.Run);
        }
        public override void FixedUpdateState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            PerformMove(stateMachine);
        }
        private void PerformMove(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            float speed = stateMachine.Context.MoveSpeed;

            Vector2 inputDirection = stateMachine.Context.MoveInput;
            Vector3 moveDirection = new(inputDirection.x, 0f, inputDirection.y);
            Vector3 moveVector = speed * Time.deltaTime * moveDirection;

            float inputMagnitude = new Vector2(inputDirection.x, inputDirection.y).magnitude;
            if (inputMagnitude > 0.5f)
            {
                stateMachine.Context.PlayAnimation(PlayerRequestedAnimation.Run);
            }
            else
            {
                stateMachine.Context.PlayAnimation(PlayerRequestedAnimation.Walk);
            }

            stateMachine.Context.Move(moveVector);
        }

        public override PlayerStates GetNextState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            PlayerStates nextStateKey = stateMachine.CurrentStateKey;
            if (!stateMachine.Context.IsMoving)
            {
                nextStateKey = PlayerStates.Idle;
            }
            if (stateMachine.Context.PressedDashInput && stateMachine.Context.CanDash)
            {
                nextStateKey = PlayerStates.Dodge;
            }
            if (stateMachine.Context.PressedAttackInput && stateMachine.Context.CanAttack)
            {
                nextStateKey = PlayerStates.Attack;
            }
            return nextStateKey;
        }
        public override PlayerSubStates GetNextSubState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            return PlayerSubStates.NoAim;
        }
    }
}