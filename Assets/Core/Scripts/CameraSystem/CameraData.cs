using UnityEngine;

namespace Core.Camera
{
    [CreateAssetMenu(menuName = ("Level/Camera/Borders Data"))]
    public class CameraData : ScriptableObject
    {
        [SerializeField] private Vector2 _bordersX;
        [SerializeField] private Vector2 _bordersZ;

        [SerializeField] private float _hight;
        [SerializeField] private float _tilt;

        [SerializeField] private float _depthOffset;

        public Vector2 GetBordersX => _bordersX;
        public Vector2 GetBordersZ => _bordersZ;

        public float GetCameraHight => _hight;
        public float GetCameraTilt => _tilt;
        public float GetCameraDepthOffset => _depthOffset;
    }
}