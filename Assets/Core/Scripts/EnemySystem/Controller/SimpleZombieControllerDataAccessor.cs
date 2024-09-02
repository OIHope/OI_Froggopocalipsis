using Components;
using Data;
using Entity.EnemySystem;
using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem
{
    public enum EnemyRequestedAnimation 
        { Idle, Walk, Run, Charge, Attack, TakeDamage, Spawn, Die }
    public class SimpleZombieControllerDataAccessor
    {
        private SimpleZombieController _controllerData;
        private Vector3 _startPos;

        private AnimationComponent Animation => _controllerData.Animation;

        public bool IsStatic => _controllerData.EnemyData.RoamingData.IsStatic;
        public bool CanAttack => _controllerData.SimpleAttackCooldown.CanUseAbility;
        public bool AnimationComplete(string animationName)
        {
            return Animation.IsAnimationComplete(animationName);
        }

        public int HP => _controllerData.EnemyData.HealthData.StartHP;
        public int MaxHP => _controllerData.EnemyData.HealthData.MaxHP;

        public int Damage => _controllerData.EnemyData.AttackData.Damage;
        public int CritDamage => _controllerData.EnemyData.AttackData.CritDamage;
        public int CritChance => _controllerData.EnemyData.AttackData.CritChance;
        public int RoamingDistance => _controllerData.EnemyData.RoamingData.RoamingDistance;

        public float WalkSpeed => _controllerData.EnemyData.MovementData.WalkSpeed;
        public float RunSpeed => _controllerData.EnemyData.MovementData.RunSpeed;
        public float StopDistance => _controllerData.EnemyData.RoamingData.StopDistance;
        public float WaitTillRoam => _controllerData.EnemyData.RoamingData.WaitTillRoamTime;
        public float CheckPointDistance => _controllerData.EnemyData.RoamingData.CheckPointDistance;

        public float SightDistance => _controllerData.EnemyData.EnemyVisionData.SightDistance;
        public float TimeToSpotTarget => _controllerData.EnemyData.EnemyVisionData.TimeToSpotTarget;

        public float SimpleAttackDuration => _controllerData.SimpleDamageDealerComponent.AttackAnimationData.Duration;
        public float SimpleAttackSlideDistance => _controllerData.SimpleDamageDealerComponent.AttackAnimationData.SlideDistance;


        public Vector3 StartPosition => _startPos;
        public Vector3 AimDirection { get; set; }

        public AnimationCurve SimpleAttackAnimationCurve => _controllerData.SimpleDamageDealerComponent.AttackAnimationData.DataAnimationCurve;

        public Transform TargetTransform { get; set; }

        public NavMeshAgent Agent { get => _controllerData.Agent; set => _controllerData.Agent = value; }

        public void PlayAnimation(EnemyRequestedAnimation request)
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
        public string AnimationName(EnemyRequestedAnimation request)
        {
            if (AimDirection.z > 0)
            {
                switch (request)
                {
                    case EnemyRequestedAnimation.Idle:
                        return "anim_simpleZom_idle_back";
                    case EnemyRequestedAnimation.Walk:
                        return "anim_simpleZom_move_back";
                    case EnemyRequestedAnimation.Run:
                        return "anim_simpleZom_move_back";
                    case EnemyRequestedAnimation.Charge:
                        return "anim_simpleZom_chargeAttack_back";
                    case EnemyRequestedAnimation.Attack:
                        return "anim_simpleZom_attack_back";
                    case EnemyRequestedAnimation.TakeDamage:
                        return "anim_simpleZom_takeDamage_back";
                    case EnemyRequestedAnimation.Spawn:
                        return "anim_simpleZom_spawn_back";
                    case EnemyRequestedAnimation.Die:
                        return "anim_simpleZom_die_back";
                }
            }
            else
            {
                switch (request)
                {
                    case EnemyRequestedAnimation.Idle:
                        return "anim_simpleZom_idle";
                    case EnemyRequestedAnimation.Walk:
                        return "anim_simpleZom_move";
                    case EnemyRequestedAnimation.Run:
                        return "anim_simpleZom_move";
                    case EnemyRequestedAnimation.Charge:
                        return "anim_simpleZom_chargeAttack";
                    case EnemyRequestedAnimation.Attack:
                        return "anim_simpleZom_attack";
                    case EnemyRequestedAnimation.TakeDamage:
                        return "anim_simpleZom_takeDamage";
                    case EnemyRequestedAnimation.Spawn:
                        return "anim_simpleZom_spawn";
                    case EnemyRequestedAnimation.Die:
                        return "anim_simpleZom_die";
                }
            }
            return "anim_simpleZom_idle";
        }
        public AttackDataSO PerformAttack(AttackType attackType) => _controllerData.SimpleDamageDealerComponent.PerformAttack(AimDirection);
        public void FinishAttack(AttackType attackType) => _controllerData.SimpleDamageDealerComponent.FinishAttack();
        public void StartAttackCooldown() => _controllerData.SimpleAttackCooldown.Cooldown(_controllerData.SimpleDamageDealerComponent.LastAttackData.CooldownTime);
        public void Move(Vector3 moveVector) => _controllerData.MovementComponent.Move(moveVector);
        public void EnableTargetDetector() => _controllerData.TargetDetector.EnableTargetDetection();

        public SimpleZombieControllerDataAccessor(SimpleZombieController controllerData)
        {
            _controllerData = controllerData;
            _startPos = _controllerData.transform.position;
        }
    }
}