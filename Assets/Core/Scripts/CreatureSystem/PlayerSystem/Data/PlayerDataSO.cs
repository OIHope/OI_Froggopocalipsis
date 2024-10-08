using Data;
using UnityEngine;

namespace PlayerSystem
{
    [CreateAssetMenu(fileName = "PlayerDataSO", menuName = "Data/Player/Player Data")]
    public class PlayerDataSO : CreatureDataSO
    {
        [Space]
        [Header("Additional Data")]
        [Space]
        [SerializeField] private DashDataSO _dashData;

        public DashDataSO DashData => _dashData;
    }
}