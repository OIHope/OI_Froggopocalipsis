using System.Collections.Generic;
using UnityEngine;

namespace Components
{
    public class ColliderSwitchComponent : ComponentBase
    {
        private Collider _�ollider;
        private List<LayerMask> _excludeLayers;

        private bool _isColliding = true;


        public void DontCollideWithLayers()
        {
            if (!_isColliding) return;

            foreach(LayerMask layer in _excludeLayers)
            {
                _�ollider.excludeLayers -= layer;
            }
            
            _isColliding = false;
        }
        public void CollideWithLayers()
        {
            if (_isColliding) return;

            foreach (LayerMask layer in _excludeLayers)
            {
                _�ollider.excludeLayers += layer;
            }
            _isColliding = true;
        }

        public ColliderSwitchComponent(Collider �ollider, List<LayerMask> excludeLayers)
        {
            _�ollider = �ollider;
            _excludeLayers = excludeLayers;
        }
        public override void UpdateComponent() { }
    }
}