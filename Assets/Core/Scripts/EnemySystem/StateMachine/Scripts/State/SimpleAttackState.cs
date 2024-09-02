using Components;
using Data;
using EnemySystem;
using System;
using UnityEngine;

namespace BehaviourSystem.EnemySystem
{
    public class SimpleAttackState : State<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor>
    {
        private bool _isCharged = false;
        private bool _isAttacked = false;

        public override void EnterState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            base.EnterState(stateMachine);

            _isCharged = false;
            _isAttacked = false;

            stateMachine.Context.Agent.isStopped = true;
        }
        public override void ExitState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            base.ExitState(stateMachine);
            if (_isCharged)
            {
                stateMachine.Context.FinishAttack(AttackType.SimpleAttack);
                stateMachine.Context.StartAttackCooldown();
            }
        }
        public override void FixedUpdateState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {

            if (!_isCharged && !_isAttacked)
            {
                PerformCharge(stateMachine);
                return;
            }
            if (_isCharged && !_isAttacked)
            {
                PerformAttack(stateMachine);
                return;
            }
            if (_isCharged && _isAttacked)
            {
                _isComplete = true;
            }
        }
        private void PerformCharge(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            stateMachine.Context.PlayAnimation(EnemyRequestedAnimation.Charge);
            _isCharged = stateMachine.Context.AnimationComplete(stateMachine.Context.AnimationName(EnemyRequestedAnimation.Charge));
        }
        private void PerformAttack(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            stateMachine.Context.PlayAnimation(EnemyRequestedAnimation.Attack);
            _isAttacked = stateMachine.Context.AnimationComplete(stateMachine.Context.AnimationName(EnemyRequestedAnimation.Attack));
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