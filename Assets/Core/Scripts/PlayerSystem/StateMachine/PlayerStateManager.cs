using PlayerSystem;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourSystem
{
    public enum PlayerStates
    { Empty, Idle, Move, Dash, Attack, Stun}
    public enum PlayerSubStates
    { Empty, Aim, NoAim, TakeDamage }
    public class PlayerStateManager : MonoBehaviour
    {
        [Header("State Machine Settings")]
        [Space]
        [SerializeField] private PlayerStates _stateKey;
        [SerializeField] private PlayerSubStates _subStateKey;
        [Space]
        [Header("Main States")]
        [Space]
        [SerializeField] private StateSO<PlayerStates, PlayerSubStates> emptyState;
        [SerializeField] private StateSO<PlayerStates, PlayerSubStates> idleState;
        [SerializeField] private StateSO<PlayerStates, PlayerSubStates> moveState;
        [SerializeField] private StateSO<PlayerStates, PlayerSubStates> dashState;
        [SerializeField] private StateSO<PlayerStates, PlayerSubStates> attackState;
        [SerializeField] private StateSO<PlayerStates, PlayerSubStates> stunState;
        [Space]
        [Header("Sub States")]
        [Space]
        [SerializeField] private SubStateSO emptySubState;
        [SerializeField] private SubStateSO aimSubState;
        [SerializeField] private SubStateSO noAimSubState;
        [SerializeField] private SubStateSO takeDamageSubState;


        private Dictionary<PlayerStates, StateSO<PlayerStates, PlayerSubStates>> _states;
        private Dictionary<PlayerSubStates, SubStateSO> _subStates;

        private StateSO<PlayerStates, PlayerSubStates> CurrentState;
        private SubStateSO CurrentSubState;

        private PlayerControllerContext _context;

        public PlayerStates StateKey => _stateKey;
        public PlayerSubStates SubStateKey => _subStateKey;
        public PlayerControllerContext Context => _context;

        public void SetupStateManager(PlayerControllerContext context)
        {
            _context = context;

            _states = new Dictionary<PlayerStates, StateSO<PlayerStates, PlayerSubStates>>
            {
                { PlayerStates.Empty, emptyState },
                { PlayerStates.Idle, idleState },
                { PlayerStates.Move, moveState },
                { PlayerStates.Dash, dashState },
                { PlayerStates.Attack, attackState },
                { PlayerStates.Stun, stunState }
            };
            _subStates = new Dictionary<PlayerSubStates, SubStateSO>
            {
                { PlayerSubStates.Empty, emptySubState },
                { PlayerSubStates.Aim, aimSubState },
                { PlayerSubStates.NoAim, noAimSubState },
                { PlayerSubStates.TakeDamage, takeDamageSubState }
            };

            _stateKey = PlayerStates.Idle;
            CurrentState = _states[_stateKey];
            CurrentState.EnterState(this);

            _subStateKey = PlayerSubStates.NoAim;
            CurrentSubState = _subStates[_subStateKey];
            CurrentSubState.EnterSubState(this);
        }

        private void Update()
        {
            if (CurrentState.IsSwitchingState) return;

            PlayerStates nextStateKey = CurrentState.GetNextState(this);
            PlayerSubStates nextSubStatKey = CurrentState.GetNextSubState(this);

            if (nextStateKey.Equals(_stateKey))
            {
                CurrentState.UpdateState(this);
            }
            else
            {
                SwitchState(nextStateKey);
            }

            if (nextSubStatKey.Equals(_subStateKey))
            {
                CurrentSubState.UpdateSubState(this);
            }
            else
            {
                SwitchSubState(nextSubStatKey);
            }

        }
        private void FixedUpdate()
        {
            if (CurrentState.IsSwitchingState) return;

            CurrentState.FixedUpdateState(this);
            CurrentSubState.FixedUpdateSubState(this);
        }
        public void SwitchState(PlayerStates state)
        {
            if (state.Equals(_stateKey)) return;

            CurrentState.ExitState(this);
            _stateKey = state;
            CurrentState = _states[_stateKey];
            CurrentState.EnterState(this);
        }
        public void SwitchSubState(PlayerSubStates subState)
        {
            if (subState.Equals(_subStateKey)) return;

            CurrentSubState.ExitSubState(this);
            _subStateKey = subState;
            CurrentSubState= _subStates[_subStateKey];
            CurrentSubState.EnterSubState(this);
        }
    }
}