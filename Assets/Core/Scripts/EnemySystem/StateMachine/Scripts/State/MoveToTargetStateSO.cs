using EnemySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourSystem.EnemySystem
{
    [CreateAssetMenu(fileName = "MoveToTarget State", menuName = ("State Machine/Enemy/State/MoveToTarget State"))]
    public class MoveToTargetStateSO : StateSO<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor>
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