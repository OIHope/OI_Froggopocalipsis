using UnityEngine;

namespace BehaviourSystem
{
    [CreateAssetMenu(fileName = "Dash State", menuName = ("State Machine/State/Dash State"))]
    public class DashState : StateSO<PlayerStates, PlayerSubStates>
    {
        private float _duration;
        private float _elapsedTime = 0f;
        private Vector3 _dashDirection;

        public override void EnterState(PlayerStateManager stateMachine)
        {
            base.EnterState(stateMachine);

            _duration = stateMachine.Context.DashAnimCurveDuration;
            _elapsedTime = 0f;
            _dashDirection = new(stateMachine.Context.AimDirection.x, 
                0f, stateMachine.Context.AimDirection.z);
        }
        public override void ExitState(PlayerStateManager stateMachine)
        {
            base.ExitState(stateMachine);
            stateMachine.Context.StartDashCooldown();
        }
        public override void FixedUpdateState(PlayerStateManager stateMachine)
        {
            if (_elapsedTime > _duration)
            {
                _isComplete = true;
            }
            else
            {
                PerformDash(stateMachine);
            }
        }
        private void PerformDash(PlayerStateManager stateMachine)
        {
            stateMachine.Context.Controller.Move(
                stateMachine.Context.DashAnimationCurve.Evaluate(_elapsedTime)
                * stateMachine.Context.DashSpeed * Time.deltaTime 
                * _dashDirection);
            _elapsedTime += Time.deltaTime;
        }

        public override PlayerStates GetNextState(PlayerStateManager stateMachine)
        {
            if (!_isComplete) return PlayerStates.Dash;
            return stateMachine.Context.IsMoving ? PlayerStates.Move : PlayerStates.Idle;
        }

        public override PlayerSubStates GetNextSubState(PlayerStateManager stateMachine)
        {
            return stateMachine.SubStateKey;
        }
    }
}