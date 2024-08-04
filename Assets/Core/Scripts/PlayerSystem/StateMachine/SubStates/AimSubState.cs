using UnityEngine;

namespace BehaviourSystem
{
    [CreateAssetMenu(fileName = "Aim SubState", menuName = ("State Machine/SubState/Aim SubState"))]
    public class AimSubState : SubStateSO
    {
        [SerializeField] private LayerMask _groundLayer;

        public override void UpdateSubState(PlayerStateMachine stateMachine)
        {
            UpdateLookDirection(stateMachine);
            RotateNavigationArrow(stateMachine);
        }
        private void UpdateLookDirection(PlayerStateMachine stateMachine)
        {
            Vector2 pointerVector = PointerVector(stateMachine);
            Vector3 aimDirection;

            if (stateMachine.Context.UsingGamepad)
            {
                aimDirection = new Vector3(pointerVector.x, 0, pointerVector.y);
            }
            else
            {
                Vector3 mouseWorldPosition = GetMouseWorldPosition();
                aimDirection = mouseWorldPosition - stateMachine.transform.position;
            }
            stateMachine.Context.AimDirection = aimDirection.normalized;
        }

        private void RotateNavigationArrow(PlayerStateMachine stateMachine)
        {
            Quaternion navigationArrowRotation = Quaternion.LookRotation(stateMachine.Context.AimDirection);
            stateMachine.Context.NavigationArrow.transform.rotation = Quaternion.Euler(90f, navigationArrowRotation.eulerAngles.y, 0f);
        }

        private Vector3 GetMouseWorldPosition()
        {
            Vector3 mousePositionWorld = Vector3.zero;
            Vector3 mousePositionScreen = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePositionScreen);

            if (Physics.Raycast(ray, out RaycastHit hitData, Mathf.Infinity, _groundLayer))
            {
                mousePositionWorld = hitData.point;
            }
            return mousePositionWorld;
        }
        private Vector2 PointerVector(PlayerStateMachine stateMachine)
        {
            Vector2 inputVector = stateMachine.Context.AimInput;
            return inputVector;
        }
    }
}