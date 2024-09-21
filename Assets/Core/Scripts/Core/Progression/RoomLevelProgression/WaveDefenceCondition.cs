using Level;
using System.Collections;
using UnityEngine;

namespace Core.Progression
{
    public class WaveDefenceCondition : RoomCondition
    {
        [SerializeField] private Spawner _spawner;
        [SerializeField] private float _delayBetweenWaves = 1f;

        private int _enemyCount = 0;
        private int _enemyDestroyedCount = 0;
        private bool _finished = false;

        private bool AllWavesAreFinished => !_spawner.CanSpawnAnotherWave;
        private bool AllEnemiesDestroyed => _enemyDestroyedCount >= _enemyCount;

        public override bool ConditionMet()
        {
            return AllEnemiesDestroyed && AllWavesAreFinished && _finished;
        }

        public override void PrepareCondition()
        {
            ResetCondition();
            StartCoroutine(StartWaveDefenceCondition());
        }

        private IEnumerator StartWaveDefenceCondition()
        {
            GameEventsBase.OnEnemyDeath += AddToEnemyDefeatedCount;

            while (!AllWavesAreFinished)
            {
                yield return new WaitForSeconds(_delayBetweenWaves);

                int waveEnemiesCount = _spawner.InitSpawn();
                _enemyCount += waveEnemiesCount;

                yield return new WaitUntil(() => AllEnemiesDestroyed);
            }
            yield return new WaitForSeconds(_delayBetweenWaves);

            _finished = true;
            GameEventsBase.OnEnemyDeath -= AddToEnemyDefeatedCount;
        }

        private void AddToEnemyDefeatedCount() => _enemyDestroyedCount++;

        private void ResetCondition()
        {
            _enemyCount = 0;
            _enemyDestroyedCount = 0;
            _finished = false;
        }

        public override void ClearAfterYourself()
        {

        }
    }
}