using UnityEngine;

namespace PlayerSystem
{
    [System.Serializable]
    public class PlayerControllerDataAccessor
    {
        private PlayerController _basePlayerController;
        private PlayerDataSO _playerControllerData;

        public GameObject NavigationArrow { get => _basePlayerController.NavigationArrow; set => _basePlayerController.NavigationArrow = value; }

        public InputControls Input => _basePlayerController.InputManager.Input;

        public CharacterController Controller => _basePlayerController.Controller;

        public bool UsingGamepad => _basePlayerController.InputManager.IsUsingGamepad;
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
        public bool CanAttack => _basePlayerController.CooldownManager.CanAttack;
        public bool CanDash => _basePlayerController.CooldownManager.CanDash;

        public Vector2 MoveInput => Input.GameplayInputMap.Move.ReadValue<Vector2>();
        public Vector2 AimInput => Input.GameplayInputMap.LookAround.ReadValue<Vector2>();

        public Vector3 AimDirection { get; set; }
        public Vector3 PointerDirection { get; set; }

        public int Damage => _playerControllerData.Damage;
        public int CritChance => _playerControllerData.CritChance;
        public float AttackSlideDistance => _playerControllerData.AttackSlideDistance;

        public float StunDuration => _playerControllerData.StunDuration;
        public float MoveSpeed => _playerControllerData.MoveSpeed;
        public float DashSpeed => _playerControllerData.DashSpeed;
        public float AttackCooldown => _playerControllerData.AttackCooldown;
        public float DashCooldown => _playerControllerData.DashCooldown;

        public float AtttackAnimCurveDuration => Utilities.AnimationCurveDuration.Duration(AttackAnimationCurve);
        public float DashAnimCurveDuration => Utilities.AnimationCurveDuration.Duration(DashAnimationCurve);

        public AnimationCurve AttackAnimationCurve => _playerControllerData.AttackAnimationCurve;
        public AnimationCurve DashAnimationCurve => _playerControllerData.DashAnimationCurve;

        public void PerformAttack(AttackDataSO attackDataSO, Vector3 attackDirection) => _basePlayerController.DamageDealerManager.PerformAttack(attackDataSO, attackDirection);
        public void FinishAttack() => _basePlayerController.DamageDealerManager.FinishAttack();
        public void StartAttackCooldown() => _basePlayerController.CooldownManager.ReloadAttack(AttackCooldown);
        public void StartDashCooldown() => _basePlayerController.CooldownManager.ReloadDash(DashCooldown);
        public void DontCollideWithEnemy() => _basePlayerController.DamageColliderManager.DontCollideWithEnemy();
        public void CollideWithEnemy() => _basePlayerController.DamageColliderManager.CollideWithEnemy();
        public void Move(Vector3 moveVector) => _basePlayerController.MovementManager.Move(moveVector);
        public void HandleGravity(bool specialGravity) => _basePlayerController.MovementManager.HandleGravity(specialGravity);

        public PlayerControllerDataAccessor(PlayerController basePlayerController)
        {
            _basePlayerController = basePlayerController;
            _playerControllerData = basePlayerController.PlayerData;
        }
        public PlayerControllerDataAccessor() { }
    }
}