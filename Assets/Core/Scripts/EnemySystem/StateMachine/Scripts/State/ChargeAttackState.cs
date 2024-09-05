using Data;
using EnemySystem;

namespace BehaviourSystem.EnemySystem
{
    public class ChargeAttackState : State<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor>
    {
        public override void EnterState(StateMachine<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> stateMachine)
        {
            base.EnterState(stateMachine);
            stateMachine.Context.Agent.isStopped = true;
        }
        public override void UpdateState(StateMachine<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> stateMachine)
        {
            PerformCharge(stateMachine);
        }
        private void PerformCharge(StateMachine<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> stateMachine)
        {
            stateMachine.Context.PlayAnimation(EnemyRequestedAnimation.Charge);
            _isComplete = stateMachine.Context.AnimationComplete(stateMachine.Context.AnimationName(EnemyRequestedAnimation.Charge));
        }

        public override EnemyState GetNextState(StateMachine<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> stateMachine)
        {
            if (!_isComplete) return EnemyState.ChargeAttack;
            return EnemyState.Attack;
        }

        public override EnemySubState GetNextSubState(StateMachine<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> stateMachine)
        {
            return EnemySubState.Empty;
        }
    }
}