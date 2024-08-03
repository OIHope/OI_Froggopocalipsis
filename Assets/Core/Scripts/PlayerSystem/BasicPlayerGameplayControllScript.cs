using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace PlayerSystem
{
    public class BasicPlayerGameplayControllScript : MonoBehaviour
    {
        public bool usingStickDirection = false;
        public bool usingMoveDirection = false;
        public bool usingMouseDirection = false;

        public LayerMask groundLayer;

        private InputControls _inputSystem;

        private InputAction _move;
        private InputAction _dash;
        private InputAction _interact;
        private InputAction _attack;
        private InputAction _lookAround;

        [SerializeField] private bool _usingGamepad = false;

        [SerializeField] private Vector3 _lastMoveDirection = new(1f, 0f, 0f);
        [SerializeField] Vector3 _lookDirection = new(1f, 0f, 0f);

        [SerializeField] private Vector3 _stickDirection;
        [SerializeField] private Vector3 _mousePosition;
        [SerializeField] private Vector3 _pointerVector;

        [SerializeField] private CharacterController _characterController;

        [SerializeField] private float _moveSpeed = 2f;
        [SerializeField] float _actualMoveSpeed;

        [SerializeField] private bool _inDash = false;
        [SerializeField] private float _dashSpeed = 5f;
        [SerializeField] private AnimationCurve _dashSpeedCurve;

        [SerializeField] private GameObject _damageDealler;
        [SerializeField] private float _damageDeallerDistance = 2f;

        [SerializeField] private Vector3 _damageDeallerInitPos = new(0f, 0.2f, 0f);


        private void Awake()
        {
            _inputSystem = new InputControls();

            _usingGamepad = false;
            SetupInputControls();

            _damageDealler.SetActive(false);
            _actualMoveSpeed = _moveSpeed;
        }

        private void SetupInputControls()
        {
            _move = _inputSystem.GameplayInputMap.Move;
            _dash = _inputSystem.GameplayInputMap.Dash;
            _interact = _inputSystem.GameplayInputMap.Interact;
            _attack = _inputSystem.GameplayInputMap.Attack;
            _lookAround = _inputSystem.GameplayInputMap.LookAround;

            _dash.performed += OnDash;
            _interact.performed += OnInteract;
            _attack.performed += OnAttack;
            _lookAround.performed += OnLookAround;

            _lookAround.canceled += OnLookAroundGamepadStickRelease;
        }

        private void OnLookAround(InputAction.CallbackContext context)
        {
            

            _pointerVector = context.ReadValue<Vector2>();
        }
        private void OnLookAroundGamepadStickRelease(InputAction.CallbackContext context)
        {
            _stickDirection = new Vector2(0, 0);
        }

        private Vector3 GetMouseWorldPosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(_pointerVector);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
            {
                return hit.point;
            }
            return Vector3.zero;
        }
        private void ManageLookDirection()
        {
            Vector3 lookDirection = Vector3.zero;

            if (_usingGamepad && _pointerVector != Vector3.zero)
            {
                usingStickDirection = true;
                usingMoveDirection = false;
                usingMouseDirection = false;

                lookDirection = new Vector3(_pointerVector.x, 0, _pointerVector.y);
            }
            else if (_usingGamepad && _pointerVector == Vector3.zero)
            {
                usingStickDirection = false;
                usingMoveDirection = true;
                usingMouseDirection = false;

                lookDirection = _lastMoveDirection;
            }
            else
            {
                usingStickDirection = false;
                usingMoveDirection = false;
                usingMouseDirection = true;

                Vector3 mouseWorldPosition = GetMouseWorldPosition();
                lookDirection = mouseWorldPosition - transform.position;
            }

            _lookDirection = lookDirection.normalized;
        }
        private void OnDrawGizmos()
        {
            Vector3 startPos = new(transform.position.x, transform.position.y + 1f, transform.position.z);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(startPos, (startPos + _lastMoveDirection));

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(startPos, (startPos + _lookDirection));

            if (Application.isPlaying && !_usingGamepad)
            {
                Vector3 targetPosition = GetMouseWorldPosition();
                if (targetPosition != Vector3.zero)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawCube(targetPosition, new Vector3(0.2f, 0.2f, 0.2f));
                }
            }
        }

        private void PerformMovement()
        {
            if (_inDash) return;

            Vector2 inputValue = _move.ReadValue<Vector2>();
            Vector3 moveValue = new(inputValue.x, 0f, inputValue.y);

            _characterController.Move(_actualMoveSpeed * Time.deltaTime * moveValue);

            if (inputValue == Vector2.zero) return;

            Vector3 moveToPoint = transform.position + moveValue;
            _lastMoveDirection = (moveToPoint - transform.position).normalized;
        }

        private void OnDash(InputAction.CallbackContext callback)
        {
            StartCoroutine(PerformDash());
        }
        private IEnumerator PerformDash()
        {
            _inDash = true;

            float duration = AnimationCurveDuration.Duration(_dashSpeedCurve);
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                _characterController.Move(_dashSpeedCurve.Evaluate(elapsedTime) * _dashSpeed * Time.deltaTime * _lastMoveDirection);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _inDash = false;
        }
        private void OnInteract(InputAction.CallbackContext callback)
        {
        }
        private void OnAttack(InputAction.CallbackContext callback)
        {
            StartCoroutine(SpawnAttack());
        }
        private IEnumerator SpawnAttack()
        {
            _actualMoveSpeed = _moveSpeed / 4;

            _attack.Disable();
            _damageDealler.SetActive(true);

            yield return new WaitForSeconds(0.2f);

            _actualMoveSpeed = _moveSpeed;

            _attack.Enable();
            _damageDealler.SetActive(false);

        }

        private void Update()
        {
            ManageLookDirection();
        }

        private void FixedUpdate()
        {
            PerformMovement();
            HandleGravity();
            PositionDamageDealler();
        }

        private void HandleGravity()
        {
            float gravity = -9.5f;
            float groundedGravity = -0.5f;

            float usedGravityValue = _characterController.isGrounded ? groundedGravity : gravity;
            Vector3 gravityDirection = new(0f, usedGravityValue, 0f);

            _characterController.SimpleMove(gravityDirection);
        }
        private void PositionDamageDealler()
        {
            Vector3 position = transform.position + _damageDeallerInitPos + _lookDirection * _damageDeallerDistance;
            Quaternion rotation = Quaternion.LookRotation(_lookDirection);

            _damageDealler.transform.SetPositionAndRotation(position, rotation);
        }

        private void Log(string message)
        {
            Debug.Log(message);
        }
        private void OnEnable()
        {
            _inputSystem.Enable();
        }
        private void OnDisable()
        {
            _inputSystem.Disable();
        }
    }
}