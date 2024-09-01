using EnemySystem;
using UnityEngine;

namespace BehaviourSystem.EnemySystem
{
    public class MoveToTargetState : State<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor>
    {
        private Transform _targetTransform;

        public override void EnterState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            base.EnterState(stateMachine);

            _targetTransform = stateMachine.Context.TargetTransform;
            stateMachine.Context.Agent.isStopped = false;
        }
        public override void ExitState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            base.ExitState(stateMachine);

            bool targetIsAlive = stateMachine.Context.TargetTransform != null;
            if (targetIsAlive)
            {
                Vector3 direction = (_targetTransform.position - stateMachine.Context.Agent.transform.position).normalized;
                stateMachine.Context.AimDirection = direction;
            }
        }
        public override void FixedUpdateState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            Vector3 moveToPos = _targetTransform.position - (stateMachine.Context.AimDirection * stateMachine.Context.StopDistance * 0.75f);
            stateMachine.Context.Agent.SetDestination(moveToPos);
            stateMachine.Context.Agent.speed = stateMachine.Context.RunSpeed;

            Vector3 direction = (_targetTransform.position - stateMachine.Context.Agent.transform.position).normalized;
            stateMachine.Context.AimDirection = direction;

            stateMachine.Context.PlayAnimation(EnemyRequestedAnimation.Run);
        }
        public override EnemyState GetNextState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
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

        public override EnemySubState GetNextSubState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            return EnemySubState.Empty;
        }
    }
}