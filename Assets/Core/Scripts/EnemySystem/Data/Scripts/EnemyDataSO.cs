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

        public EnemyVisionDataSO EnemyVisionData => _enemyVisionData;
        public RoamingDataSO RoamingData => _roamingData;
    }
}