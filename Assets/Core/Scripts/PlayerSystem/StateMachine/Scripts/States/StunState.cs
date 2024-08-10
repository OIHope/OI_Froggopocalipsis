using PlayerSystem;
using UnityEngine;

namespace BehaviourSystem
{
    [CreateAssetMenu(fileName = "Stun State", menuName = ("State Machine/Player/State/Stun State"))]
    public class StunState : StateSO<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor>, IGravityAffected<StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor>>
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
            HandleGravity(stateMachine);
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
            stateMachine.Context.Controller.Move(
                stateMachine.Context.DashAnimationCurve.Evaluate(_elapsedTime)
                * stateMachine.Context.DashSpeed * Time.deltaTime
                * stateMachine.Context.AimDirection);
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

        public void HandleGravity(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            stateMachine.Context.HandleGravity(false);
        }
    }
}