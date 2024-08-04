using UnityEngine;

namespace BehaviourSystem
{
    [CreateAssetMenu(fileName = "NoAim SubState", menuName = ("State Machine/SubState/NoAim SubState"))]
    public class NoAimSubState : SubStateSO
    {
        public override void UpdateSubState(PlayerStateMachine stateMachine)
        {
            WriteLastMoveDirection(stateMachine);
            RotateNavigationArrow(stateMachine);
        }
        private void RotateNavigationArrow(PlayerStateMachine stateMachine)
        {
            Quaternion navigationArrowRotation = Quaternion.LookRotation(stateMachine.Context.AimDirection);
            stateMachine.Context.NavigationArrow.transform.rotation = Quaternion.Euler(90f, navigationArrowRotation.eulerAngles.y, 0f);
        }
        private void WriteLastMoveDirection(PlayerStateMachine stateMachine)
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