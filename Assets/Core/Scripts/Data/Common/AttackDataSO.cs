using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = ("Attack DataSO"), menuName = ("Data/Common/Attack Data"))]
    public class AttackDataSO : ScriptableObject
    {
        [Header("Damage")]
        [Space]
        [SerializeField] private int _damage;
        [SerializeField][Range(0, 75)] private int _critDamageChance;
        [Space]
        [SerializeField] private float _chargeTime;
        [SerializeField] private float _cooldownTime;
        [Space]
        [Header("Effect")]
        [Space]
        [SerializeField] private EffectDataSO _effectData;

        private int _critDamage;

        public int Damage => _damage;
        public int CritDamage => (int)(Damage + (Damage * 0.35));
        public int CritChance => _critDamageChance;
        public float ChargeTime => _chargeTime;
        public float CooldownTime => _cooldownTime;
        public EffectDataSO EffectData => _effectData;

        public int GetDamage
        {
            get
            {
                int chance = Random.Range(0, 101);
                return chance >= CritChance ? Damage : CritDamage;
            }
        }

        public void SetDamageValue(int damage) { _damage = damage; }
        public void SetCritChance(int value) { _critDamageChance = value; }
        public void SetCooldownTime(float value) { _cooldownTime = value; }
    }
}