using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourSystem
{
    [CreateAssetMenu(fileName = "Empty State", menuName = ("State Machine/State/Empty State"))]
    public class EmptyState : StateSO<PlayerStates, PlayerSubStates>
    {
        public override PlayerStates GetNextState(PlayerStateMachine stateMachine)
        {
            return PlayerStates.Empty;
        }

        public override PlayerSubStates GetNextSubState(PlayerStateMachine stateMachine)
        {
            return PlayerSubStates.Empty;
        }

    }
}