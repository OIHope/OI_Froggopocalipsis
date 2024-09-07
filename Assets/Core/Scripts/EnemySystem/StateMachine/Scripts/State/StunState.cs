using EnemySystem;

namespace BehaviourSystem.EnemySystem
{
    public class StunState : State<EnemyState, EnemySubState, EnemyControllerDataAccessor>
    {
        public override EnemyState GetNextState(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            throw new System.NotImplementedException();
        }

        public override EnemySubState GetNextSubState(StateMachine<EnemyState, EnemySubState, EnemyControllerDataAccessor> stateMachine)
        {
            throw new System.NotImplementedException();
        }
    }
}