using PlayerSystem;
using UnityEngine;

namespace BehaviourSystem.PlayerSystem
{
    public class FixedAimSubState : SubState<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor>
    {
        private Vector3 _lastAimDirection;

        public override void EnterSubState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            base.EnterSubState(stateMachine);
            _lastAimDirection = stateMachine.Context.AimDirection;
        }
        public override void UpdateSubState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            stateMachine.Context.AimDirection = _lastAimDirection;
        }
    }
}