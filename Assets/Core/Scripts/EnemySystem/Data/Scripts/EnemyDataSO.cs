using UnityEngine;
using Data;

namespace EnemySystem
{
    [CreateAssetMenu(fileName =("Enemy DataSO"),menuName =("Data/Enemy/Enemy Data"))]
    public class EnemyDataSO : CreatureDataSO
    {
        [Space]
        [Header("Additional Data")]
        [Space]
        [SerializeField] private EnemyVisionDataSO _enemyVisionData;
        [SerializeField] private RoamingDataSO _roamingData;
        [Header("Attack Data")]
        [SerializeField] protected float _minAttackRange;
        [SerializeField] protected float _maxAttackRange;
        [Space]
        [Header("This is for ranged enemy types!")]
        [SerializeField] protected GameObject _projectile;

        public EnemyVisionDataSO EnemyVisionData => _enemyVisionData;
        public RoamingDataSO RoamingData => _roamingData;
        public float MinAttackRange => _minAttackRange;
        public float MaxAttackRange => _maxAttackRange;
        public GameObject Projectile => _projectile;
    }
}