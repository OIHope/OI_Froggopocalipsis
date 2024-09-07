using System.Collections;
using UnityEngine;

namespace Components
{
    public class ProjectileDamageDealer : DamageDealer
    {
        [Space]
        [SerializeField] private Transform _parent;
        [SerializeField] private Transform _projectile;
        [SerializeField] private float _speed;
        [SerializeField] private AnimationCurve _trajectory;

        public override void StartAttack()
        {
            base.StartAttack();
            StartCoroutine(ShootingProjectile());
        }
        public override void FinishAttack() { }
        protected override void ToggleDamageDealer(bool value)
        {
            base.ToggleDamageDealer(value);
            if (!value) ResetProjectile();
        }
        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);

            StopCoroutine(ShootingProjectile());
            ToggleDamageDealer(false);
        }
        private IEnumerator ShootingProjectile()
        {
            _projectile.parent = null;
            _projectile.position = _parent.position;

            float duration = Utilities.AnimationCurveDuration.Duration(_trajectory);
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                Vector3 currentPos = _projectile.position;
                Vector3 offsetPos = new(_attackDirection.x, _trajectory.Evaluate(elapsedTime), _attackDirection.z);
                Vector3 targetPos = currentPos + (_speed * Time.deltaTime * offsetPos);

                _projectile.position = targetPos;

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            ToggleDamageDealer(false);
        }
        private void ResetProjectile()
        {
            _projectile.parent = _parent;
            _projectile.position = Vector3.zero;
        }
    }
}