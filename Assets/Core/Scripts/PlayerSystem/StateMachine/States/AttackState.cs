using PlayerSystem;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourSystem
{
    [CreateAssetMenu(fileName = "Attack State", menuName = ("State Machine/State/Attack State"))]
    public class AttackState : StateSO<PlayerStates, PlayerSubStates>, IGravityAffected<PlayerStateMachine>
    {
        private float _duration;
        private float _elapsedTime = 0f;
        private Vector3 _attackDirection;

        [SerializeField] private List<AttackDataSO> attackDataList;

        public override void EnterState(PlayerStateMachine stateMachine)
        {
            base.EnterState(stateMachine);

            _duration = stateMachine.Context.AtttackAnimCurveDuration;
            _elapsedTime = 0f;
            _attackDirection = new(stateMachine.Context.AimDirection.x,
                0f, stateMachine.Context.AimDirection.z);

            AttackDataSO attackDataSO = attackDataList[Random.Range(0, attackDataList.Count)];
            stateMachine.Context.PerformAttack(attackDataSO, stateMachine.Context.AimDirection);
        }

        public override void ExitState(PlayerStateMachine stateMachine)
        {
            base.ExitState(stateMachine);
            stateMachine.Context.FinishAttack();
            stateMachine.Context.StartAttackCooldown();
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
                PerformAttack(stateMachine);
            }
            
        }
        private void PerformAttack(PlayerStateMachine stateMachine)
        {
            Vector3 moveVector = stateMachine.Context.AttackAnimationCurve.Evaluate(_elapsedTime)
                * stateMachine.Context.AttackSlideDistance * Time.deltaTime
                * _attackDirection;
            stateMachine.Context.Move(moveVector);
            _elapsedTime += Time.deltaTime;
        }



        public override PlayerStates GetNextState(PlayerStateMachine stateMachine)
        {
            if (!_isComplete) return PlayerStates.Attack;
            return stateMachine.Context.IsMoving ? PlayerStates.Move : PlayerStates.Idle;
        }
        public override PlayerSubStates GetNextSubState(PlayerStateMachine stateMachine)
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

        public void HandleGravity(PlayerStateMachine stateMachine)
        {
            stateMachine.Context.HandleGravity(true);
        }
    }
}