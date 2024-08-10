using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = ("Vision DataSO"), menuName = ("Data/Enemy/Vision Data"))]
    public class EnemyVisionDataSO : ScriptableObject
    {
        [Header("Vision")]
        [Space]
        [SerializeField] private float _sightDistance;
        [SerializeField] private float _timeToSpotTarget;

        public float SightDistance => _sightDistance;
        public float TimeToSpotTarget => _timeToSpotTarget;
    }
}