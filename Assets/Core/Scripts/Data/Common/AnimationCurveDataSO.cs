using UnityEngine;
using Utilities;

namespace Data
{
    [CreateAssetMenu(menuName =("Data/Common/AnimationCurve Data"))]
    public class AnimationCurveDataSO : ScriptableObject
    {
        [SerializeField] private float _slideDistance;
        [SerializeField] private AnimationCurve _animationCurve;

        public float SlideDistance => _slideDistance;
        public float Duration => AnimationCurveDuration.Duration(_animationCurve);
        public AnimationCurve DataAnimationCurve => _animationCurve;
    }
}