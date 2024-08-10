using PlayerSystem;
using UnityEngine;

namespace BehaviourSystem
{
    [CreateAssetMenu(fileName = "TakeDamage SubState", menuName = ("State Machine/Player/SubState/TakeDamage SubState"))]
    public class TakeDamageSubState : SubStateSO<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor>
    {
        public override void EnterSubState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            throw new System.NotImplementedException();
        }

        public override void ExitSubState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            throw new System.NotImplementedException();
        }

        public override void FixedUpdateSubState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateSubState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            throw new System.NotImplementedException();
        }
    }
}