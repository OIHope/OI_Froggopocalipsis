using UnityEngine;

namespace Core.Camera
{
    [CreateAssetMenu(menuName = ("Level/Camera/Borders Data"))]
    public class CameraData : ScriptableObject
    {
        [SerializeField] private Vector2 _bordersX;
        [SerializeField] private Vector2 _bordersZ;

        [SerializeField] private float _hight;

        public Vector2 GetBordersX => _bordersX;
        public Vector2 GetBordersZ => _bordersZ;

        public float GetCameraHight => _hight;
    }
}