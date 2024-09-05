using Data;
using EnemySystem;
using UnityEngine;

namespace BehaviourSystem.EnemySystem
{
    public class MoveToTargetState : State<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor>
    {
        private IAttackableTarget _target;
        private float _reachRange = 1.5f;

        public override void EnterState(StateMachine<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> stateMachine)
        {
            base.EnterState(stateMachine);

            _target = stateMachine.Context.Target;
        }
        public override void ExitState(StateMachine<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> stateMachine)
        {
            base.ExitState(stateMachine);

            if (_target.TargetIsAlive)
            {
                Vector3 direction = (_target.InstanceTransform.position - stateMachine.Context.Agent.transform.position).normalized;
                stateMachine.Context.AimDirection = direction;
            }
        }
        public override void FixedUpdateState(StateMachine<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> stateMachine)
        {
            Vector3 direction = (_target.InstanceTransform.position - stateMachine.Context.Agent.transform.position).normalized;
            stateMachine.Context.AimDirection = direction;
            
            float distance = Vector3.Distance(_target.InstanceTransform.position, stateMachine.Context.Agent.transform.position);
            bool inAttackRange = distance <= _reachRange;

            if (inAttackRange)
            {
                stateMachine.Context.Agent.isStopped = true;
                stateMachine.Context.PlayAnimation(EnemyRequestedAnimation.Idle);
            }
            else
            {
                stateMachine.Context.Agent.isStopped = false;
                stateMachine.Context.PlayAnimation(EnemyRequestedAnimation.Run);

                Vector3 moveToPos = _target.InstanceTransform.position - (stateMachine.Context.AimDirection * _reachRange * 0.75f);
                stateMachine.Context.Agent.SetDestination(moveToPos);
                stateMachine.Context.Agent.speed = stateMachine.Context.RunSpeed;
            }
        }
        public override EnemyState GetNextState(StateMachine<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> stateMachine)
        {
            if (!_target.TargetIsAlive || _target == null)
            {
                return EnemyState.Idle;
            }

            float distance = Vector3.Distance(_target.InstanceTransform.position, stateMachine.Context.Agent.transform.position);
            bool inAttackRange = distance <= _reachRange;
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