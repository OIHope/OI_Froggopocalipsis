using PlayerSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicCameraFollowPlayerScript : MonoBehaviour
{

    private InputControls _inputSystem;
    private InputAction _move;

    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _cameraPos;

    [SerializeField] private float _followSmooth = 0.35f;
    [SerializeField] private float _releaseSmooth = 0.1f;
    [SerializeField] private float _forwardDistance = 0.5f;

    private void FixedUpdate()
    {
        FollowTarget(_target);
    }
    private void FollowTarget(Transform target)
    {
        Vector2 inputValue = _move.ReadValue<Vector2>();
        Vector3 inputDirection = new(inputValue.x, 0f, inputValue.y);

        bool isMoving = inputDirection != Vector3.zero;

        Vector3 moveDirection = isMoving ? inputDirection.normalized * _forwardDistance : Vector3.zero;
        Vector3 targetPos = _target.position + _cameraPos;

        if (isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos + moveDirection, _followSmooth);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, _releaseSmooth);
        }

        
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
}
