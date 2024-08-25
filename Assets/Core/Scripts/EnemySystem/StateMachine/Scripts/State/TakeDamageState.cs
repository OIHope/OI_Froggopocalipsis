using EnemySystem;
using UnityEngine;

namespace BehaviourSystem.EnemySystem
{
    public class TakeDamageState : State<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor>
    {
        private float _duration = 0f;
        private float _elapsedTime = 0f;
        public override void EnterState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            base.EnterState(stateMachine);
            stateMachine.Context.PlayAnimation(EnemyRequestedAnimation.TakeDamage);

            _duration = 0.5f;
            _elapsedTime = 0f;
        }
        public override void FixedUpdateState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            if (_elapsedTime < _duration) _elapsedTime += Time.deltaTime;
            else _isComplete = true;
        }
        public override EnemyState GetNextState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            if (!_isComplete) return EnemyState.TakeDamage;

            bool targetIsAlive = stateMachine.Context.TargetTransform != null;
            if (!targetIsAlive)
            {
                stateMachine.Context.EnableTargetDetector();
                return EnemyState.Idle;
            }
            return EnemyState.MoveToTarget;
        }
        public override EnemySubState GetNextSubState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            return EnemySubState.Empty;
        }
    }
}