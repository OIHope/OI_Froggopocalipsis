using Components;
using Data;
using PlayerSystem;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourSystem.PlayerSystem
{
    public class AttackState : State<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor>
    {
        private float _duration;
        private float _elapsedTime = 0f;
        private Vector3 _attackDirection;

        public override void EnterState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            base.EnterState(stateMachine);

            _duration = stateMachine.Context.AtttackAnimCurveDuration;
            _elapsedTime = 0f;
            _attackDirection = new(stateMachine.Context.AimDirection.x,
                0f, stateMachine.Context.AimDirection.z);

            stateMachine.Context.PerformAttack(AttackType.SimpleAttack);
            stateMachine.Context.StartAttackCooldown();
        }

        public override void ExitState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            base.ExitState(stateMachine);
            stateMachine.Context.FinishAttack();
        }

        public override void FixedUpdateState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            if (_elapsedTime > _duration)
            {
                _isComplete = true;
            }
            else
            {
                PerformAttack(stateMachine);
            }
            
        }
        private void PerformAttack(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            Vector3 moveVector = stateMachine.Context.AttackAnimationCurve.Evaluate(_elapsedTime)
                * stateMachine.Context.AttackSlideDistance * Time.deltaTime
                * _attackDirection;
            stateMachine.Context.Move(moveVector);
            _elapsedTime += Time.deltaTime;
        }

        public override PlayerStates GetNextState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            if (!_isComplete) return PlayerStates.Attack;
            return stateMachine.Context.IsMoving ? PlayerStates.Move : PlayerStates.Idle;
        }
        public override PlayerSubStates GetNextSubState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            PlayerSubStates nextSubStateKey = stateMachine.GetSubStateKey;
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
    }
}