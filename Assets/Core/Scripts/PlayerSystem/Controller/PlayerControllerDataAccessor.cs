using UnityEngine;
using Components;
using Data;
using Entity.PlayerSystem;

namespace PlayerSystem
{
    [System.Serializable]
    public class PlayerControllerDataAccessor
    {
        private PlayerController _controllerData;

        public GameObject NavigationArrow { get => _controllerData.NavigationArrow; set => _controllerData.NavigationArrow = value; }

        public InputControls Input => _controllerData.InputManager.Input;

        public bool UsingGamepad => _controllerData.InputManager.IsUsingGamepad;
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
        public bool CanAttack => _controllerData.AttackCooldown.CanUseAbility;
        public bool CanDash => _controllerData.DashCooldown.CanUseAbility;

        public Vector2 MoveInput => Input.GameplayInputMap.Move.ReadValue<Vector2>();
        public Vector2 AimInput => Input.GameplayInputMap.LookAround.ReadValue<Vector2>();

        public Vector3 AimDirection { get; set; }

        public Transform InstanceTransform => _controllerData.InstanceTransform;

        public float AttackSlideDistance => _controllerData.PlayerData.AttackData.AttackSlideDistance;

        public float MoveSpeed => _controllerData.PlayerData.MovementData.RunSpeed;
        public float DashSpeed => _controllerData.PlayerData.DashData.DashSpeed;

        public float AtttackAnimCurveDuration => Utilities.AnimationCurveDuration.Duration(AttackAnimationCurve);
        public float DashAnimCurveDuration => Utilities.AnimationCurveDuration.Duration(DashAnimationCurve);

        public AnimationCurve AttackAnimationCurve => _controllerData.PlayerData.AttackData.AttackAnimationCurve;
        public AnimationCurve DashAnimationCurve => _controllerData.PlayerData.DashData.DashAnimationCurve;

        public LayerMask GroundLayer => _controllerData.LayersData.GroundLayer;

        public void PerformAttack(AttackType attackType) => _controllerData.DamageDealerComponent.PerformAttack(AimDirection, attackType);
        public void FinishAttack() => _controllerData.DamageDealerComponent.FinishAttack();
        public void StartAttackCooldown() => _controllerData.AttackCooldown.Cooldown(_controllerData.DamageDealerComponent.LastAttackData.CooldownTime);
        public void StartDashCooldown() => _controllerData.DashCooldown.Cooldown(_controllerData.PlayerData.DashData.CooldownTime);
        public void DontCollideWithEnemy() => _controllerData.ColliderSwitch.DontCollideWithEnemy();
        public void CollideWithEnemy() => _controllerData.ColliderSwitch.CollideWithEnemy();
        public void Move(Vector3 moveVector) => _controllerData.MovementComponent.Move(moveVector);

        public PlayerControllerDataAccessor(PlayerController basePlayerController)
        {
            _controllerData = basePlayerController;
        }
    }
}