using Components;
using PlayerSystem;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourSystem
{
    public enum PlayerStates
    { Empty, Idle, Move, Dash, Attack, Stun}
    public enum PlayerSubStates
    { Empty, Aim, NoAim, TakeDamage, DashAttack }

    public class PlayerStateMachine : StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor>
    {
        [Header("Main States")]
        [Space]
        [SerializeField] private StateSO<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> emptyState;
        [SerializeField] private StateSO<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> idleState;
        [SerializeField] private StateSO<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> moveState;
        [SerializeField] private StateSO<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> dashState;
        [SerializeField] private StateSO<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> attackState;
        [SerializeField] private StateSO<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stunState;
        [Space]
        [Header("Sub States")]
        [Space]
        [SerializeField] private SubStateSO<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> emptySubState;
        [SerializeField] private SubStateSO<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> aimSubState;
        [SerializeField] private SubStateSO<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> noAimSubState;
        [SerializeField] private SubStateSO<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> takeDamageSubState;
        [SerializeField] private SubStateSO<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> dashAttackSubState;

        public override void SetupStateMachine(PlayerControllerDataAccessor dataAccessor)
        {
            _dataAccessor = dataAccessor;

            _states = new Dictionary<PlayerStates, StateSO<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor>>
            {
                { PlayerStates.Empty, emptyState },
                { PlayerStates.Idle, idleState },
                { PlayerStates.Move, moveState },
                { PlayerStates.Dash, dashState },
                { PlayerStates.Attack, attackState },
                { PlayerStates.Stun, stunState }
            };
            _subStates = new Dictionary<PlayerSubStates, SubStateSO<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor>>
            {
                { PlayerSubStates.Empty, emptySubState },
                { PlayerSubStates.Aim, aimSubState },
                { PlayerSubStates.NoAim, noAimSubState },
                { PlayerSubStates.TakeDamage, takeDamageSubState },
                { PlayerSubStates.DashAttack, dashAttackSubState }
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