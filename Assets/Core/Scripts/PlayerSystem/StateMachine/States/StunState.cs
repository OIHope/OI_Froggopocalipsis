using UnityEngine;

namespace BehaviourSystem
{
    [CreateAssetMenu(fileName = "Stun State", menuName = ("State Machine/State/Stun State"))]
    public class StunState : StateSO<PlayerStates, PlayerSubStates>, IGravityAffected<PlayerStateMachine>
    {
        private float _duration;
        private float _elapsedTime = 0f;

        public override void EnterState(PlayerStateMachine stateMachine)
        {
            base.EnterState(stateMachine);
            _duration = stateMachine.Context.StunDuration;
            _elapsedTime = 0f;
        }
        public override void FixedUpdateState(PlayerStateMachine stateMachine)
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
        private void PerformStun(PlayerStateMachine stateMachine)
        {
            stateMachine.Context.Controller.Move(
                stateMachine.Context.DashAnimationCurve.Evaluate(_elapsedTime)
                * stateMachine.Context.DashSpeed * Time.deltaTime
                * stateMachine.Context.AimDirection);
            _elapsedTime += Time.deltaTime;
        }

        public override PlayerStates GetNextState(PlayerStateMachine stateMachine)
        {
            if (!_isComplete) return PlayerStates.Stun;
            return stateMachine.Context.IsMoving ? PlayerStates.Move : PlayerStates.Idle;
        }
        public override PlayerSubStates GetNextSubState(PlayerStateMachine stateMachine)
        {
            return PlayerSubStates.Empty;
        }

        public void HandleGravity(PlayerStateMachine stateMachine)
        {
            stateMachine.Context.HandleGravity(false);
        }
    }
}