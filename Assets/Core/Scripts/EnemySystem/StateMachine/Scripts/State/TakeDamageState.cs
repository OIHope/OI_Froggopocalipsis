using Data;
using EnemySystem;
using UnityEngine;

namespace BehaviourSystem.EnemySystem
{
    public class TakeDamageState : State<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor>
    {
        public override void EnterState(StateMachine<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> stateMachine)
        {
            base.EnterState(stateMachine);
            stateMachine.Context.PlayAnimation(EnemyRequestedAnimation.TakeDamage);
        }
        public override void UpdateState(StateMachine<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> stateMachine)
        {
            _isComplete = stateMachine.Context.AnimationComplete(stateMachine.Context.AnimationName(EnemyRequestedAnimation.TakeDamage));
        }
        public override EnemyState GetNextState(StateMachine<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> stateMachine)
        {
            if (!_isComplete) return EnemyState.TakeDamage;
            return EnemyState.Idle;
        }
        public override EnemySubState GetNextSubState(StateMachine<EnemyState, EnemySubState, MeleeZombieControllerDataAccessor> stateMachine)
        {
            return EnemySubState.Invincible;
        }
    }
}