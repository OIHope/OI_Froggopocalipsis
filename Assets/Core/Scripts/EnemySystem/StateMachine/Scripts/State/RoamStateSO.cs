using EnemySystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviourSystem.EnemySystem
{
    [CreateAssetMenu(fileName = "Roaming State", menuName = ("State Machine/Enemy/State/Roaming State"))]
    public class RoamStateSO : StateSO<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor>
    {
        [SerializeField] private int _roamingDistance;
        [SerializeField] private float _checkPointDistance;
        [SerializeField] private float _stopDistance;

        private NavMeshPath _roamingPath;
        private Vector3 _targetPos;

        public override void EnterState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            base.EnterState(stateMachine);
            _roamingPath = new();
            _targetPos = GetRandomPoint(stateMachine.Context.StartPosition);
            stateMachine.Context.Agent.SetDestination(_targetPos);
            stateMachine.Context.Agent.speed = stateMachine.Context.WalkSpeed;
            stateMachine.Context.Agent.stoppingDistance = _stopDistance;
        }
        private Vector3 GetRandomDirection()
        {
            float randomX = Random.value;
            float randomZ = Random.value;
            return new Vector3(randomX, 0f, randomZ);
        }
        private Vector3 GetRandomPoint(Vector3 startPos)
        {
            Vector3 targetPoint = (GetRandomDirection() * _roamingDistance) + startPos;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(targetPoint, out hit, _checkPointDistance, NavMesh.AllAreas))
            {
                return hit.position;
            }
            else
            {
                return startPos;
            }
        }
        public override void UpdateState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            bool closeToTargetPoint = stateMachine.Context.Agent.remainingDistance < stateMachine.Context.Agent.stoppingDistance;
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