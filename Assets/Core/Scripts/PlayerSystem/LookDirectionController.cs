using UnityEngine;

namespace PlayerSystem
{
    public class LookDirectionController : MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayer;

        private Vector3 _lookDirection;
        private Vector3 _pointerVector;
        private Vector3 _lastMoveDirection = Vector3.right;

        public Vector3 LookDirection => _lookDirection;
        public Vector3 LastMoveDirection => _lastMoveDirection;

        public void UpdateLookDirection(bool usingGamepad, Vector3 pointerVector, Vector3 position)
        {
            _pointerVector = pointerVector;
            if (usingGamepad)
            {
                if (pointerVector != Vector3.zero)
                {
                    _lookDirection = new Vector3(pointerVector.x, 0, pointerVector.y);
                }
                else
                {
                    _lookDirection = _lastMoveDirection;
                }
            }
            else
            {
                Vector3 mouseWorldPosition = GetMouseWorldPosition();
                _lookDirection = mouseWorldPosition - position;
            }
            _lookDirection = _lookDirection.normalized;
        }
        public void UpdateLastMoveDirection(Vector3 lastDirection)
        {
            _lastMoveDirection = lastDirection;
        }

        public Vector3 GetMouseWorldPosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(_pointerVector);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
            {
                return hit.point;
            }
            return Vector3.zero;
        }
    }
}