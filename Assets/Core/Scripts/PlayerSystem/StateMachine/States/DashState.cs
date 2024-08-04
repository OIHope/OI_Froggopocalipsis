using UnityEngine;

namespace BehaviourSystem
{
    [CreateAssetMenu(fileName = "Dash State", menuName = ("State Machine/State/Dash State"))]
    public class DashState : StateSO<PlayerStates, PlayerSubStates>
    {
        private float _duration;
        private float _elapsedTime = 0f;
        private Vector3 _dashDirection;

        public override void EnterState(PlayerStateMachine stateMachine)
        {
            base.EnterState(stateMachine);

            _duration = stateMachine.Context.DashAnimCurveDuration;
            _elapsedTime = 0f;
            _dashDirection = new(stateMachine.Context.AimDirection.x, 
                0f, stateMachine.Context.AimDirection.z);

            stateMachine.Context.DontCollideWithEnemy();
        }
        public override void ExitState(PlayerStateMachine stateMachine)
        {
            base.ExitState(stateMachine);
            stateMachine.Context.StartDashCooldown();
            stateMachine.Context.CollideWithEnemy();
        }
        public override void FixedUpdateState(PlayerStateMachine stateMachine)
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
        private void PerformDash(PlayerStateMachine stateMachine)
        {
            Vector3 moveVector = stateMachine.Context.DashAnimationCurve.Evaluate(_elapsedTime)
                * stateMachine.Context.DashSpeed * Time.deltaTime * _dashDirection;
            stateMachine.Context.Move(moveVector);
            _elapsedTime += Time.deltaTime;
        }

        public override PlayerStates GetNextState(PlayerStateMachine stateMachine)
        {
            if (!_isComplete) return PlayerStates.Dash;
            return stateMachine.Context.IsMoving ? PlayerStates.Move : PlayerStates.Idle;
        }

        public override PlayerSubStates GetNextSubState(PlayerStateMachine stateMachine)
        {
            if (stateMachine.Context.PressedAttackInput) return PlayerSubStates.DashAttack;
            return stateMachine.SubStateKey;
        }
    }
}