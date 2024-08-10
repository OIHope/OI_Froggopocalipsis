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

        public EnemyVisionDataSO EnemyVisionData => _enemyVisionData;
    }
}