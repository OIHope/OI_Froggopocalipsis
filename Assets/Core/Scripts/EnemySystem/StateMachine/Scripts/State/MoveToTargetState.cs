using EnemySystem;

namespace BehaviourSystem.EnemySystem
{
    public class MoveToTargetState : State<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor>
    {
        public override EnemyState GetNextState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            throw new System.NotImplementedException();
        }

        public override EnemySubState GetNextSubState(StateMachine<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor> stateMachine)
        {
            throw new System.NotImplementedException();
        }
    }
}