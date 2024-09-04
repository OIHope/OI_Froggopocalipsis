using Data;
using EnemySystem;
using UnityEngine;

namespace BehaviourSystem.EnemySystem
{
    public class MoveToTargetState : State<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor>
    {
        private Transform _targetTransform;

        public override void EnterState(StateMachine<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> stateMachine)
        {
            base.EnterState(stateMachine);

            _targetTransform = stateMachine.Context.TargetTransform;
        }
        public override void ExitState(StateMachine<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> stateMachine)
        {
            base.ExitState(stateMachine);

            bool targetIsAlive = stateMachine.Context.TargetTransform != null;
            if (targetIsAlive)
            {
                Vector3 direction = (_targetTransform.position - stateMachine.Context.Agent.transform.position).normalized;
                stateMachine.Context.AimDirection = direction;
            }
        }
        public override void FixedUpdateState(StateMachine<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> stateMachine)
        {
            Vector3 direction = (_targetTransform.position - stateMachine.Context.Agent.transform.position).normalized;
            stateMachine.Context.AimDirection = direction;
            
            float distance = Vector3.Distance(_targetTransform.position, stateMachine.Context.Agent.transform.position);
            bool inAttackRange = distance <= 2f;

            if (inAttackRange)
            {
                stateMachine.Context.Agent.isStopped = true;
                stateMachine.Context.PlayAnimation(EnemyRequestedAnimation.Idle);
            }
            else
            {
                stateMachine.Context.Agent.isStopped = false;
                stateMachine.Context.PlayAnimation(EnemyRequestedAnimation.Run);

                Vector3 moveToPos = _targetTransform.position - (stateMachine.Context.AimDirection * stateMachine.Context.StopDistance * 0.75f);
                stateMachine.Context.Agent.SetDestination(moveToPos);
                stateMachine.Context.Agent.speed = stateMachine.Context.RunSpeed;
            }
        }
        public override EnemyState GetNextState(StateMachine<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> stateMachine)
        {
            bool targetIsAlive = stateMachine.Context.TargetTransform != null;
            if (!targetIsAlive)
            {
                stateMachine.Context.EnableTargetDetector();
                return EnemyState.Idle;
            }

            float distance = Vector3.Distance(_targetTransform.position, stateMachine.Context.Agent.transform.position);
            bool inAttackRange = distance <= 2f;
            bool canAttack = stateMachine.Context.CanAttack;
            if (inAttackRange && canAttack) return EnemyState.Attack;

            return EnemyState.MoveToTarget;
        }

        public override EnemySubState GetNextSubState(StateMachine<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> stateMachine)
        {
            return EnemySubState.Empty;
        }
    }
}