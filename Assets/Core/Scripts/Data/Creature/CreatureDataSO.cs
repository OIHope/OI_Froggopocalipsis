using UnityEngine;

namespace Data
{
    public abstract class CreatureDataSO : ScriptableObject
    {
        [Header("Basic Data")]
        [Space]
        [SerializeField] protected HealthDataSO _healthData;
        [SerializeField] protected AttackDataSO _attackData;
        [SerializeField] protected MovementDataSO _movementData;

        public HealthDataSO HealthData => _healthData;
        public AttackDataSO AttackData => _attackData;
        public MovementDataSO MovementData => _movementData;
    }
}