using Data;
using EnemySystem;
using UnityEngine;

namespace BehaviourSystem.EnemySystem
{
    public class ChargeAttackState : State<EnemyState, EnemySubState, EnemyControllerDataAccessor>
    {
        IAttackableTarget _target;
        public override void EnterState(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            base.EnterState(stateMachine);
            stateMachine.Context.Agent.isStopped = true;
            _target = stateMachine.Context.Target;
            stateMachine.Context.PerformCharge();
        }
        public override void ExitState(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            base.ExitState(stateMachine);
            if (stateMachine.StateMachineData.DisplayAttackDirection) stateMachine.Context.DisplayAim(false);
        }
        public override void UpdateState(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            PerformCharge(stateMachine);
        }
        private void PerformCharge(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            stateMachine.Context.PlayAnimation(EnemyRequestedAnimation.Charge);
            _isComplete = stateMachine.Context.AnimationComplete(stateMachine.Context.AnimationName(EnemyRequestedAnimation.Charge));
            if (stateMachine.StateMachineData.EnRangeChargeState) AimAtTarget(stateMachine);
            if (stateMachine.StateMachineData.DisplayAttackDirection) stateMachine.Context.DisplayAim(true);
        }
        private void AimAtTarget(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            Vector3 direction = (_target.InstanceTransform.position - stateMachine.Context.Agent.transform.position).normalized;
            stateMachine.Context.AimDirection = direction;
        }

        public override EnemyState GetNextState(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            bool hasTarget = stateMachine.Context.HasTarget;
            if (!hasTarget) return EnemyState.Idle;

            return _isComplete ? EnemyState.Attack : EnemyState.ChargeAttack;
        }

        public override EnemySubState GetNextSubState(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            return EnemySubState.Empty;
        }
    }
}