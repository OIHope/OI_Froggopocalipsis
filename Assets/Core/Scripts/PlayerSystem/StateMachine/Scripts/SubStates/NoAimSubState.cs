using PlayerSystem;
using UnityEngine;

namespace BehaviourSystem.PlayerSystem
{
    public class NoAimSubState : SubState<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor>
    {
        public override void UpdateSubState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            WriteLastMoveDirection(stateMachine);
            RotateNavigationArrow(stateMachine);
        }
        private void RotateNavigationArrow(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            Quaternion navigationArrowRotation = Quaternion.LookRotation(stateMachine.Context.AimDirection);
            stateMachine.Context.NavigationArrow.transform.rotation = Quaternion.Euler(90f, navigationArrowRotation.eulerAngles.y, 0f);
        }
        private void WriteLastMoveDirection(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            Vector3 moveDirection = stateMachine.Context.AimDirection != Vector3.zero
                ? stateMachine.Context.AimDirection : Vector3.right;
            Vector2 inputDirection = stateMachine.Context.MoveInput;

            if (inputDirection != Vector2.zero)
            {
                stateMachine.Context.AimDirection = new(inputDirection.x, 0f, inputDirection.y);
            }
            else
            {
                stateMachine.Context.AimDirection = moveDirection;
            }
        }
    }
}