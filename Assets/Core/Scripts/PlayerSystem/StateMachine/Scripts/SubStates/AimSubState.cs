using PlayerSystem;
using UnityEngine;

namespace BehaviourSystem.PlayerSystem
{
    public class AimSubState : SubState<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor>
    {
        public override void UpdateSubState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            UpdateLookDirection(stateMachine);
            RotateNavigationArrow(stateMachine);
        }
        private void UpdateLookDirection(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            Vector2 pointerVector = PointerVector(stateMachine);
            Vector3 aimDirection;

            if (stateMachine.Context.UsingGamepad)
            {
                aimDirection = new Vector3(pointerVector.x, 0, pointerVector.y);
            }
            else
            {
                Vector3 mouseWorldPosition = GetMouseWorldPosition(stateMachine.Context.GroundLayer);
                aimDirection = mouseWorldPosition - stateMachine.Context.InstanceTransform.position;
            }
            stateMachine.Context.AimDirection = aimDirection.normalized;
        }

        private void RotateNavigationArrow(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            Quaternion navigationArrowRotation = Quaternion.LookRotation(stateMachine.Context.AimDirection);
            stateMachine.Context.NavigationArrow.transform.rotation = Quaternion.Euler(90f, navigationArrowRotation.eulerAngles.y, 0f);
        }

        private Vector3 GetMouseWorldPosition(LayerMask groundLayer)
        {
            Vector3 mousePositionWorld = Vector3.zero;
            Vector3 mousePositionScreen = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePositionScreen);

            if (Physics.Raycast(ray, out RaycastHit hitData, Mathf.Infinity, groundLayer))
            {
                mousePositionWorld = hitData.point;
            }
            return mousePositionWorld;
        }
        private Vector2 PointerVector(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            Vector2 inputVector = stateMachine.Context.AimInput;
            return inputVector;
        }
    }
}