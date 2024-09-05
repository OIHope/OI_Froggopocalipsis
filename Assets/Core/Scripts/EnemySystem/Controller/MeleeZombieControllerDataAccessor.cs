using BehaviourSystem;
using Components;
using Data;
using Entity.EnemySystem;
using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem
{
    public class MeleeZombieControllerDataAccessor 
        : CreatureDataAccessor<MeleeZombieController, EnemyRequestedAnimation>
    {
        private Vector3 _startPos;

        protected override AnimationComponent Animation => _controllerData.Animation;

        public override bool AnimationComplete(string animationName) => Animation.IsAnimationComplete(animationName);
        public override bool IsStatic => _controllerData.EnemyData.RoamingData.IsStatic;
        public override bool CanAttack => _controllerData.SimpleAttackCooldown.CanUseAbility;

        public override int RoamingDistance => _controllerData.EnemyData.RoamingData.RoamingDistance;

        public override float WalkSpeed => _controllerData.EnemyData.MovementData.WalkSpeed;
        public override float RunSpeed => _controllerData.EnemyData.MovementData.RunSpeed;
        public override float StopDistance => _controllerData.EnemyData.RoamingData.StopDistance;
        public override float WaitTillRoam => _controllerData.EnemyData.RoamingData.WaitTillRoamTime;
        public override float CheckPointDistance => _controllerData.EnemyData.RoamingData.CheckPointDistance;

        public override float SightDistance => _controllerData.EnemyData.EnemyVisionData.SightDistance;
        public override float TimeToSpotTarget => _controllerData.EnemyData.EnemyVisionData.TimeToSpotTarget;

        public override float SimpleAttackDuration => _controllerData.SimpleDamageDealerComponent.AttackAnimationData.Duration;
        public override float SimpleAttackSlideDistance => _controllerData.SimpleDamageDealerComponent.AttackAnimationData.SlideDistance;

        public override Vector3 StartPosition => _startPos;
        public override Vector3 AimDirection { get; set; }

        public override AnimationCurve SimpleAttackAnimationCurve => _controllerData.SimpleDamageDealerComponent.AttackAnimationData.DataAnimationCurve;

        public override IAttackableTarget Target { get; set; }

        public override NavMeshAgent Agent { get => _controllerData.Agent; set => _controllerData.Agent = value; }

        public override StateMachineDataSO StateMachineData => _stateMachineData;

        public override AttackDataSO PerformAttack(AttackType attackType) => _controllerData.SimpleDamageDealerComponent.PerformAttack(AimDirection);
        public override void FinishAttack(AttackType attackType) => _controllerData.SimpleDamageDealerComponent.FinishAttack();
        public override void StartAttackCooldown() => _controllerData.SimpleAttackCooldown.Cooldown(_controllerData.SimpleDamageDealerComponent.LastAttackData.CooldownTime);
        public override void Move(Vector3 moveVector) => _controllerData.MovementComponent.Move(moveVector);
        public override void EnableTargetDetector() => _controllerData.TargetDetector.EnableTargetDetection();
        public override void MakeInvincible(bool value) => _controllerData.MakeInvincible(value);

        public override void PlayAnimation(EnemyRequestedAnimation request)
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

        public override string AnimationName(EnemyRequestedAnimation request)
        {
            if (AimDirection.z > 0)
            {
                switch (request)
                {
                    case EnemyRequestedAnimation.Idle:
                        return _controllerData.AnimationNameData.IdleBackAnimationName;
                    case EnemyRequestedAnimation.Walk:
                        return _controllerData.AnimationNameData.WalkBackAnimationName;
                    case EnemyRequestedAnimation.Run:
                        return _controllerData.AnimationNameData.RunBackAnimationName;
                    case EnemyRequestedAnimation.Charge:
                        return _controllerData.AnimationNameData.ChargeBackAnimationName;
                    case EnemyRequestedAnimation.Attack:
                        return _controllerData.AnimationNameData.AttackBackAnimationName;
                    case EnemyRequestedAnimation.TakeDamage:
                        return _controllerData.AnimationNameData.TakeDamageBackAnimationName;
                    case EnemyRequestedAnimation.Spawn:
                        return _controllerData.AnimationNameData.SpawnBackAnimationName;
                    case EnemyRequestedAnimation.Die:
                        return _controllerData.AnimationNameData.DieBackAnimationName;
                }
            }
            else
            {
                switch (request)
                {
                    case EnemyRequestedAnimation.Idle:
                        return _controllerData.AnimationNameData.IdleAnimationName;
                    case EnemyRequestedAnimation.Walk:
                        return _controllerData.AnimationNameData.WalkAnimationName;
                    case EnemyRequestedAnimation.Run:
                        return _controllerData.AnimationNameData.RunAnimationName;
                    case EnemyRequestedAnimation.Charge:
                        return _controllerData.AnimationNameData.ChargeAnimationName;
                    case EnemyRequestedAnimation.Attack:
                        return _controllerData.AnimationNameData.AttackAnimationName;
                    case EnemyRequestedAnimation.TakeDamage:
                        return _controllerData.AnimationNameData.TakeDamageAnimationName;
                    case EnemyRequestedAnimation.Spawn:
                        return _controllerData.AnimationNameData.SpawnAnimationName;
                    case EnemyRequestedAnimation.Die:
                        return _controllerData.AnimationNameData.DieAnimationName;
                }
            }
            return _controllerData.AnimationNameData.IdleAnimationName;
        }

        public MeleeZombieControllerDataAccessor(MeleeZombieController controllerData) : base(controllerData)
        {
            _controllerData = controllerData;
            _startPos = _controllerData.transform.position;
            _stateMachineData = controllerData.StateMachineData;
        }
    }
}