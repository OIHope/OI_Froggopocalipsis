using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourSystem
{
    public abstract class StateMachine<EState, ESubState, ContextData>
        where EState : Enum where ESubState : Enum where ContextData : class
    {
        protected EState _stateKey;
        protected ESubState _subStateKey;

        protected Dictionary<EState, State<EState, ESubState, ContextData>> _states;
        protected Dictionary<ESubState, SubState<EState, ESubState, ContextData>> _subStates;

        protected State<EState, ESubState, ContextData> CurrentState;
        protected SubState<EState, ESubState, ContextData> CurrentSubState;

        protected ContextData _dataAccessor;
        protected StateMachineDataSO _stateMachineData;

        public EState CurrentStateKey => _stateKey;
        public ESubState CurrentSubStateKey => _subStateKey;
        public ContextData Context => _dataAccessor;
        public StateMachineDataSO StateMachineData => _stateMachineData;

        public virtual void UpdateStateMachine()
        {
            if (CurrentState.IsSwitchingState) return;

            EState nextStateKey = CurrentState.GetNextState(this);
            ESubState nextSubStatKey = CurrentState.GetNextSubState(this);

            if (nextStateKey.Equals(CurrentStateKey))
            {
                CurrentState.UpdateState(this);
            }
            else
            {
                SwitchState(nextStateKey);
            }

            if (nextSubStatKey.Equals(CurrentSubStateKey))
            {
                CurrentSubState.UpdateSubState(this);
            }
            else
            {
                SwitchSubState(nextSubStatKey);
            }

        }
        public virtual void FixedUpdateStateMachine()
        {
            if (CurrentState.IsSwitchingState) return;

            CurrentState.FixedUpdateState(this);
            CurrentSubState.FixedUpdateSubState(this);
        }
        public virtual void SwitchState(EState state)
        {
            if (state.Equals(CurrentStateKey)) return;

            CurrentState.ExitState(this);
            _stateKey = state;
            CurrentState = _states[CurrentStateKey];
            CurrentState.EnterState(this);
        }
        public virtual void SwitchSubState(ESubState subState)
        {
            if (subState.Equals(CurrentSubStateKey)) return;

            CurrentSubState.ExitSubState(this);
            _subStateKey = subState;
            CurrentSubState = _subStates[CurrentSubStateKey];
            CurrentSubState.EnterSubState(this);
        }
    }
}