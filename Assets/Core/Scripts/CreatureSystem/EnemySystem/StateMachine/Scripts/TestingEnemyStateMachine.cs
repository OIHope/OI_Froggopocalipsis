using Entity.EnemySystem;
using UnityEngine;

public class TestingEnemyStateMachine : MonoBehaviour
{
    [SerializeField] private EnemyController _testSubject;

    public void EnterEmptyState()
    {
        _testSubject.StateMachine.SwitchState(BehaviourSystem.EnemySystem.EnemyState.Empty);
    }
    public void EnterIdleState()
    {
        _testSubject.StateMachine.SwitchState(BehaviourSystem.EnemySystem.EnemyState.Idle);
    }
    public void EnterRoamState()
    {
        _testSubject.StateMachine.SwitchState(BehaviourSystem.EnemySystem.EnemyState.Roaming);
    }
    public void EnterMoveToTargetState()
    {
        _testSubject.StateMachine.SwitchState(BehaviourSystem.EnemySystem.EnemyState.MoveToTarget);
    }
    public void EnterSpawnState()
    {
        _testSubject.StateMachine.SwitchState(BehaviourSystem.EnemySystem.EnemyState.Spawn);
    }
    public void EnterDieState()
    {
        _testSubject.StateMachine.SwitchState(BehaviourSystem.EnemySystem.EnemyState.Death);
    }
    public void EnterAttackState()
    {
        _testSubject.StateMachine.SwitchState(BehaviourSystem.EnemySystem.EnemyState.Attack);
    }
}
