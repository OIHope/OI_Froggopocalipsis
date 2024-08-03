using UnityEngine;

namespace PlayerSystem
{
    [CreateAssetMenu(fileName = "Stats SO", menuName = "Stats/Player Stats SO")]
    public class PlayerStatsSO : ScriptableObject
    {
        [Header("State values")]
        [SerializeField] private float _stunDuration = 1f;
        [Space(25)]
        [Header("Movement")]
        [SerializeField] private float _moveSpeed = 10f;
        [SerializeField] private float _dashSpeed = 25f;
        [Space]
        [SerializeField] private AnimationCurve _dashAnimationCurve;
        [Space(25)]
        [Header("Animation")]
        [SerializeField] private AnimationClip _idleAnim;
        [SerializeField] private string _idleAnimName;
        [Space]
        [SerializeField] private AnimationClip _moveAnim;
        [SerializeField] private string _moveAnimName;


        public float StunDuration => _stunDuration;
        public float MoveSpeed => _moveSpeed;
        public float DashSpeed => _dashSpeed;
        public AnimationCurve DashAnimationCurve => _dashAnimationCurve;

    }
}