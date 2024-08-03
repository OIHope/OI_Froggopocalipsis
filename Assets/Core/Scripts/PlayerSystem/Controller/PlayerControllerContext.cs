using UnityEngine;

namespace PlayerSystem
{
    [System.Serializable]
    public class PlayerControllerContext
    {
        private GameObject _damageDealerPivot;
        private GameObject _navigationArrow;
        private InputManager _inputManager;
        private PlayerCooldownManager _cooldownManager;

        private CharacterController _controller;
        private PlayerStatsSO _playerStats;


        public GameObject DamageDealerPivot { get => _damageDealerPivot; set => _damageDealerPivot = value; }
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
        public bool CanAttack => _cooldownManager.CanAttack;
        public bool CanDash => _cooldownManager.CanDash;

        public Vector2 MoveInput => Input.GameplayInputMap.Move.ReadValue<Vector2>();
        public Vector2 AimInput => Input.GameplayInputMap.LookAround.ReadValue<Vector2>();

        public Vector3 AimDirection { get; set; }
        public Vector3 PointerDirection { get; set; }

        public int Damage => _playerStats.Damage;
        public int CritChance => _playerStats.CritChance;
        public float AttackSlideDistance => _playerStats.AttackSlideDistance;

        public float StunDuration => _playerStats.StunDuration;
        public float MoveSpeed => _playerStats.MoveSpeed;
        public float DashSpeed => _playerStats.DashSpeed;
        public float AttackCooldown => _playerStats.AttackCooldown;
        public float DashCooldown => _playerStats.DashCooldown;

        public float AtttackAnimCurveDuration => Utilities.AnimationCurveDuration.Duration(AttackAnimationCurve);
        public float DashAnimCurveDuration => Utilities.AnimationCurveDuration.Duration(DashAnimationCurve);

        public AnimationCurve AttackAnimationCurve => _playerStats.AttackAnimationCurve;
        public AnimationCurve DashAnimationCurve => _playerStats.DashAnimationCurve;

        public void StartAttackCooldown() => _cooldownManager.ReloadAttack(AttackCooldown);
        public void StartDashCooldown() => _cooldownManager.ReloadDash(DashCooldown);

        public PlayerControllerContext(CharacterController controller, PlayerStatsSO playerStats, InputManager inputManager, 
            GameObject damageDealer, GameObject navigationArrow, PlayerCooldownManager cooldownManager)
        {
            _controller = controller;
            _playerStats = playerStats;
            _inputManager = inputManager;
            _damageDealerPivot = damageDealer;
            _navigationArrow = navigationArrow;
            _cooldownManager = cooldownManager;
        }
        public PlayerControllerContext() { }
    }
}