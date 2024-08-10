using UnityEngine;

namespace Components
{
    public class ColliderSwitchComponent : ComponentBase
    {
        private Collider _damageCollider;
        private LayerMask _enemyLayer;

        private bool _isCollidingWithEnemy = true;


        public void DontCollideWithEnemy()
        {
            if (!_isCollidingWithEnemy) return;

            _damageCollider.excludeLayers -= _enemyLayer;
            _isCollidingWithEnemy = false;
        }
        public void CollideWithEnemy()
        {
            if (_isCollidingWithEnemy) return;

            _damageCollider.excludeLayers += _enemyLayer;
            _isCollidingWithEnemy = true;
        }

        public ColliderSwitchComponent(Collider damageCollider, LayerMask enemyLayer)
        {
            _damageCollider = damageCollider;
            _enemyLayer = enemyLayer;
        }
        public override void UpdateComponent() { }
    }
}