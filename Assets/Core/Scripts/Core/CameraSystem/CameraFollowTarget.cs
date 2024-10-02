using PlayerSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Camera
{
    public class CameraFollowTarget : MonoBehaviour
    {
        private InputAction _move;

        [SerializeField] private Transform _target;
        [SerializeField] private float _followSmooth = 0.35f;
        [SerializeField] private float _forwardDistance = 0.5f;
        [SerializeField] private float _cameraTransitionDuration = 1f;

        private CameraData _cameraData;
        private CameraData _previousCameraData;
        private float _cameraTransitionProgress = 0f;
        private bool _isTransitioning = false;

        private void FixedUpdate()
        {
            if (_target != null && _cameraData != null)
            {
                if (_isTransitioning)
                {
                    _cameraTransitionProgress += Time.deltaTime / _cameraTransitionDuration;

                    if (_cameraTransitionProgress >= 1f)
                    {
                        _isTransitioning = false;
                        _cameraTransitionProgress = 1f;
                    }
                }

                FollowTarget(_target);
            }
        }

        private void FollowTarget(Transform target)
        {
            Vector2 inputValue = _move.ReadValue<Vector2>();
            Vector3 inputDirection = new(inputValue.x, 0f, inputValue.y);
            bool isMoving = inputDirection != Vector3.zero;

            Vector3 position = transform.position;

            float cameraHeight = Mathf.Lerp(_previousCameraData.GetCameraHight, _cameraData.GetCameraHight, _cameraTransitionProgress);
            float cameraDepthOffset = Mathf.Lerp(_previousCameraData.GetCameraDepthOffset, _cameraData.GetCameraDepthOffset, _cameraTransitionProgress);
            Vector3 cameraBordersX = Vector2.Lerp(_previousCameraData.GetBordersX, _cameraData.GetBordersX, _cameraTransitionProgress);
            Vector3 cameraBordersZ = Vector2.Lerp(_previousCameraData.GetBordersZ, _cameraData.GetBordersZ, _cameraTransitionProgress);
            float cameraTilt = Mathf.Lerp(_previousCameraData.GetCameraTilt, _cameraData.GetCameraTilt, _cameraTransitionProgress);

            position.y = cameraHeight;

            Vector3 moveDirection = isMoving ? inputDirection.normalized * _forwardDistance : Vector3.zero;
            Vector3 targetPos = new(target.position.x, target.position.y + position.y, target.position.z + cameraDepthOffset);

            position = LimitCameraPosition(targetPos + moveDirection, cameraBordersX, cameraBordersZ);

            transform.position = Vector3.Lerp(transform.position, position, _followSmooth * Time.deltaTime);
            transform.eulerAngles = new Vector3(cameraTilt, transform.eulerAngles.y, transform.eulerAngles.z);
        }

        private Vector3 LimitCameraPosition(Vector3 position, Vector2 bordersX, Vector2 bordersZ)
        {
            position.x = Mathf.Clamp(position.x, bordersX.x, bordersX.y);
            position.z = Mathf.Clamp(position.z, bordersZ.x, bordersZ.y);

            return position;
        }

        private void OnEnable()
        {
            _move = InputManager.Instance.Input.MainInputMap.Move;
        }

        public void SetupCameraLimits(CameraData cameraData)
        {
            if (_cameraData != null)
            {
                _previousCameraData = _cameraData;
                _cameraTransitionProgress = 0f;
                _isTransitioning = true;
            }
            else
            {
                _previousCameraData = cameraData;
                _cameraTransitionProgress = 1f; // якщо немаЇ попередн≥х даних, пропускаЇмо перех≥д
            }

            _cameraData = cameraData;
        }
    }

}