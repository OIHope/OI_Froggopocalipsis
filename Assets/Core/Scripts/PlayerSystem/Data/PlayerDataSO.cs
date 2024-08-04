using UnityEngine;

namespace PlayerSystem
{
    [CreateAssetMenu(fileName = "PlayerDataSO", menuName = "Data/Player Data SO")]
    public class PlayerDataSO : ScriptableObject
    {
        [Header("Movement")]
        [SerializeField] private float _moveSpeed = 10f;
        [SerializeField] private float _dashSpeed = 25f;
        [Space]
        [SerializeField] private AnimationCurve _dashAnimationCurve;
        [Space(25)]
        [Header("Attack")]
        [SerializeField] private int _baseDamage = 1;
        [SerializeField] private int _critChance = 10;
        [Space]
        [SerializeField] private float _attackSlideDistance = 5f;
        [Space]
        [SerializeField] private AnimationCurve _attackAnimationCurve;
        [Space(25)]
        [Header("Cooldown values")]
        [SerializeField] private float _stunDuration = 1f;
        [SerializeField] private float _attackCooldown = 0.5f;
        [SerializeField] private float _dashCooldown = 0.5f;
        [Space(25)]
        [Header("Animation")]
        [SerializeField] private AnimationClip _idleAnim;
        [SerializeField] private string _idleAnimName;
        [Space]
        [SerializeField] private AnimationClip _moveAnim;
        [SerializeField] private string _moveAnimName;


        public int Damage => _baseDamage;
        public int CritChance => _critChance;
        public float AttackSlideDistance => _attackSlideDistance;
        public float StunDuration => _stunDuration;
        public float MoveSpeed => _moveSpeed;
        public float DashSpeed => _dashSpeed;
        public float AttackCooldown => _attackCooldown;
        public float DashCooldown => _dashCooldown;
        public AnimationCurve AttackAnimationCurve => _attackAnimationCurve;
        public AnimationCurve DashAnimationCurve => _dashAnimationCurve;

    }
}