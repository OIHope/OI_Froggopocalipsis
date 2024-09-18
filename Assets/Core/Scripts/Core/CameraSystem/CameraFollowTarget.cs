using PlayerSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Camera
{
    public class CameraFollowTarget : MonoBehaviour
    {

        private InputControls _inputSystem;
        private InputAction _move;

        [SerializeField] private Transform _target;

        [SerializeField] private float _followSmooth = 0.35f;
        [SerializeField] private float _forwardDistance = 0.5f;

        [SerializeField] private float _depthDistance = 0.5f;

        private CameraData _cameraData;

        private void FixedUpdate()
        {
            if (_target != null && _cameraData != null)
            {
                FollowTarget(_target);
            }
        }
        private void FollowTarget(Transform target)
        {
            Vector2 inputValue = _move.ReadValue<Vector2>();
            Vector3 inputDirection = new(inputValue.x, 0f, inputValue.y);

            bool isMoving = inputDirection != Vector3.zero;

            Vector3 position = transform.position;
            position.y = _cameraData.GetCameraHight;

            Vector3 moveDirection = isMoving ? inputDirection.normalized * _forwardDistance : Vector3.zero;
            Vector3 targetPos = new(target.position.x, target.position.y + position.y, target.position.z + _depthDistance);

            position = Vector3.Lerp(transform.position, targetPos + moveDirection, _followSmooth);

            transform.position = LimitCameraPosition(position);
        }
        private Vector3 LimitCameraPosition(Vector3 position)
        {
            float minX = _cameraData.GetBordersX.x;
            float maxX = _cameraData.GetBordersX.y;
            float minZ = _cameraData.GetBordersZ.x;
            float maxZ = _cameraData.GetBordersZ.y;

            position.x = Mathf.Clamp(position.x, minX, maxX);
            position.z = Mathf.Clamp(position.z, minZ, maxZ);

            return position;
        }

        private void OnEnable()
        {
            _inputSystem = new();
            _inputSystem.Enable();
            _move = _inputSystem.GameplayInputMap.Move;
        }
        private void OnDisable()
        {
            _inputSystem.Disable();
        }

        public void SetupCameraLimits(CameraData cameraData)
        {
            _cameraData = cameraData;
        }
    }
}