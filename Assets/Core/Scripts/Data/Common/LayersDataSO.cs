using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName ="Data/Common/Layers Data")]
    public class LayersDataSO : ScriptableObject
    {
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private LayerMask _enemyLayer;

        public LayerMask GroundLayer => _groundLayer;
        public LayerMask PlayerLayer => _playerLayer;
        public LayerMask EnemyLayer => _enemyLayer;
    }
}