using PlayerSystem;
using UnityEngine;

namespace BehaviourSystem.PlayerSystem
{
    public class StunState : State<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor>
    {
        private float _duration;
        private float _elapsedTime = 0f;

        public override void EnterState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            base.EnterState(stateMachine);
            //_duration = stateMachine.Context.StunDuration;
            _elapsedTime = 0f;
        }
        public override void FixedUpdateState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            if (_elapsedTime > _duration)
            {
                _isComplete = true;
            }
            else
            {
                PerformStun(stateMachine);
            }
        }
        private void PerformStun(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            _elapsedTime += Time.deltaTime;
        }

        public override PlayerStates GetNextState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            if (!_isComplete) return PlayerStates.Stun;
            return stateMachine.Context.IsMoving ? PlayerStates.Move : PlayerStates.Idle;
        }
        public override PlayerSubStates GetNextSubState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            return PlayerSubStates.Empty;
        }
    }
}