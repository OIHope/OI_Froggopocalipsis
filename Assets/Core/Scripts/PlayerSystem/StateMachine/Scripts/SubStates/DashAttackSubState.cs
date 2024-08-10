using Data;
using PlayerSystem;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourSystem
{
    [CreateAssetMenu(fileName = "DashAttack SubState", menuName = ("State Machine/Player/SubState/DashAttack SubState"))]
    public class DashAttackSubState : SubStateSO<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor>
    {
        [SerializeField] private List<AttackDataSO> attackDataList;
        public override void EnterSubState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            AttackDataSO attackDataSO = attackDataList[Random.Range(0, attackDataList.Count)];
            stateMachine.Context.PerformAttack(attackDataSO, stateMachine.Context.AimDirection);
            stateMachine.Context.StartAttackCooldown();
        }

        public override void ExitSubState(StateMachine<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor> stateMachine)
        {
            stateMachine.Context.FinishAttack();
        }
    }
}