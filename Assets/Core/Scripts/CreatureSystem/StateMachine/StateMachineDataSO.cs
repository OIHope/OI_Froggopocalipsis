using UnityEngine;

namespace BehaviourSystem
{
    [CreateAssetMenu(menuName =("State Machine/States Data SO"))]
    public class StateMachineDataSO : ScriptableObject
    {
        [Header("Player StateMachine Data")]
        [SerializeField] private bool _forPlayerStateMachine;
        [Header("Check states that should work in the stateMachine")]
        [Space]
        [SerializeField] private bool pl_MoveState;
        [SerializeField] private bool pl_TakeDamageState;
        [SerializeField] private bool pl_StunState;
        [SerializeField] private bool pl_SpawnState;
        [SerializeField] private bool pl_DeathState;
        [SerializeField] private bool pl_AttackState;
        [SerializeField] private bool pl_ChargeAttackState;
        [SerializeField] private bool pl_DodgeState;
        [Space(20)]
        [Header("Enemy StateMachine Data")]
        [SerializeField] private bool _forEnemyStateMachine;
        [Header("Check states that should work in the stateMachine")]
        [Space]
        [SerializeField] private bool en_ChargeAttackState;
        [SerializeField] private bool en_RunAwayState;
        [SerializeField] private bool en_MoveToTargetState;
        [SerializeField] private bool en_MakeDistanceState;
        [SerializeField] private bool en_RangeChargeState;
        [SerializeField] private bool en_TakeDamageState;
        [SerializeField] private bool en_StunState;
        [Space(20)]
        [Header("Common State Data")]
        [Space]
        [SerializeField] private bool cm_InvincibleOnTakeDamage;
        [SerializeField] private bool cm_DisplayAttackDirection;

        public bool ForPlayerStateMachine => _forPlayerStateMachine;
        public bool PlMoveState => pl_MoveState;
        public bool PlTakeDamageState => pl_TakeDamageState;
        public bool PlStunState => pl_StunState;
        public bool PlSpawnState => pl_SpawnState;
        public bool PlDeathState => pl_DeathState;
        public bool PlAttackState => pl_AttackState;
        public bool PlChargeAttackState => pl_ChargeAttackState;
        public bool PlDodgeState => pl_DodgeState;

        public bool ForEnemyStateMachine => _forEnemyStateMachine;
        public bool EnChargeAttackState => en_ChargeAttackState;
        public bool EnRangeChargeState => en_RangeChargeState;
        public bool EnRunAwayState => en_RunAwayState;
        public bool EnMoveToTargetState => en_MoveToTargetState;
        public bool EnMakeDistanceState => en_MakeDistanceState;
        public bool EnTakeDamageState => en_TakeDamageState;
        public bool EnStunState => en_StunState;

        public bool InvincibleOnTakeDamage => cm_InvincibleOnTakeDamage;
        public bool DisplayAttackDirection => cm_DisplayAttackDirection;

        private void OnValidate()
        {
            if (_forPlayerStateMachine)
            {
                _forEnemyStateMachine = false;
            }

            if (_forEnemyStateMachine)
            {
                _forPlayerStateMachine = false;
            }
        }
    }
}