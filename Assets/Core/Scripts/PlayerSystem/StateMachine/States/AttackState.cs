using UnityEngine;

namespace BehaviourSystem
{
    [CreateAssetMenu(fileName = "Attack State", menuName = ("State Machine/State/Attack State"))]
    public class AttackState : StateSO<PlayerStates, PlayerSubStates>, IGravityAffected<PlayerStateManager>
    {
        private float _duration;
        private float _elapsedTime = 0f;
        private Vector3 _attackDirection;

        public override void EnterState(PlayerStateManager stateMachine)
        {
            Debug.Log("Entered attack state");
            base.EnterState(stateMachine);

            _duration = stateMachine.Context.AtttackAnimCurveDuration;
            _elapsedTime = 0f;
            _attackDirection = new(stateMachine.Context.AimDirection.x,
                0f, stateMachine.Context.AimDirection.z);

            stateMachine.Context.DamageDealerPivot.SetActive(true);
            RotateDamageDealerArrow(stateMachine);
        }

        private void RotateDamageDealerArrow(PlayerStateManager stateMachine)
        {
            Quaternion navigationArrowRotation = Quaternion.LookRotation(stateMachine.Context.AimDirection);
            stateMachine.Context.DamageDealerPivot.transform.rotation = Quaternion.Euler(0f, navigationArrowRotation.eulerAngles.y, 0f);
        }

        public override void ExitState(PlayerStateManager stateMachine)
        {
            base.ExitState(stateMachine);
            stateMachine.Context.DamageDealerPivot.SetActive(false);
            stateMachine.Context.StartAttackCooldown();
        }

        public override void FixedUpdateState(PlayerStateManager stateMachine)
        {
            Debug.Log("Performing attack state");
            HandleGravity(stateMachine);
            if (_elapsedTime > _duration)
            {
                _isComplete = true;
            }
            else
            {
                PerformAttack(stateMachine);
            }
            
        }
        private void PerformAttack(PlayerStateManager stateMachine)
        {
            stateMachine.Context.Controller.Move(
                stateMachine.Context.AttackAnimationCurve.Evaluate(_elapsedTime)
                * stateMachine.Context.AttackSlideDistance * Time.deltaTime
                * _attackDirection);
            _elapsedTime += Time.deltaTime;
        }



        public override PlayerStates GetNextState(PlayerStateManager stateMachine)
        {
            if (!_isComplete) return PlayerStates.Attack;
            return stateMachine.Context.IsMoving ? PlayerStates.Move : PlayerStates.Idle;
        }
        public override PlayerSubStates GetNextSubState(PlayerStateManager stateMachine)
        {
            PlayerSubStates nextSubStateKey = stateMachine.SubStateKey;
            if (stateMachine.Context.IsAiming)
            {
                nextSubStateKey = PlayerSubStates.Aim;
            }
            else
            {
                nextSubStateKey = PlayerSubStates.NoAim;
            }
            return nextSubStateKey;
        }

        public void HandleGravity(PlayerStateManager stateMachine)
        {
            float gravity = -0.5f;
            stateMachine.Context.Controller.Move(gravity * Time.deltaTime * Vector3.up);
        }
    }
}