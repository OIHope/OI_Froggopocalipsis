using UnityEngine;

namespace Components
{
    public class DetectTargetComponent : MonoBehaviour
    {

        [SerializeField] private Collider _triggerZoneCollider;

        private Transform _targetTransform;

        public System.Action<Transform> OnTargetDetected;
        public Transform TargetTransform => _targetTransform;

        public void EnableTargetDetection()
        {
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
            if (targetTransform != null)
            {
                _targetTransform = targetTransform;
                OnTargetDetected?.Invoke(targetTransform);
            }
        }
    }
}