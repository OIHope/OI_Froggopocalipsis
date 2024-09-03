using EnemySystem;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviourSystem.EnemySystem
{
    public class RoamState : State<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor>
    {
        public override void EnterState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            base.EnterState(stateMachine);

            Vector3 targetPos = GetRandomPoint(stateMachine);
            stateMachine.Context.Agent.SetDestination(targetPos);

            stateMachine.Context.Agent.speed = stateMachine.Context.WalkSpeed;
            stateMachine.Context.Agent.isStopped = false;
        }
        private Vector3 GetRandomDirection()
        {
            float randomX = Random.Range(-1f,1f);
            float randomZ = Random.Range(-1f, 1f);
            return new Vector3(randomX, 0f, randomZ);
        }
        private Vector3 GetRandomPoint(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            Vector3 targetPoint = (GetRandomDirection() * stateMachine.Context.RoamingDistance) + stateMachine.Context.StartPosition;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(targetPoint, out hit, stateMachine.Context.CheckPointDistance, NavMesh.AllAreas))
            {
                return hit.position;
            }
            else
            {
                return stateMachine.Context.StartPosition;
            }
        }
        public override void ExitState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            base.ExitState(stateMachine);
        }
        public override void UpdateState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            stateMachine.Context.PlayAnimation(EnemyRequestedAnimation.Walk);

            bool closeToTargetPoint = stateMachine.Context.Agent.remainingDistance < 2f;
            bool pathIsBlocked = stateMachine.Context.Agent.pathPending;
            if (closeToTargetPoint && !pathIsBlocked)
            {
                _isComplete = true;
            }
        }
        public override EnemyState GetNextState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            return _isComplete ? EnemyState.Idle : EnemyState.Roaming;
        }

        public override EnemySubState GetNextSubState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            return EnemySubState.Empty;
        }
    }
}