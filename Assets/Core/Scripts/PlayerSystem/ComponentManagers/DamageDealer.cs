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

        [SerializeField] private AttackDataListSO _simpleAttackDataList;
        [SerializeField] private AttackDataListSO _powerAttackDataList;
        [SerializeField] private AttackDataListSO _dashAttackDataList;

        private AttackType _requestedAttackType;
        private AttackDataSO _lastAttackData;

        public AttackDataSO LastAttackData => _lastAttackData;

        private void Awake()
        {
            ToggleDamageDealer(false);
            _requestedAttackType = AttackType.SimpleAttack;
        }
        public void PerformAttack(Vector3 rotateDirection, AttackType attackType)
        {
            _requestedAttackType = attackType;
            RotateDamageDealer(rotateDirection);
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
            damagable?.TakeDamage(AttackData(), transform.position);
        }

        public AttackDataSO AttackData()
        {
            _lastAttackData = _requestedAttackType switch
            {
                AttackType.SimpleAttack => _simpleAttackDataList.GetRandomAttackData(),
                AttackType.PowerAttack => _powerAttackDataList.GetRandomAttackData(),
                AttackType.DashAttack => _dashAttackDataList.GetRandomAttackData(),
                _ => _simpleAttackDataList.GetRandomAttackData(),
            };
            return _lastAttackData;
        }
    }
}