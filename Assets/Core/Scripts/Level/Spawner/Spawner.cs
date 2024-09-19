using Data;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    public class Spawner : MonoBehaviour
    {
        [Header("This object must be placed on the scene")]
        [Header("Entities will spawn in range, be sure to have no obstacles in spawn area")]
        [Space(10)]
        [SerializeField] private WaveDataSO _waveList;
        [SerializeField][Range(0.5f, 10f)] private float _spawnRange = 1f;

        private int _currentWaveIndex = 0;

        public bool CanSpawnAnotherWave => _currentWaveIndex < _waveList.WavesCount;

        public int InitSpawn()
        {
            if (_currentWaveIndex >= _waveList.WavesCount) return 0;

            int spawnedCount = 0;
            int spawnCount = _waveList.SpawnCount(_currentWaveIndex);

            spawnedCount += spawnCount;

            List<GameObject> entitiesToSpawn = _waveList.EntityList(_currentWaveIndex);

            foreach (GameObject entity in entitiesToSpawn)
            {
                Instantiate(entity, GetSpawnPosition(), Quaternion.identity, this.transform);
            }
            _currentWaveIndex++;

            return spawnedCount;
        }
        private Vector3 GetSpawnPosition()
        {
            Vector3 startPos = transform.position;
            Vector3 spawnDirection = new(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            Vector3 offsetPos = new(Random.Range(0.5f, _spawnRange), 0f, Random.Range(0.5f, _spawnRange));
            Vector3 prePos = new(offsetPos.x * spawnDirection.x, 0f, offsetPos.z * spawnDirection.z);

            return startPos + prePos;
        }
    }
}