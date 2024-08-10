using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName =("Data/Common/AttackData List"))]
    public class AttackDataListSO : ScriptableObject
    {
        [SerializeField] private List<AttackDataSO> _attackDataList;

        public List<AttackDataSO> AttackDataList => _attackDataList;
        public AttackDataSO GetRandomAttackData()
        {
            return _attackDataList[Random.Range(0, _attackDataList.Count)];
        }
    }
}