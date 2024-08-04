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
        public virtual void EnterSubState(PlayerStateMachine stateMachine) { }
        public virtual void UpdateSubState(PlayerStateMachine stateMachine) { }
        public virtual void FixedUpdateSubState(PlayerStateMachine stateMachine) { }
        public virtual void ExitSubState(PlayerStateMachine stateMachine) { }
    }
}