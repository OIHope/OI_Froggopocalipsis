using Components;
using System;
using UnityEngine;

namespace BehaviourSystem
{
    public abstract class StateSO<EState, ESubState, ContextData> : ScriptableObject 
        where EState : Enum where ESubState : Enum where ContextData : class
    {
        protected bool _isComplete = false;
        protected bool _isSwitchingState = false;
        public bool StateIsComplete => _isComplete;
        public bool IsSwitchingState => _isSwitchingState;
        public virtual void EnterState(StateMachine<EState, ESubState, ContextData> stateMachine) 
        {
            _isComplete = false;
            _isSwitchingState = false;
        }
        public virtual void UpdateState(StateMachine<EState, ESubState, ContextData> stateMachine) { }
        public virtual void FixedUpdateState(StateMachine<EState, ESubState, ContextData> stateMachine) { }
        public virtual void ExitState(StateMachine<EState, ESubState, ContextData> stateMachine) 
        { 
            _isComplete = true;
            _isSwitchingState = true;
        }
        public abstract EState GetNextState(StateMachine<EState, ESubState, ContextData> stateMachine);
        public abstract ESubState GetNextSubState(StateMachine<EState, ESubState, ContextData> stateMachine);
    }
}