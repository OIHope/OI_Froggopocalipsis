using Components;
using Data;
using EnemySystem;
using PlayerSystem;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourSystem.PlayerSystem
{
    public class SimpleAttackState : State<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor>
    {
        private float _duration;
        private float _elapsedTime = 0f;
        private Vector3 _attackDirection;

        public override void EnterState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            base.EnterState(stateMachine);

            _duration = stateMachine.Context.SimpleAttackDuration;
            _elapsedTime = 0f;
            _attackDirection = new(stateMachine.Context.AimDirection.x,
                0f, stateMachine.Context.AimDirection.z);

            stateMachine.Context.PerformAttack(AttackType.SimpleAttack);
            stateMachine.Context.StartAttackCooldown(AttackType.SimpleAttack);

            PlayAnimation(stateMachine);
        }

        public override void ExitState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            base.ExitState(stateMachine);
            stateMachine.Context.FinishAttack(AttackType.SimpleAttack);
        }

        public override void FixedUpdateState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            if (_elapsedTime > _duration && stateMachine.Context.AnimationComplete)
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
            Vector3 moveVector = stateMachine.Context.SimpleAttackAnimationCurve.Evaluate(_elapsedTime)
                * stateMachine.Context.SimpleAttackSlideDistance * Time.deltaTime
                * _attackDirection;
            stateMachine.Context.Move(moveVector);
            _elapsedTime += Time.deltaTime;
        }

        private void PlayAnimation(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            float randomAttackAnimationValue = Random.Range(-1f, 1f);
            if (randomAttackAnimationValue >= 0)
            {
                stateMachine.Context.PlayAnimation(PlayerRequestedAnimation.Attack01);
            }
            else
            {
                stateMachine.Context.PlayAnimation(PlayerRequestedAnimation.Attack02);
            }
        }

        public override PlayerStates GetNextState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            if (!_isComplete) return PlayerStates.Attack;
            return stateMachine.Context.IsMoving ? PlayerStates.Move : PlayerStates.Idle;
        }
        public override PlayerSubStates GetNextSubState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            return PlayerSubStates.NoAim;
        }
    }
}