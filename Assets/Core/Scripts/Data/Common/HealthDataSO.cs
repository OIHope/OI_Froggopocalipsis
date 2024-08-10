using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = ("Health DataSO"), menuName = ("Data/Common/Health Data"))]
    public class HealthDataSO : ScriptableObject
    {
        [Header("Health")]
        [Space]
        [SerializeField] private int _startHp;
        [SerializeField] private int _maxHp;

        public int StartHP => _startHp;
        public int MaxHP => _maxHp;
    }
}