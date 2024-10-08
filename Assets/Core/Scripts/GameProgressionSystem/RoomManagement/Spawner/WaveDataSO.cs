using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName =("Level/Wave Defence/Wave Data"), fileName = ("Wave Data"))]
    public class WaveDataSO : ScriptableObject
    {
        [SerializeField] private List<WaveData> _waveList;

        public int WavesCount => _waveList.Count;
        public List<WaveData> WaveList => _waveList;
        public int SpawnCount(int index)
        {
            return _waveList[index].Count;
        }
        public List<GameObject> EntityList(int index)
        {
            return _waveList[index].EntityList;
        }
    }

    [System.Serializable]
    public class WaveData
    {
        [SerializeField] public List<SpawnData> _entityList;

        public int Count
        {
            get
            {
                int count = 0;
                foreach (SpawnData entity in _entityList)
                {
                    count += entity.Count;
                }
                return count;
            }
        }
        public List<GameObject> EntityList
        {
            get
            {
                List<GameObject> list = new List<GameObject>();

                foreach (SpawnData entity in _entityList)
                {
                    for (int i = 0; i < entity.Count; i++)
                    {
                        list.Add(entity.Entity);
                    }
                }

                return list;
            }
        }
    }

    [System.Serializable]
    public class SpawnData
    {
        [SerializeField] private GameObject _entity;
        [SerializeField][Range(1,10)] private int _spawnCount = 1;
        [Space]
        [Tooltip("If TRUE, will randomise spawn count between _spawnCount and _maxSpawnCount")]
        [SerializeField] private bool _hasSpawnRate = false;
        [Tooltip("Must be MORE then _spawnCount")]
        [SerializeField][Range(1, 10)] private int _maxSpawnCount = 1;

        public int Count
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