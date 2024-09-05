using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName ="Data/Common/Layers Data")]
    public class LayersDataSO : ScriptableObject
    {
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private LayerMask _creatureLayer;
        [Space] 
        [SerializeField] private List<LayerMask> _layerMasks;

        public LayerMask GroundLayer => _groundLayer;
        public LayerMask CreatureLayer => _creatureLayer;
        public List<LayerMask> LayerMasks => _layerMasks;
    }
}