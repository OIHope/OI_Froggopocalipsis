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
        [SerializeField] private List<SpawnDataSO> _spawnList;
        [SerializeField][Range(0.5f, 10f)] private float _spawnRange = 1f;

        [ContextMenu("Spawn Entities")]
        public void InitSpawn()
        {
            foreach (SpawnDataSO entity in _spawnList)
            {
                int spawnCound = entity.SpawnCount;
                for (int i = 0; i < spawnCound; i++)
                {
                    Instantiate(entity.Entity, GetSpawnPosition(), Quaternion.identity, this.transform); 
                }
            }
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