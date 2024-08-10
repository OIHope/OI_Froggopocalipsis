using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = ("Attack DataSO"), menuName = ("Data/Common/Attack Data"))]
    public class AttackDataSO : ScriptableObject
    {
        [Header("Damage")]
        [Space]
        [SerializeField] private int _damage;
        [SerializeField] private int _critDamage;
        [SerializeField][Range(0, 75)] private int _critDamageChance;
        [Space]
        [SerializeField] private float _chargeTime;
        [SerializeField] private float _cooldownTime;
        [Space]
        [SerializeField] private float _attackSlideDistance;
        [SerializeField] private AnimationCurve _attackAnimationCurve;
        [Space]
        [Header("Effect")]
        [Space]
        [SerializeField] private EffectDataSO _effectData;

        public int Damage => _damage;
        public int CritDamage => _critDamage;
        public int CritChance => _critDamageChance;
        public float ChargeTime => _chargeTime;
        public float CooldownTime => _cooldownTime;
        public float AttackSlideDistance => _attackSlideDistance;
        public AnimationCurve AttackAnimationCurve => _attackAnimationCurve;
        public EffectDataSO EffectData => _effectData;
    }
}