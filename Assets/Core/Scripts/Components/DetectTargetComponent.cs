using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Components
{
    public class DetectTargetComponent : MonoBehaviour
    {

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
            if (_target != null && !_target.TargetIsAlive)
            {
                OnTargetLost.Invoke();
            }
        }
    }
}