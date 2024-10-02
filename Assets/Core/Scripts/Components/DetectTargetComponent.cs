using UnityEngine;

namespace Components
{
    public class DetectTargetComponent : MonoBehaviour
    {
        [SerializeField] private bool isWorking = true;
        [SerializeField] private Collider _triggerZoneCollider;

        private IAttackableTarget _target;

        public System.Action<IAttackableTarget> OnTargetDetected;
        public System.Action OnTargetLost;

        public void EnableTargetDetection()
        {
            _target = null;
            _triggerZoneCollider.enabled = true;
        }
        public void DisableTargetDetection()
        {
            _triggerZoneCollider.enabled = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!isWorking) return;

            IAttackableTarget target = other.GetComponent<IAttackableTarget>();
            if (target == null) return;
            if (target.TargetIsAlive)
            {
                _target = target;
                OnTargetDetected?.Invoke(target);
            }
        }
        private void Update()
        {
            if (!isWorking) return;

            if (_target != null && !_target.TargetIsAlive)
            {
                OnTargetLost.Invoke();
            }
        }
    }
}