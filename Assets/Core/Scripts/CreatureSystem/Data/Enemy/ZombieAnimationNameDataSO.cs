using UnityEngine;

namespace Data
{
    public enum EnemyRequestedAnimation
    { Idle, Walk, Run, Charge, Attack, TakeDamage, Spawn, Die }

    [CreateAssetMenu(menuName =("Data/Enemy/AnimationNaming DataSO"))]
    public class ZombieAnimationNameDataSO : ScriptableObject
    {
        [Header("Front animations")]
        [Space]
        [SerializeField] private string idleAnimationName;
        [SerializeField] private string walkAnimationName;
        [SerializeField] private string runAnimationName;
        [SerializeField] private string chargeAnimationName;
        [SerializeField] private string attackAnimationName;
        [SerializeField] private string takeDamageAnimationName;
        [SerializeField] private string spawnAnimationName;
        [SerializeField] private string dieAnimationName;
        [Space(15)]
        [Header("Back animations")]
        [Space]
        [SerializeField] private string idleBackAnimationName;
        [SerializeField] private string walkBackAnimationName;
        [SerializeField] private string runBackAnimationName;
        [SerializeField] private string chargeBackAnimationName;
        [SerializeField] private string attackBackAnimationName;
        [SerializeField] private string takeDamageBackAnimationName;
        [SerializeField] private string spawnBackAnimationName;
        [SerializeField] private string dieBackAnimationName;

        public string IdleAnimationName => idleAnimationName;
        public string WalkAnimationName => walkAnimationName;
        public string RunAnimationName => runAnimationName;
        public string ChargeAnimationName => chargeAnimationName;
        public string AttackAnimationName => attackAnimationName;
        public string TakeDamageAnimationName => takeDamageAnimationName;
        public string SpawnAnimationName => spawnAnimationName;
        public string DieAnimationName => dieAnimationName;

        public string IdleBackAnimationName => idleBackAnimationName;
        public string WalkBackAnimationName => walkBackAnimationName;
        public string RunBackAnimationName => runBackAnimationName;
        public string ChargeBackAnimationName => chargeBackAnimationName;
        public string AttackBackAnimationName => attackBackAnimationName;
        public string TakeDamageBackAnimationName => takeDamageBackAnimationName;
        public string SpawnBackAnimationName => spawnBackAnimationName;
        public string DieBackAnimationName => dieBackAnimationName;
    }
}