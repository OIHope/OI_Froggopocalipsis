using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Components
{
    public class DetectTargetComponent : MonoBehaviour
    {

        [SerializeField] private Collider _triggerZoneCollider;

        private Transform _targetTransform;
        private IAttackableTarget _target;

        public System.Action<Transform> OnTargetDetected;
        public System.Action OnTargetLost;
        public Transform TargetTransform => _targetTransform;

        public void EnableTargetDetection()
        {
            _target = null;
            _targetTransform = null;
            _triggerZoneCollider.enabled = true;
        }
        public void DisableTargetDetection()
        {
            _triggerZoneCollider.enabled = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            IAttackableTarget target = other.GetComponent<IAttackableTarget>();
            Transform targetTransform = target?.InstanceTransform;
            if (targetTransform != null && target.TargetIsAlive)
            {
                _target = target;
                _targetTransform = targetTransform;
                OnTargetDetected?.Invoke(targetTransform);
            }
        }
        private void Update()
        {
            if (_target != null && !_target.TargetIsAlive)
            {
                OnTargetLost.Invoke();
            }
        }
    }
}