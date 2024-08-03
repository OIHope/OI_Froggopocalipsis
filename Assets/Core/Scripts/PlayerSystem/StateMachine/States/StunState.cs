using UnityEngine;

namespace BehaviourSystem
{
    [CreateAssetMenu(fileName = "Stun State", menuName = ("State Machine/State/Stun State"))]
    public class StunState : StateSO<PlayerStates, PlayerSubStates>, IGravityAffected<PlayerStateManager>
    {
        private float _duration;
        private float _elapsedTime = 0f;

        public override void EnterState(PlayerStateManager stateMachine)
        {
            base.EnterState(stateMachine);
            _duration = stateMachine.Context.StunDuration;
            _elapsedTime = 0f;
        }
        public override void FixedUpdateState(PlayerStateManager stateMachine)
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
        private void PerformStun(PlayerStateManager stateMachine)
        {
            stateMachine.Context.Controller.Move(
                stateMachine.Context.DashAnimationCurve.Evaluate(_elapsedTime)
                * stateMachine.Context.DashSpeed * Time.deltaTime
                * stateMachine.Context.AimDirection);
            _elapsedTime += Time.deltaTime;
        }

        public override PlayerStates GetNextState(PlayerStateManager stateMachine)
        {
            if (!_isComplete) return PlayerStates.Stun;
            return stateMachine.Context.IsMoving ? PlayerStates.Move : PlayerStates.Idle;
        }
        public override PlayerSubStates GetNextSubState(PlayerStateManager stateMachine)
        {
            return PlayerSubStates.Empty;
        }

        public void HandleGravity(PlayerStateManager stateMachine)
        {
            float gravity = -9.5f;
            float groundedGravity = -0.5f;
            float usedGravityValue = stateMachine.Context.Controller.isGrounded ? groundedGravity : gravity;
            stateMachine.Context.Controller.Move(Vector3.up * usedGravityValue * Time.deltaTime);
        }
    }
}