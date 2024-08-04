using PlayerSystem;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourSystem
{
    [CreateAssetMenu(fileName = "DashAttack SubState", menuName = ("State Machine/SubState/DashAttack SubState"))]
    public class DashAttackSubState : SubStateSO
    {
        [SerializeField] private List<AttackDataSO> attackDataList;
        public override void EnterSubState(PlayerStateMachine stateMachine)
        {
            AttackDataSO attackDataSO = attackDataList[Random.Range(0, attackDataList.Count)];
            stateMachine.Context.PerformAttack(attackDataSO, stateMachine.Context.AimDirection);
        }

        public override void ExitSubState(PlayerStateMachine stateMachine)
        {
            stateMachine.Context.FinishAttack();
            stateMachine.Context.StartAttackCooldown();
        }
    }
}