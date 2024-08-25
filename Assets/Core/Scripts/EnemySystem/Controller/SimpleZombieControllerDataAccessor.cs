using Components;
using Data;
using Entity.EnemySystem;
using PlayerSystem;
using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem
{
    public enum EnemyRequestedAnimation 
        { Idle, Walk, Run, ChargeSimpleAttack, PerformSimpleAttack, TakeDamage }
    public class SimpleZombieControllerDataAccessor
    {
        private SimpleZombieController _controllerData;
        private Vector3 _startPos;

        private AnimationComponent Animation => _controllerData.Animation;

        public bool IsStatic => _controllerData.EnemyData.RoamingData.IsStatic;
        public bool CanAttack => _controllerData.SimpleAttackCooldown.CanUseAbility;

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
            switch (request)
            {
                case EnemyRequestedAnimation.Idle:
                    Animation.PlayAnimation("anim_enemy_idle");
                    break;
                case EnemyRequestedAnimation.Walk:
                    Animation.PlayAnimation("anim_enemy_walk");
                    break;
                case EnemyRequestedAnimation.Run:
                    Animation.PlayAnimation("anim_enemy_run");
                    break;
                case EnemyRequestedAnimation.ChargeSimpleAttack:
                    Animation.PlayAnimation("anim_enemy_chargeSimpleAttack");
                    break;
                case EnemyRequestedAnimation.PerformSimpleAttack:
                    Animation.PlayAnimation("anim_enemy_simpleAttack");
                    break;
                case EnemyRequestedAnimation.TakeDamage:
                    Animation.PlayAnimation("anim_enemy_run");
                    break;
            }
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