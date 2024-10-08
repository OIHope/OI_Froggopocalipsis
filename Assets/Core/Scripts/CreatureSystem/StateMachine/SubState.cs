using System;

namespace BehaviourSystem
{
    public abstract class SubState<EState, ESubState, ContextData>
        where EState : Enum where ESubState : Enum where ContextData : class
    {
        protected bool _isComplete = false;
        protected bool _isSwitchingState = false;
        public bool SubStateIsComplete => _isComplete;
        public bool IsSwitchingSubState => _isSwitchingState;
        public virtual void EnterSubState(StateMachine<EState, ESubState, ContextData> stateMachine) { }
        public virtual void UpdateSubState(StateMachine<EState, ESubState, ContextData> stateMachine) { }
        public virtual void FixedUpdateSubState(StateMachine<EState, ESubState, ContextData> stateMachine) { }
        public virtual void ExitSubState(StateMachine<EState, ESubState, ContextData> stateMachine) { }
    }
}