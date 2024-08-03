using UnityEngine;
using System.Collections;

namespace PlayerSystem
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _moveSpeed = 10f;
        [SerializeField] private float _dashSpeed = 20f;
        [SerializeField] private AnimationCurve _dashSpeedCurve;

        private float _actualMoveSpeed;
        private bool _inDash = false;
        private Vector3 _lastMoveDirection = Vector3.right;

        public bool IsGrounded => _characterController.isGrounded;

        private void Awake()
        {
            _actualMoveSpeed = _moveSpeed;
        }

        public void PerformMovement(Vector3 moveValue)
        {
            if (_inDash) return;

            _characterController.Move(_actualMoveSpeed * Time.deltaTime * moveValue);

            if (moveValue != Vector3.zero)
            {
                _lastMoveDirection = moveValue.normalized;
            }
        }

        public void ApplyGravity(Vector3 gravity)
        {
            _characterController.Move(gravity * Time.deltaTime);
        }

        public void StartDash()
        {
            if (!_inDash)
            {
                StartCoroutine(PerformDash());
            }
        }

        private IEnumerator PerformDash()
        {
            _inDash = true;
            float duration = _dashSpeedCurve.keys[_dashSpeedCurve.length - 1].time;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                _characterController.Move(_dashSpeedCurve.Evaluate(elapsedTime) * _dashSpeed * Time.deltaTime * _lastMoveDirection);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _inDash = false;
        }
    }
}