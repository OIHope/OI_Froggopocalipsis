using Data;
using System.Collections.Generic;
using UnityEngine;

namespace Components
{
    public class ColliderSwitchComponent : ComponentBase
    {
        private Collider _�ollider;
        private LayersDataSO _maskLayers;

        private bool _isColliding = true;


        public void DontCollideWithLayers()
        {
            if (!_isColliding) return;

            _�ollider.excludeLayers = _maskLayers.ExcludeLayers;

            _isColliding = false;
        }
        public void CollideWithLayers()
        {
            if (_isColliding) return;

            _�ollider.excludeLayers = _maskLayers.IncludeLayers;
            _isColliding = true;
        }

        public ColliderSwitchComponent(Collider �ollider, LayersDataSO excludeLayers)
        {
            _�ollider = �ollider;
            _maskLayers = excludeLayers;
        }
        public override void UpdateComponent() { }
    }
}