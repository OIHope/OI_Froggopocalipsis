using System;
using UnityEngine;

namespace BehaviourSystem
{
    public abstract class StateSO<EState1, EState2> : ScriptableObject where EState1 : Enum where EState2 : Enum
    {
        protected bool _isComplete = false;
        protected bool _isSwitchingState = false;
        public bool StateIsComplete => _isComplete;
        public bool IsSwitchingState => _isSwitchingState;
        public virtual void EnterState(PlayerStateMachine stateMachine) 
        {
            _isComplete = false;
            _isSwitchingState = false;
        }
        public virtual void UpdateState(PlayerStateMachine stateMachine) { }
        public virtual void FixedUpdateState(PlayerStateMachine stateMachine) { }
        public virtual void ExitState(PlayerStateMachine stateMachine) 
        { 
            _isComplete = true;
            _isSwitchingState = true;
        }
        public abstract EState1 GetNextState(PlayerStateMachine stateMachine);
        public abstract EState2 GetNextSubState(PlayerStateMachine stateMachine);
    }
}