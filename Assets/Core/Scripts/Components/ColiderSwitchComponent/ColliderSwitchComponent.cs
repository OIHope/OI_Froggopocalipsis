using Data;
using System.Collections.Generic;
using UnityEngine;

namespace Components
{
    public class ColliderSwitchComponent : ComponentBase
    {
        private Collider _ñollider;
        private LayersDataSO _maskLayers;

        private bool _isColliding = true;


        public void DontCollideWithLayers()
        {
            if (!_isColliding) return;

            _ñollider.excludeLayers = _maskLayers.ExcludeLayers;

            _isColliding = false;
        }
        public void CollideWithLayers()
        {
            if (_isColliding) return;

            _ñollider.excludeLayers = _maskLayers.IncludeLayers;
            _isColliding = true;
        }

        public ColliderSwitchComponent(Collider ñollider, LayersDataSO excludeLayers)
        {
            _ñollider = ñollider;
            _maskLayers = excludeLayers;
        }
        public override void UpdateComponent() { }
    }
}