using PlayerSystem;
using UnityEngine;

namespace BehaviourSystem.PlayerSystem
{
    public class DashState : State<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor>
    {
        private float _duration;
        private float _elapsedTime = 0f;
        private Vector3 _dashDirection;

        public override void EnterState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            base.EnterState(stateMachine);

            _duration = stateMachine.Context.DashAnimationCurveDuration;
            _elapsedTime = 0f;
            _dashDirection = new(stateMachine.Context.AimDirection.x, 
                0f, stateMachine.Context.AimDirection.z);

            stateMachine.Context.DontCollideWithEnemy();
            stateMachine.Context.StartDashCooldown();
        }
        public override void ExitState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            base.ExitState(stateMachine);
            stateMachine.Context.CollideWithEnemy();
        }
        public override void UpdateState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            _isComplete = stateMachine.Context.AnimationComplete(stateMachine.Context.AnimationName(PlayerRequestedAnimation.Dash));
            PerformDash(stateMachine);
        }
        private void PerformDash(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            stateMachine.Context.PlayAnimation(PlayerRequestedAnimation.Dash);

            Vector3 moveVector = stateMachine.Context.DashAnimationCurve.Evaluate(_elapsedTime)
                * stateMachine.Context.DashSpeed * Time.deltaTime * _dashDirection;
            stateMachine.Context.Move(moveVector);
            _elapsedTime += Time.deltaTime;
        }

        public override PlayerStates GetNextState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            if (!_isComplete) return PlayerStates.Dash;
            return PlayerStates.Idle;
        }

        public override PlayerSubStates GetNextSubState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            return PlayerSubStates.FixedAim;
        }
    }
}