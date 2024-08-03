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
        public virtual void EnterState(PlayerStateManager stateMachine) 
        {
            _isComplete = false;
            _isSwitchingState = false;
        }
        public virtual void UpdateState(PlayerStateManager stateMachine) { }
        public virtual void FixedUpdateState(PlayerStateManager stateMachine) { }
        public virtual void ExitState(PlayerStateManager stateMachine) 
        { 
            _isComplete = true;
            _isSwitchingState = true;
        }
        public abstract EState1 GetNextState(PlayerStateManager stateMachine);
        public abstract EState2 GetNextSubState(PlayerStateManager stateMachine);
    }
}