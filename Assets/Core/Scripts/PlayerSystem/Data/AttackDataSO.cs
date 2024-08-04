using UnityEngine;

namespace PlayerSystem
{
    [CreateAssetMenu(fileName = "AttackDataSO", menuName = "Data/Attack Data SO")]

    public class AttackDataSO : ScriptableObject
    {
        [SerializeField] private int _baseDamage;
        [SerializeField] private int _critDamage;
        [SerializeField][Range(0,50)] private int _critChance;
        [Space]
        [SerializeField] private AnimationClip _attackAnim;

        public int Damage => _baseDamage;
        public int CritDamage => _critDamage;
        public int CritChance => _critChance;
        public AnimationClip AttackAnim => _attackAnim;
    }
}