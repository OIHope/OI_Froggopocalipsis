using Components;
using Data;
using EnemySystem;
using System;
using UnityEngine;

namespace BehaviourSystem.EnemySystem
{
    public class SimpleAttackState : State<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor>
    {
        private float _chargeDuration;
        private float _elapsedChargeTime = 0f;
        private bool _isCharged = false;

        private float _attackDuration;
        private float _elapsedAttackTime = 0f;

        private Vector3 _attackDirection;

        private AttackDataSO _currentAttackData;

        public override void EnterState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            base.EnterState(stateMachine);
            stateMachine.Context.PlayAnimation(EnemyRequestedAnimation.ChargeSimpleAttack);

            stateMachine.Context.Agent.isStopped = true;

            _attackDuration = stateMachine.Context.SimpleAttackDuration;
            _elapsedAttackTime = 0f;
            _attackDirection = stateMachine.Context.AimDirection;

            _currentAttackData = stateMachine.Context.PerformAttack(AttackType.SimpleAttack);
            stateMachine.Context.StartAttackCooldown();

            _chargeDuration = _currentAttackData.ChargeTime;
            _elapsedChargeTime = 0f;
            _isCharged = false;
        }
        public override void ExitState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            base.ExitState(stateMachine);
            if ( _isCharged ) stateMachine.Context.FinishAttack(AttackType.SimpleAttack);
        }
        public override void FixedUpdateState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            if (_elapsedAttackTime > _attackDuration)
            {
                _isComplete = true;
                return;
            }
            if (!_isCharged)
            {
                PerformCharge(stateMachine);
                _isCharged = _elapsedChargeTime > _chargeDuration;
            }
            else
            {
                PerformAttack(stateMachine);
            }
        }
        private void PerformCharge(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            _elapsedChargeTime += Time.deltaTime;
        }
        private void PerformAttack(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            stateMachine.Context.PlayAnimation(EnemyRequestedAnimation.PerformSimpleAttack);

            Vector3 moveVector = stateMachine.Context.SimpleAttackAnimationCurve.Evaluate(_elapsedAttackTime)
                * stateMachine.Context.SimpleAttackSlideDistance * Time.deltaTime
                * _attackDirection;
            stateMachine.Context.Move(moveVector);
            _elapsedAttackTime += Time.deltaTime;
        }

        public override EnemyState GetNextState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            if (!_isComplete) return EnemyState.Attack;

            bool targetIsAlive = stateMachine.Context.TargetTransform != null;
            if (!targetIsAlive)
            {
                stateMachine.Context.EnableTargetDetector();
                return EnemyState.Idle;
            }
            return EnemyState.MoveToTarget;
        }

        public override EnemySubState GetNextSubState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            return EnemySubState.Empty;
        }
    }
}