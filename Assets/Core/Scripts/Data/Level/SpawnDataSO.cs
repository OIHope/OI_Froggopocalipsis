using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName =("Level/Spawn Data/Entity to spawn"), fileName = ("Spawn Entity Data"))]
    public class SpawnDataSO : ScriptableObject
    {
        [SerializeField] private GameObject _entity;
        [SerializeField][Range(1,999)] private int _spawnCount = 1;
        [Space]
        [Tooltip("Is TRUE, will randomise spawn count between _spawnCount and _maxSpawnCount")]
        [SerializeField] private bool _hasSpawnRate = false;
        [Tooltip("Must be MORE then _spawnCount")]
        [SerializeField][Range(1, 999)] private int _maxSpawnCount = 1;

        public int SpawnCount
        {
            get
            {
                if (!_hasSpawnRate) return _spawnCount;
                return Random.Range(_spawnCount, _maxSpawnCount + 1);
            }
        }
        public GameObject Entity => _entity;
    }
}