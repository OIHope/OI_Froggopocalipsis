using PlayerSystem;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourSystem.PlayerSystem
{
    public enum PlayerStates
    { Empty, Idle, Move, Dodge, Attack, Stun }
    public enum PlayerSubStates
    { Empty, Aim, NoAim, FixedAim, TakeDamage}

    public class PlayerStateMachine : StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor>
    {
        private State<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> emptyState;
        private State<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> idleState;
        private State<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> moveState;
        private State<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> dodgeState;
        private State<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> attackState;
        private State<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stunState;
        
        private SubState<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> emptySubState;
        private SubState<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> aimSubState;
        private SubState<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> noAimSubState;
        private SubState<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> fixedAimSubState;
        private SubState<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> takeDamageSubState;

        public PlayerStateMachine(PlayerControllerDataAccessor dataAccessor)
        {
            _dataAccessor = dataAccessor;
            _stateMachineData = dataAccessor.StateMachineData;

            if (_stateMachineData.ForEnemyStateMachine)
            {
                Debug.Log("<color=red>Wrong StateMachineData!</color>");
                return;
            }

            emptyState = new EmptyState();
            idleState = new IdleState();
            moveState = new MoveState();
            dodgeState = new DodgeState();
            attackState = new SimpleAttackState();
            stunState = new StunState();

            emptySubState = new EmptySubState();
            aimSubState = new AimSubState();
            noAimSubState = new NoAimSubState();
            fixedAimSubState = new FixedAimSubState();
            takeDamageSubState = new TakeDamageSubState();

            _states = new Dictionary<PlayerStates, State<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor>>
            {
                { PlayerStates.Empty, emptyState },
                { PlayerStates.Idle, idleState },
                { PlayerStates.Move, moveState },
                { PlayerStates.Dodge, dodgeState },
                { PlayerStates.Attack, attackState },
                { PlayerStates.Stun, stunState }
            };
            _subStates = new Dictionary<PlayerSubStates, SubState<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor>>
            {
                { PlayerSubStates.Empty, emptySubState },
                { PlayerSubStates.Aim, aimSubState },
                { PlayerSubStates.NoAim, noAimSubState },
                { PlayerSubStates.FixedAim, fixedAimSubState },
                { PlayerSubStates.TakeDamage, takeDamageSubState },
            };

            _stateKey = PlayerStates.Idle;
            CurrentState = _states[_stateKey];
            CurrentState.EnterState(this);

            _subStateKey = PlayerSubStates.NoAim;
            CurrentSubState = _subStates[_subStateKey];
            CurrentSubState.EnterSubState(this);
        }
    }
}