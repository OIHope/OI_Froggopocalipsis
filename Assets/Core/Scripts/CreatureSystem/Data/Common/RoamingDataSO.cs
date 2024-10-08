using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName =("Data/Common/Roaming DataSO"))]
    public class RoamingDataSO : ScriptableObject
    {
        [Header("Defies if will roam around or stand still")]
        [SerializeField] private bool _static = false;
        [Space]
        [Header("Only if is not static")]
        [SerializeField] private float _waitTillRoamTime;
        [Space]
        [SerializeField] private int _roamingDistance;
        [SerializeField] private float _checkPointDistance;
        [SerializeField] private float _stopDistance;

        public bool IsStatic => _static;
        public float WaitTillRoamTime => _waitTillRoamTime;
        public int RoamingDistance => _roamingDistance;
        public float CheckPointDistance => _checkPointDistance;
        public float StopDistance => _stopDistance;
    }
}