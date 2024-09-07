using Data;
using EnemySystem;
using UnityEngine;

namespace BehaviourSystem.EnemySystem
{
    public class IdleState : State<EnemyState, EnemySubState, EnemyControllerDataAccessor>
    {
        private float _elapsedTime = 0f;

        public override void EnterState(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            base.EnterState(stateMachine);

            _elapsedTime = 0f;
            stateMachine.Context.Agent.isStopped = true;
        }
        public override void FixedUpdateState(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            stateMachine.Context.PlayAnimation(EnemyRequestedAnimation.Idle);

            if (stateMachine.Context.IsStatic) return;
            _elapsedTime += Time.deltaTime;
            _isComplete = _elapsedTime >= stateMachine.Context.WaitTillRoam;
        }

        public override EnemyState GetNextState(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            bool hasTarget = stateMachine.Context.HasTarget;
            if (hasTarget)
            {
                bool runAway = stateMachine.StateMachineData.EnRunAwayState && stateMachine.Context.IsInCriticalCondition;
                if (runAway) return EnemyState.RunAway;

                bool canAttack = stateMachine.Context.CanAttack;
                bool charge = stateMachine.StateMachineData.EnChargeAttackState;
                if (canAttack) return charge ? EnemyState.ChargeAttack : EnemyState.Attack;

                return EnemyState.MoveToTarget;
            }

            if (_isComplete)
            {
                return stateMachine.Context.IsStatic ? EnemyState.Idle : EnemyState.Roaming;
            }
            return EnemyState.Idle;
        }

        public override EnemySubState GetNextSubState(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            return EnemySubState.Empty;
        }
    }
}