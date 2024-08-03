using UnityEngine;

namespace BehaviourSystem
{
    [CreateAssetMenu(fileName = "Attack State", menuName = ("State Machine/State/Attack State"))]
    public class AttackState : StateSO<PlayerStates, PlayerSubStates>, IGravityAffected<PlayerStateManager>
    {
        public override void EnterState(PlayerStateManager stateMachine)
        {
            throw new System.NotImplementedException();
        }

        public override void ExitState(PlayerStateManager stateMachine)
        {
            throw new System.NotImplementedException();
        }

        public override void FixedUpdateState(PlayerStateManager stateMachine)
        {
            HandleGravity(stateMachine);

        }

        public override void UpdateState(PlayerStateManager stateMachine)
        {
            throw new System.NotImplementedException();
        }

        public void HandleGravity(PlayerStateManager stateMachine)
        {
            float gravity = -0.5f;
            stateMachine.Context.Controller.Move(gravity * Time.deltaTime * Vector3.up);
        }

        public override PlayerStates GetNextState(PlayerStateManager stateMachine)
        {
            throw new System.NotImplementedException();
        }

        public override PlayerSubStates GetNextSubState(PlayerStateManager stateMachine)
        {
            throw new System.NotImplementedException();
        }
    }
}