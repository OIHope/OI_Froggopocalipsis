using PlayerSystem;
using System.Collections.Generic;

namespace BehaviourSystem.PlayerSystem
{
    public enum PlayerStates
    { Empty, Idle, Move, Dash, Attack, Stun }
    public enum PlayerSubStates
    { Empty, Aim, NoAim, TakeDamage, DashAttack }

    public class PlayerStateMachine : StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor>
    {
        private State<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> emptyState;
        private State<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> idleState;
        private State<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> moveState;
        private State<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> dashState;
        private State<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> attackState;
        private State<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stunState;
        
        private SubState<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> emptySubState;
        private SubState<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> aimSubState;
        private SubState<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> noAimSubState;
        private SubState<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> takeDamageSubState;
        private SubState<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> dashAttackSubState;

        public PlayerStateMachine(PlayerControllerDataAccessor dataAccessor)
        {
            _dataAccessor = dataAccessor;

            emptyState = new EmptyState();
            idleState = new IdleState();
            moveState = new MoveState();
            dashState = new DashState();
            attackState = new SimpleAttackState();
            stunState = new StunState();

            emptySubState = new EmptySubState();
            aimSubState = new AimSubState();
            noAimSubState = new NoAimSubState();
            takeDamageSubState = new TakeDamageSubState();
            dashAttackSubState = new DashAttackSubState();

            _states = new Dictionary<PlayerStates, State<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor>>
            {
                { PlayerStates.Empty, emptyState },
                { PlayerStates.Idle, idleState },
                { PlayerStates.Move, moveState },
                { PlayerStates.Dash, dashState },
                { PlayerStates.Attack, attackState },
                { PlayerStates.Stun, stunState }
            };
            _subStates = new Dictionary<PlayerSubStates, SubState<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor>>
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