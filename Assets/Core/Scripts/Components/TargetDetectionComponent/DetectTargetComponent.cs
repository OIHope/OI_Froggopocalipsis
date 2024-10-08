using Data;
using EnemySystem;
using UnityEngine;
using System;

namespace Components
{
    public class DetectTargetComponent : MonoBehaviour
    {
        [SerializeField] private bool _triggeredByGoodGuys = true;
        [SerializeField] private bool isWorking = true;
        [SerializeField] private Collider _triggerZoneCollider;

        private EnemyControllerDataAccessor _dataAccessor;
        private bool _init = false;

        public bool HasTarget => _dataAccessor.Target != null && _dataAccessor.Target.TargetIsAlive;

        public void InitComponent(EnemyControllerDataAccessor dataAccessor)
        {
            _dataAccessor = dataAccessor;
            EnableTargetDetection();
            _init = true;
        }

        public void EnableTargetDetection()
        {
            _dataAccessor.Target = null;
            _triggerZoneCollider.enabled = true;
        }
        public void DisableTargetDetection()
        {
            _triggerZoneCollider.enabled = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!_init) return;
            if (!isWorking) return;

            if (!other.TryGetComponent<IAttackableTarget>(out var target)) return;
            if (target.TargetIsAlive && _triggeredByGoodGuys == target.ThisTargetIsGoodSide)
            {
                _dataAccessor.Target = target;
                DisableTargetDetection();
            }
        }
        private void Update()
        {
            if (!_init) return;
            if (!isWorking) return;

            if (!HasTarget && _triggerZoneCollider.enabled == false)
            {
                EnableTargetDetection();
            }
        }
    }
}