using UnityEngine;
using Data;

namespace Components
{
    public enum AttackType { SimpleAttack, PowerAttack, DashAttack }
    public class DamageDealer : MonoBehaviour, IDamageDealler
    {
        [SerializeField] private Transform _damageDealerPivot;
        [SerializeField] private Collider _damageDealerCollider;
        [SerializeField] private GameObject _render;
        [Space]
        [SerializeField] private AttackDataListSO _attackDataList;
        [SerializeField] private AnimationCurveDataSO _attackAnimationData;


        private AttackDataSO _requestedAttackData;

        public AnimationCurveDataSO AttackAnimationData => _attackAnimationData;

        public AttackDataSO LastAttackData => _requestedAttackData;
        public AttackDataSO AttackData() => _attackDataList.GetRandomAttackData();

        private void Awake()
        {
            ToggleDamageDealer(false);
            _requestedAttackData = _attackDataList.GetRandomAttackData();
        }
        public AttackDataSO PerformAttack(Vector3 rotateDirection)
        {
            _requestedAttackData = AttackData();
            RotateDamageDealer(rotateDirection);
            return _requestedAttackData;
        }
        public void StartAttack()
        {
            ToggleDamageDealer(true);
        }
        public void FinishAttack()
        {
            ToggleDamageDealer(false);
        }
        private void ToggleDamageDealer(bool value)
        {
            _damageDealerCollider.enabled = value;
            _render.SetActive(value);
        }
        private void RotateDamageDealer(Vector3 rotateDirection)
        {
            Quaternion rotation = Quaternion.LookRotation(rotateDirection);
            _damageDealerPivot.rotation = Quaternion.Euler(0f, rotation.eulerAngles.y, 0f);
        }

        private void OnTriggerEnter(Collider other)
        {
            IDamagable damagable = other.GetComponent<IDamagable>();
            damagable?.TakeDamage(_requestedAttackData, transform.position, damagable);
        }

    }
}