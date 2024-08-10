using EnemySystem;
using UnityEngine;

namespace BehaviourSystem.EnemySystem
{
    [CreateAssetMenu(fileName = "Attack State", menuName = ("State Machine/Enemy/State/Attack State"))]
    public class AttackStateSO : StateSO<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor>
    {
        public override EnemyState GetNextState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            throw new System.NotImplementedException();
        }

        public override EnemySubState GetNextSubState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            throw new System.NotImplementedException();
        }
    }
}