using UnityEngine;

namespace Utilities
{
    public class FPSLimiter : MonoBehaviour
    {
        [SerializeField][Range(30, 999)] private int _targetFPS = 60;
        private void Awake()
        {
            LimitFPS(_targetFPS);
        }
        private void LimitFPS(int targetFPS)
        {
            Application.targetFrameRate = targetFPS;
        }
    }
}