using UnityEngine;

namespace Utilities
{
    public static class AnimationCurveDuration
    {
        public static float Duration(AnimationCurve animationCurve)
        {
            Keyframe maxTimeKeyframe = animationCurve.keys[0];
            foreach (Keyframe key in animationCurve.keys)
            {
                if (key.time > maxTimeKeyframe.time) maxTimeKeyframe = key;
            }
            return maxTimeKeyframe.time;
        }
    }
}