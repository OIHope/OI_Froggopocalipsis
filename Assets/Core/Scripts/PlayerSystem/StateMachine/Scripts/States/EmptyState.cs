using PlayerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourSystem
{
    [CreateAssetMenu(fileName = "Empty State", menuName = ("State Machine/Player/State/Empty State"))]
    public class EmptyState : StateSO<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor>
    {
        public override PlayerStates GetNextState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            return PlayerStates.Empty;
        }

        public override PlayerSubStates GetNextSubState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            return PlayerSubStates.Empty;
        }

    }
}