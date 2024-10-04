using UnityEngine;
using Components;
using Data;
using Entity.PlayerSystem;
using BehaviourSystem;

namespace PlayerSystem
{
    public enum PlayerRequestedAnimation
    { Idle, Walk, Run, Attack01, Attack02, Dodge, Die}
    public class PlayerControllerDataAccessor
    {
        private PlayerController _controllerData;
        private StateMachineDataSO _stateMachineData;

        public StateMachineDataSO StateMachineData => _stateMachineData;
        public GameObject NavigationArrow { get => _controllerData.NavigationArrow; set => _controllerData.NavigationArrow = value; }

        public InputControls Input => _controllerData.InputManager.Input;
        private AnimationComponent Animation => _controllerData.Animation;

        public bool UsingGamepad => _controllerData.InputManager.IsUsingGamepad;
        public bool IsMoving => Input.MainInputMap.Move.ReadValue<Vector2>() != Vector2.zero;
        public bool IsAiming
        {
            get
            {
                if (UsingGamepad)
                {
                    return Input.MainInputMap.LookAround.ReadValue<Vector2>() != Vector2.zero;
                }
                return true;
            }
        }
        public bool PressedInteractInput => Input.MainInputMap.Interact.WasReleasedThisFrame();
        public bool PressedInteractAltInput => Input.MainInputMap.AltInteract.WasReleasedThisFrame();

        public bool PressedDashInput => Input.MainInputMap.Dash.IsPressed();
        public bool PressedAttackInput => Input.MainInputMap.Attack.IsPressed();
        public bool CanAttack
        {
            get
            {
                bool isReloaded = _controllerData.SimpleAttackCooldown.CanUseAbility;
                bool keyIsUp = Input.MainInputMap.Attack.WasPressedThisFrame();
                return isReloaded && keyIsUp;
            }
        }
        public bool CanDash => _controllerData.DashCooldown.CanUseAbility;
        public bool AnimationComplete(string animationName)
        {
            return Animation.IsAnimationComplete(animationName);
        }

        public Vector2 MoveInput => Input.MainInputMap.Move.ReadValue<Vector2>();
        public Vector2 AimInput => Input.MainInputMap.LookAround.ReadValue<Vector2>();

        public Vector3 AimDirection { get; set; }

        public Transform InstanceTransform => _controllerData.InstanceTransform;

        public float SimpleAttackSlideDistance => _controllerData.SimpleDamageDealerComponent.AttackAnimationData.SlideDistance;

        public float MoveSpeed => _controllerData.PlayerData.MovementData.RunSpeed;
        public float DashSpeed => _controllerData.PlayerData.DashData.DashSpeed;

        public float SimpleAttackDuration => _controllerData.SimpleDamageDealerComponent.AttackAnimationData.Duration;
        public float DashAnimationCurveDuration => _controllerData.PlayerData.DashData.Duration;

        public AnimationCurve SimpleAttackAnimationCurve => _controllerData.SimpleDamageDealerComponent.AttackAnimationData.DataAnimationCurve;
        public AnimationCurve DashAnimationCurve => _controllerData.PlayerData.DashData.DashAnimationCurve;

        public LayerMask GroundLayer => _controllerData.LayersData.GroundLayer;

        public void PlayAnimation(PlayerRequestedAnimation request)
        {
            if (AimDirection.x < 0)
            {
                _controllerData.Renderer.flipX = true;
            }
            else
            {
                _controllerData.Renderer.flipX = false;
            }
            Animation.PlayAnimation(AnimationName(request));
        }
        public string AnimationName(PlayerRequestedAnimation request)
        {
            if (AimDirection.z > 0)
            {
                switch (request)
                {
                    case PlayerRequestedAnimation.Idle:
                        return "anim_player_idle_back";
                    case PlayerRequestedAnimation.Walk:
                        return "anim_player_walk_back";
                    case PlayerRequestedAnimation.Run:
                        return "anim_player_run_back";
                    case PlayerRequestedAnimation.Attack01:
                        return "anim_player_attack_01_back";
                    case PlayerRequestedAnimation.Attack02:
                        return "anim_player_attack_02_back";
                    case PlayerRequestedAnimation.Dodge:
                        return "anim_player_dodge_back";
                    case PlayerRequestedAnimation.Die:
                        return "anim_player_die";
                }
            }
            else
            {
                switch (request)
                {
                    case PlayerRequestedAnimation.Idle:
                        return "anim_player_idle";
                    case PlayerRequestedAnimation.Walk:
                        return "anim_player_walk";
                    case PlayerRequestedAnimation.Run:
                        return "anim_player_run";
                    case PlayerRequestedAnimation.Attack01:
                        return "anim_player_attack_01";
                    case PlayerRequestedAnimation.Attack02:
                        return "anim_player_attack_02";
                    case PlayerRequestedAnimation.Dodge:
                        return "anim_player_dodge";
                    case PlayerRequestedAnimation.Die:
                        return "anim_player_die";
                }
            }

            return "anim_player_idle";
        }

        public void PerformAttack(AttackType attackType)
        {
            switch (attackType)
            {
                case AttackType.SimpleAttack:
                    _controllerData.SimpleDamageDealerComponent.PerformAttack(AimDirection);
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
        public void DontCollideWithEnemy() => _controllerData.ColliderSwitch.DontCollideWithLayers();
        public void CollideWithEnemy() => _controllerData.ColliderSwitch.CollideWithLayers();
        public void Move(Vector3 moveVector) => _controllerData.MovementComponent.Move(moveVector);
        

        public PlayerControllerDataAccessor(PlayerController basePlayerController)
        {
            _controllerData = basePlayerController;
            _stateMachineData = basePlayerController.StateMachineData;
            AimDirection = Vector3.forward;
        }
    }
}