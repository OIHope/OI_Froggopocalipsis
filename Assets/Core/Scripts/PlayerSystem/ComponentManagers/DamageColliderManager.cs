using UnityEngine;

namespace PlayerSystem
{
    public class DamageColliderManager : MonoBehaviour
    {
        [SerializeField] private Collider _damageCollider;
        [SerializeField] private LayerMask _enemyLayer;

        [SerializeField] private bool _isCollidingWithEnemy = true;

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
    }
}