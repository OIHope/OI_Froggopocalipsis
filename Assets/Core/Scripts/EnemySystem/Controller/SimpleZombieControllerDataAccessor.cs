using Components;
using Entity.EnemySystem;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

namespace EnemySystem
{
    public class SimpleZombieControllerDataAccessor
    {
        private SimpleZombieController _controllerData;
        private Vector3 _startPos;

        public bool IsStatic => _controllerData.EnemyData.RoamingData.IsStatic;

        public int HP => _controllerData.EnemyData.HealthData.StartHP;
        public int MaxHP => _controllerData.EnemyData.HealthData.MaxHP;

        public int Damage => _controllerData.EnemyData.AttackData.Damage;
        public int CritDamage => _controllerData.EnemyData.AttackData.CritDamage;
        public int CritChance => _controllerData.EnemyData.AttackData.CritChance;
        public int RoamingDistance => _controllerData.EnemyData.RoamingData.RoamingDistance;

        public float WalkSpeed => _controllerData.EnemyData.MovementData.WalkSpeed;
        public float RunSpeed => _controllerData.EnemyData.MovementData.RunSpeed;
        public float StopDistance => _controllerData.EnemyData.RoamingData.StopDistance;
        public float WaitTillRoam => _controllerData.EnemyData.RoamingData.WaitTillRoamTime;
        public float CheckPointDistance => _controllerData.EnemyData.RoamingData.CheckPointDistance;

        public float SightDistance => _controllerData.EnemyData.EnemyVisionData.SightDistance;
        public float TimeToSpotTarget => _controllerData.EnemyData.EnemyVisionData.TimeToSpotTarget;

        public Vector3 StartPosition => _startPos;
        public Vector3 AimDirection { get; set; }

        public NavMeshAgent Agent { get => _controllerData.Agent; set => _controllerData.Agent = value; }

        public void PerformAttack(AttackType attackType) => _controllerData.DamageDealerComponent.PerformAttack(AimDirection, attackType);
        public void FinishAttack() => _controllerData.DamageDealerComponent.FinishAttack();
        public void StartAttackCooldown() => _controllerData.AttackCooldown.Cooldown(_controllerData.DamageDealerComponent.LastAttackData.CooldownTime);


        public SimpleZombieControllerDataAccessor(SimpleZombieController controllerData)
        {
            _controllerData = controllerData;
            _startPos = _controllerData.transform.position;
        }
    }
}