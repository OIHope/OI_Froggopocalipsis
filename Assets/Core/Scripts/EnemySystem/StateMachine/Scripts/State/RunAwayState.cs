using EnemySystem;
using UnityEngine;

namespace BehaviourSystem.EnemySystem
{
    public class RunAwayState : State<EnemyState, EnemySubState, EnemyControllerDataAccessor>
    {
        private IAttackableTarget _target;
        private float _runAwayDistance = 15f;

        public override void EnterState(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            base.EnterState(stateMachine);
            _target = stateMachine.Context.Target;
            stateMachine.Context.Agent.isStopped = false;
        }
        public override void UpdateState(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            stateMachine.Context.PlayAnimation(Data.EnemyRequestedAnimation.Run);
            MoveToSpot(stateMachine);
        }
        private void MoveToSpot(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            Vector3 targetLocation = _target.InstanceTransform.position;
            Vector3 direction = (stateMachine.Context.Agent.transform.position - targetLocation).normalized;
            stateMachine.Context.AimDirection = direction;

            Vector3 targetPos = targetLocation - (-stateMachine.Context.AimDirection * _runAwayDistance);
            stateMachine.Context.Agent.SetDestination(targetPos);
            stateMachine.Context.Agent.speed = stateMachine.Context.RunSpeed;

        }
        public override EnemyState GetNextState(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            bool hasTarget = stateMachine.Context.HasTarget;
            if (hasTarget)
            {
                bool inRange = Vector3.Distance(_target.InstanceTransform.position, stateMachine.Context.Agent.transform.position) < _runAwayDistance;
                return inRange ? EnemyState.RunAway : EnemyState.Idle;
            }
            return EnemyState.Idle;
        }
        public override EnemySubState GetNextSubState(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            return EnemySubState.Empty; 
        }
    }
}