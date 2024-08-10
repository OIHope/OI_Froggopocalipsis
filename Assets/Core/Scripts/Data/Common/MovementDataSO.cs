using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = ("Movement DataSO"), menuName = ("Data/Common/Movement Data"))]
    public class MovementDataSO : ScriptableObject
    {
        [Header("Movement")]
        [Space]
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _runSpeed;

        public float WalkSpeed => _walkSpeed;
        public float RunSpeed => _runSpeed;
    }
}