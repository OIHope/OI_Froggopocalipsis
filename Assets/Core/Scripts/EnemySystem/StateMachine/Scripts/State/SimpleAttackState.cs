using Components;
using Data;
using EnemySystem;

namespace BehaviourSystem.EnemySystem
{
    public class SimpleAttackState : State<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor>
    {
        public override void EnterState(StateMachine<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> stateMachine)
        {
            base.EnterState(stateMachine);

            stateMachine.Context.Agent.isStopped = true;
            stateMachine.Context.PerformAttack(AttackType.SimpleAttack);
        }
        public override void ExitState(StateMachine<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> stateMachine)
        {
            base.ExitState(stateMachine);
            stateMachine.Context.FinishAttack(AttackType.SimpleAttack);
            stateMachine.Context.StartAttackCooldown();
        }
        public override void UpdateState(StateMachine<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> stateMachine)
        {
            PerformAttack(stateMachine);
        }
        private void PerformAttack(StateMachine<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> stateMachine)
        {
            stateMachine.Context.PlayAnimation(EnemyRequestedAnimation.Attack);
            _isComplete = stateMachine.Context.AnimationComplete(stateMachine.Context.AnimationName(EnemyRequestedAnimation.Attack));
        }

        public override EnemyState GetNextState(StateMachine<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> stateMachine)
        {
            if (!_isComplete) return EnemyState.Attack;
            return EnemyState.Idle;
        }

        public override EnemySubState GetNextSubState(StateMachine<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> stateMachine)
        {
            return EnemySubState.Empty;
        }
    }
}