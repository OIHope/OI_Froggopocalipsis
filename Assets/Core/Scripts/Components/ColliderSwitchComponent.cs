using System.Collections.Generic;
using UnityEngine;

namespace Components
{
    public class ColliderSwitchComponent : ComponentBase
    {
        private Collider _ñollider;
        private List<LayerMask> _excludeLayers;

        private bool _isColliding = true;


        public void DontCollideWithLayers()
        {
            if (!_isColliding) return;

            foreach(LayerMask layer in _excludeLayers)
            {
                _ñollider.excludeLayers -= layer;
            }
            
            _isColliding = false;
        }
        public void CollideWithLayers()
        {
            if (_isColliding) return;

            foreach (LayerMask layer in _excludeLayers)
            {
                _ñollider.excludeLayers += layer;
            }
            _isColliding = true;
        }

        public ColliderSwitchComponent(Collider ñollider, List<LayerMask> excludeLayers)
        {
            _ñollider = ñollider;
            _excludeLayers = excludeLayers;
        }
        public override void UpdateComponent() { }
    }
}