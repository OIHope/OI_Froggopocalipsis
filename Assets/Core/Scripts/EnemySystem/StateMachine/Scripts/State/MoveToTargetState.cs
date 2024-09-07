using Data;
using EnemySystem;
using UnityEngine;

namespace BehaviourSystem.EnemySystem
{
    public class MoveToTargetState : State<EnemyState, EnemySubState, EnemyControllerDataAccessor>
    {
        private IAttackableTarget _target;
        private float _reachRange;


        public override void EnterState(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            base.EnterState(stateMachine);

            _target = stateMachine.Context.Target;
            _reachRange = (stateMachine.Context.EnemyData.MaxAttackRange + stateMachine.Context.EnemyData.MinAttackRange) / 2;
        }
        public override void ExitState(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            base.ExitState(stateMachine);

            if (_target.TargetIsAlive)
            {
                Vector3 direction = (_target.InstanceTransform.position - stateMachine.Context.Agent.transform.position).normalized;
                stateMachine.Context.AimDirection = direction;
            }
        }
        public override void FixedUpdateState(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            Vector3 direction = (_target.InstanceTransform.position - stateMachine.Context.Agent.transform.position).normalized;
            stateMachine.Context.AimDirection = direction;

            bool inAttackRange = stateMachine.Context.TargetInRange;

            if (inAttackRange)
            {
                stateMachine.Context.Agent.isStopped = true;
                stateMachine.Context.PlayAnimation(EnemyRequestedAnimation.Idle);
            }
            else
            {
                stateMachine.Context.Agent.isStopped = false;
                stateMachine.Context.PlayAnimation(EnemyRequestedAnimation.Run);
                MoveToSpot(stateMachine);
            }
        }

        private void MoveToSpot(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            Vector3 targetPos = _target.InstanceTransform.position - (stateMachine.Context.AimDirection * _reachRange);
            stateMachine.Context.Agent.SetDestination(targetPos);
            stateMachine.Context.Agent.speed = stateMachine.Context.RunSpeed;
        }

        public override EnemyState GetNextState(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            bool hasTarget = stateMachine.Context.HasTarget;
            if (hasTarget)
            {
                bool canAttack = stateMachine.Context.CanAttack;
                bool charge = stateMachine.StateMachineData.EnChargeAttackState;
                if (canAttack) return charge ? EnemyState.ChargeAttack : EnemyState.Attack;
                return EnemyState.MoveToTarget;
            }
            return EnemyState.Idle;
        }

        public override EnemySubState GetNextSubState(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            return EnemySubState.Empty;
        }
    }
}