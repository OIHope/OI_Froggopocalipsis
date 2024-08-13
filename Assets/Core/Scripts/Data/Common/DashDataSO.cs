using UnityEngine;
using Utilities;

namespace Data
{
    [CreateAssetMenu(fileName = ("Dash DataSO"), menuName = ("Data/Common/Dash Data"))]
    public class DashDataSO : ScriptableObject
    {
        [Header("Dash Settings")]
        [Space]
        [SerializeField] private float _dashSpeed;
        [SerializeField] private float _dashCooldown;
        [Space]
        [SerializeField] private AnimationCurve _dashAnimationCurve;

        public float DashSpeed => _dashSpeed;
        public float CooldownTime => _dashCooldown;
        public float Duration => AnimationCurveDuration.Duration(_dashAnimationCurve);
        public AnimationCurve DashAnimationCurve => _dashAnimationCurve;
    }
}