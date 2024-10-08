using PlayerSystem;

namespace BehaviourSystem.PlayerSystem
{
    public class EmptyState : State<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor>
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