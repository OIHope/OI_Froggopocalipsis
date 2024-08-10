using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem
{
    public class SimpleZombieControllerDataAccessor
    {
        private SimpleZombieController _controllerData;
        private Vector3 _startPos;

        public int HP => _controllerData.EnemyData.HealthData.StartHP;
        public int MaxHP => _controllerData.EnemyData.HealthData.MaxHP;

        public int Damage => _controllerData.EnemyData.AttackData.Damage;
        public int CritDamage => _controllerData.EnemyData.AttackData.CritDamage;
        public int CritChance => _controllerData.EnemyData.AttackData.CritChance;

        public float WalkSpeed => _controllerData.EnemyData.MovementData.WalkSpeed;
        public float RunSpeed => _controllerData.EnemyData.MovementData.RunSpeed;

        public float SightDistance => _controllerData.EnemyData.EnemyVisionData.SightDistance;
        public float TimeToSpotTarget => _controllerData.EnemyData.EnemyVisionData.TimeToSpotTarget;

        public Vector3 StartPosition => _startPos;

        public NavMeshAgent Agent { get => _controllerData.Agent; set => _controllerData.Agent = value; }


        public SimpleZombieControllerDataAccessor(SimpleZombieController controllerData)
        {
            _controllerData = controllerData;
            _startPos = _controllerData.transform.position;
        }
    }
}