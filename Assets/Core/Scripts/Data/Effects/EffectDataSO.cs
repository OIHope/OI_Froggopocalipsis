using UnityEngine;

namespace Data
{
    public abstract class EffectDataSO : ScriptableObject
    {
        [Header("Effect Data")]
        [Space]
        [SerializeField] protected float _duration;
        [SerializeField][Range(0,75)] protected int _chance;

        public float EffectDuration => _duration;
        public int EffectChance => _chance;

        public abstract void ApplyEffect();
    }
}