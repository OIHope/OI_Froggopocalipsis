using UnityEngine;

namespace PlayerSystem
{
    [System.Serializable]
    public class PlayerControllerContext
    {
        private GameObject _damageDealer;
        private GameObject _navigationArrow;
        private InputManager _inputManager;

        private CharacterController _controller;
        private PlayerStatsSO _playerStats;

        private Vector3 _lastMoveDirection = new(1, 0, 0);


        public GameObject DamageDealer { get => _damageDealer; set => _damageDealer = value; }
        public GameObject NavigationArrow { get => _navigationArrow; set => _navigationArrow = value; }

        public InputControls Input => _inputManager.Input;

        public CharacterController Controller => _controller;

        public bool UsingGamepad => _inputManager.IsUsingGamepad;
        public bool IsMoving => Input.GameplayInputMap.Move.ReadValue<Vector2>() != Vector2.zero;
        public bool IsAiming
        {
            get
            {
                if (UsingGamepad)
                {
                    return Input.GameplayInputMap.LookAround.ReadValue<Vector2>() != Vector2.zero;
                }
                return true;
            }
        }
        public bool PressedDashInput => Input.GameplayInputMap.Dash.IsPressed();
        public bool PressedAttackInput => Input.GameplayInputMap.Attack.IsPressed();

        public Vector2 MoveInput => Input.GameplayInputMap.Move.ReadValue<Vector2>();
        public Vector2 AimInput => Input.GameplayInputMap.LookAround.ReadValue<Vector2>();

        public Vector3 AimDirection { get; set; }
        public Vector3 PointerDirection { get; set; }

        public float StunDuration => _playerStats.StunDuration;
        public float MoveSpeed => _playerStats.MoveSpeed;
        public float DashSpeed => _playerStats.DashSpeed;
        public float DashAnimCurveDuration => Utilities.AnimationCurveDuration.Duration(DashAnimationCurve);

        public AnimationCurve DashAnimationCurve => _playerStats.DashAnimationCurve;

        public PlayerControllerContext(CharacterController controller, PlayerStatsSO playerStats, InputManager inputManager, 
            GameObject damageDealer, GameObject navigationArrow)
        {
            _controller = controller;
            _playerStats = playerStats;
            _inputManager = inputManager;
            _damageDealer = damageDealer;
            _navigationArrow = navigationArrow;
        }
        public PlayerControllerContext() { }
    }
}