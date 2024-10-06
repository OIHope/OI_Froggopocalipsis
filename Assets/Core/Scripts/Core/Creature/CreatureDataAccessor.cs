using BehaviourSystem;
using Components;
using EnemySystem;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Data
{
    public abstract class CreatureDataAccessor<ControllerType, AnimationRequest> 
        where ControllerType : Entity.Creature where AnimationRequest : Enum
    {
        protected ControllerType _controllerData;
        protected EnemyDataSO _enemyDataSO;
        protected StateMachineDataSO _stateMachineData;
        protected IAttackableTarget _target = null;

        protected abstract AnimationComponent Animation { get; }

        public abstract void PerformCharge();
        public abstract EnemyDataSO EnemyData { get; }
        public abstract StateMachineDataSO StateMachineData {  get; }
        public abstract bool AnimationComplete(string animationName);
        public abstract bool IsStatic { get; }
        public abstract bool CanAttack { get; }
        public abstract bool TargetInRange { get; }
        public abstract bool IsInCriticalCondition { get; }
        public abstract int RoamingDistance { get; }

        public abstract float WalkSpeed { get; }
        public abstract float RunSpeed { get; }
        public abstract float StopDistance { get; }
        public abstract float WaitTillRoam { get; }
        public abstract float CheckPointDistance { get; }

        public abstract float SightDistance { get; }
        public abstract float TimeToSpotTarget { get; }

        public abstract float SimpleAttackDuration { get; }
        public abstract float SimpleAttackSlideDistance { get; }

        public abstract Vector3 StartPosition { get; }
        public abstract Vector3 AimDirection { get; set; }

        public abstract AnimationCurve SimpleAttackAnimationCurve { get; }

        public abstract IAttackableTarget Target { get; set; }

        public abstract NavMeshAgent Agent { get; set; }

        public abstract void PlayAnimation(AnimationRequest request);
        public abstract string AnimationName(AnimationRequest request);

        public abstract AttackDataSO PerformAttack(AttackType attackType);
        public abstract void FinishAttack(AttackType attackType);
        public abstract void StartAttackCooldown();
        public abstract void Move(Vector3 moveVector);
        public abstract void EnableTargetDetector();
        public abstract void MakeInvincible(bool value);

        protected CreatureDataAccessor(ControllerType controllerData) { }
    }
}