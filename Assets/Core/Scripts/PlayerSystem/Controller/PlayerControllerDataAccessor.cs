using UnityEngine;
using Components;
using Data;
using Entity.PlayerSystem;

namespace PlayerSystem
{
    public enum PlayerRequestedAnimation
    { Idle, Walk, Run, SimpleAttack, Dash, DashAttack }
    public class PlayerControllerDataAccessor
    {
        private PlayerController _controllerData;

        public GameObject NavigationArrow { get => _controllerData.NavigationArrow; set => _controllerData.NavigationArrow = value; }

        public InputControls Input => _controllerData.InputManager.Input;
        private AnimationComponent Animation => _controllerData.Animation;

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
        public bool CanAttack => _controllerData.SimpleAttackCooldown.CanUseAbility;
        public bool CanDash => _controllerData.DashCooldown.CanUseAbility;

        public Vector2 MoveInput => Input.GameplayInputMap.Move.ReadValue<Vector2>();
        public Vector2 AimInput => Input.GameplayInputMap.LookAround.ReadValue<Vector2>();

        public Vector3 AimDirection { get; set; }

        public Transform InstanceTransform => _controllerData.InstanceTransform;

        public float SimpleAttackSlideDistance => _controllerData.SimpleDamageDealerComponent.AttackAnimationData.SlideDistance;

        public float MoveSpeed => _controllerData.PlayerData.MovementData.RunSpeed;
        public float DashSpeed => _controllerData.PlayerData.DashData.DashSpeed;

        public float SimpleAttackDuration => _controllerData.SimpleDamageDealerComponent.AttackAnimationData.Duration;
        public float DashAttackDuration => _controllerData.DashDamageDealerComponent.AttackAnimationData.Duration;
        public float DashAnimationCurveDuration => _controllerData.PlayerData.DashData.Duration;

        public AnimationCurve SimpleAttackAnimationCurve => _controllerData.SimpleDamageDealerComponent.AttackAnimationData.DataAnimationCurve;
        public AnimationCurve DashAnimationCurve => _controllerData.PlayerData.DashData.DashAnimationCurve;

        public LayerMask GroundLayer => _controllerData.LayersData.GroundLayer;

        public void PlayAnimation(PlayerRequestedAnimation request)
        {
            switch (request)
            {
                case PlayerRequestedAnimation.Idle:
                    Animation.PlayAnimation("anim_player_idle");
                    break;
                case PlayerRequestedAnimation.Run:
                    Animation.PlayAnimation("anim_player_move");
                    break;
                case PlayerRequestedAnimation.SimpleAttack:
                    Animation.PlayAnimation("anim_player_simpleAttack");
                    break;
                case PlayerRequestedAnimation.DashAttack:
                    Animation.PlayAnimation("anim_player_dashAttack");
                    break;
                case PlayerRequestedAnimation.Dash:
                    Animation.PlayAnimation("anim_player_dash");
                    break;
            }
        }
        public void PerformAttack(AttackType attackType)
        {
            switch (attackType)
            {
                case AttackType.SimpleAttack:
                    _controllerData.SimpleDamageDealerComponent.PerformAttack(AimDirection);
                    break;
                case AttackType.DashAttack:
                    _controllerData.DashDamageDealerComponent.PerformAttack(AimDirection);
                    break;
            }
        }
        public void FinishAttack(AttackType attackType)
        {
            switch (attackType)
            {
                case AttackType.SimpleAttack:
                    _controllerData.SimpleDamageDealerComponent.FinishAttack();
                    break;
                case AttackType.DashAttack:
                    _controllerData.DashDamageDealerComponent.FinishAttack();
                    break;
            }
            
        }
        public void StartAttackCooldown(AttackType attackType)
        {
            switch (attackType)
            {
                case AttackType.SimpleAttack:
                    _controllerData.SimpleAttackCooldown.Cooldown(_controllerData.SimpleDamageDealerComponent.LastAttackData.CooldownTime);

                    break;
                case AttackType.DashAttack:
                    _controllerData.SimpleAttackCooldown.Cooldown(_controllerData.SimpleDamageDealerComponent.LastAttackData.CooldownTime);
                    break;
            }
        }
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