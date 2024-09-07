using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName ="Data/Common/Layers Data")]
    public class LayersDataSO : ScriptableObject
    {
        [SerializeField] private LayerMask _groundLayer;

        [SerializeField] private LayerMask _excludeLayer;
        [SerializeField] private LayerMask _includeLayer;

        public LayerMask GroundLayer => _groundLayer;
        public LayerMask ExcludeLayers => _excludeLayer;
        public LayerMask IncludeLayers => _includeLayer;
    }
}