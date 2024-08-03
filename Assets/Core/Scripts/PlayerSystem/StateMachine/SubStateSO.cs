using System;
using UnityEngine;

namespace BehaviourSystem
{
    public abstract class SubStateSO : ScriptableObject
    {
        protected bool _isComplete = false;
        protected bool _isSwitchingState = false;
        public bool SubStateIsComplete => _isComplete;
        public bool IsSwitchingSubState => _isSwitchingState;
        public virtual void EnterSubState(PlayerStateManager stateMachine) { }
        public virtual void UpdateSubState(PlayerStateManager stateMachine) { }
        public virtual void FixedUpdateSubState(PlayerStateManager stateMachine) { }
        public virtual void ExitSubState(PlayerStateManager stateMachine) { }
    }
}