using UnityEngine;
using Components;
using Data;

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
        public bool CanAttack => _basePlayerController.AttackCooldown.CanUseAbility;
        public bool CanDash => _basePlayerController.DashCooldown.CanUseAbility;

        public Vector2 MoveInput => Input.GameplayInputMap.Move.ReadValue<Vector2>();
        public Vector2 AimInput => Input.GameplayInputMap.LookAround.ReadValue<Vector2>();

        public Vector3 AimDirection { get; set; }
        public Vector3 PointerDirection { get; set; }

        public Transform InstanceTransform => _basePlayerController.InstanceTransform;

        public int Damage => _playerControllerData.AttackData.Damage;
        public int CritChance => _playerControllerData.AttackData.CritChance;
        public float AttackSlideDistance => _playerControllerData.AttackData.AttackSlideDistance;

        public float MoveSpeed => _playerControllerData.MovementData.RunSpeed;
        public float DashSpeed => _playerControllerData.DashData.DashSpeed;
        public float AttackCooldown => _playerControllerData.AttackData.CooldownTime;
        public float DashCooldown => _playerControllerData.DashData.CooldownTime;

        public float AtttackAnimCurveDuration => Utilities.AnimationCurveDuration.Duration(AttackAnimationCurve);
        public float DashAnimCurveDuration => Utilities.AnimationCurveDuration.Duration(DashAnimationCurve);

        public AnimationCurve AttackAnimationCurve => _playerControllerData.AttackData.AttackAnimationCurve;
        public AnimationCurve DashAnimationCurve => _playerControllerData.DashData.DashAnimationCurve;

        public void PerformAttack(AttackDataSO attackDataSO, Vector3 attackDirection) => _basePlayerController.DamageDealerManager.PerformAttack(attackDataSO, attackDirection);
        public void FinishAttack() => _basePlayerController.DamageDealerManager.FinishAttack();
        public void StartAttackCooldown() => _basePlayerController.AttackCooldown.Cooldown(AttackCooldown);
        public void StartDashCooldown() => _basePlayerController.DashCooldown.Cooldown(DashCooldown);
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