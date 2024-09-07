using BehaviourSystem;
using Components;
using Data;
using Entity.EnemySystem;
using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem
{
    public class EnemyControllerDataAccessor 
        : CreatureDataAccessor<EnemyController, EnemyRequestedAnimation>
    {
        private Vector3 _startPos;

        protected override AnimationComponent Animation => _controllerData.Animation;

        public override bool AnimationComplete(string animationName) => Animation.IsAnimationComplete(animationName);
        public override bool IsStatic => EnemyData.RoamingData.IsStatic;
        public override bool CanAttack => _controllerData.SimpleAttackCooldown.CanUseAbility && TargetInRange;
        public bool HasTarget => _target != null;
        public override bool TargetInRange
        {
            get
            {
                float distance = Vector3.Distance(Target.InstanceTransform.position, _controllerData.transform.position);
                bool inRange = distance < EnemyData.MaxAttackRange && distance > EnemyData.MinAttackRange;
                return inRange;
            }
        }
        public override bool IsInCriticalCondition => _controllerData.HealthComponent.CriticalCondition;

        public override int RoamingDistance => EnemyData.RoamingData.RoamingDistance;

        public override float WalkSpeed => EnemyData.MovementData.WalkSpeed;
        public override float RunSpeed => EnemyData.MovementData.RunSpeed;
        public override float StopDistance => EnemyData.RoamingData.StopDistance;
        public override float WaitTillRoam => EnemyData.RoamingData.WaitTillRoamTime;
        public override float CheckPointDistance => EnemyData.RoamingData.CheckPointDistance;

        public override float SightDistance => EnemyData.EnemyVisionData.SightDistance;
        public override float TimeToSpotTarget => EnemyData.EnemyVisionData.TimeToSpotTarget;

        public override float SimpleAttackDuration => _controllerData.SimpleDamageDealerComponent.AttackAnimationData.Duration;
        public override float SimpleAttackSlideDistance => _controllerData.SimpleDamageDealerComponent.AttackAnimationData.SlideDistance;

        public override Vector3 StartPosition => _startPos;
        public override Vector3 AimDirection { get; set; }

        public override AnimationCurve SimpleAttackAnimationCurve => _controllerData.SimpleDamageDealerComponent.AttackAnimationData.DataAnimationCurve;

        public override IAttackableTarget Target
        {
            get => _target;
            set => _target = value;
        }

        public override NavMeshAgent Agent { get => _controllerData.Agent; set => _controllerData.Agent = value; }
        public override StateMachineDataSO StateMachineData => _stateMachineData;
        public override EnemyDataSO EnemyData => _enemyDataSO;


        public override AttackDataSO PerformAttack(AttackType attackType)
        {
            return _controllerData.SimpleDamageDealerComponent.PerformAttack(AimDirection);
        }
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
            Animation.AnimatorComponent.SetFloat("directionZ", AimDirection.z);
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
        public void DisplayAim(bool value)
        {
            if (value)
            {
                _controllerData.AimArrow.SetActive(true);

                Quaternion aimArrowRotation = Quaternion.LookRotation(AimDirection);
                _controllerData.AimArrow.transform.rotation = Quaternion.Euler(90f, aimArrowRotation.eulerAngles.y, 0f);
            }
            else
            {
                _controllerData.AimArrow.SetActive(false);
            }
        }

        public EnemyControllerDataAccessor(EnemyController controllerData) : base(controllerData)
        {
            _controllerData = controllerData;
            _startPos = _controllerData.transform.position;
            _stateMachineData = controllerData.StateMachineData;
            _enemyDataSO = controllerData.EnemyData;
        }
    }
}