using UnityEngine;
using Data;

namespace Components
{
    public enum AttackType { SimpleAttack, PowerAttack, DashAttack }
    public abstract class DamageDealer : MonoBehaviour, IDamageDealler
    {
        [SerializeField] protected Transform _damageDealerPivot;
        [SerializeField] protected Collider _damageDealerCollider;
        [SerializeField] protected GameObject _render;
        [Space]
        [SerializeField] protected AttackDataListSO _attackDataList;
        [SerializeField] protected AnimationCurveDataSO _attackAnimationData;


        protected AttackDataSO _requestedAttackData;
        protected Vector3 _attackDirection;

        public AnimationCurveDataSO AttackAnimationData => _attackAnimationData;

        public AttackDataSO LastAttackData => _requestedAttackData;
        public AttackDataSO AttackData() => _attackDataList.GetRandomAttackData();

        protected virtual void Awake()
        {
            ToggleDamageDealer(false);
            _requestedAttackData = _attackDataList.GetRandomAttackData();
        }
        public virtual AttackDataSO PerformAttack(Vector3 rotateDirection)
        {
            _requestedAttackData = AttackData();
            _attackDirection = rotateDirection;
            RotateDamageDealer(rotateDirection);
            return _requestedAttackData;
        }
        public virtual void StartAttack()
        {
            ToggleDamageDealer(true);
        }
        public virtual void FinishAttack()
        {
            ToggleDamageDealer(false);
        }
        protected virtual void ToggleDamageDealer(bool value)
        {
            _damageDealerCollider.enabled = value;
            _render.SetActive(value);
        }
        protected virtual void RotateDamageDealer(Vector3 rotateDirection)
        {
            Quaternion rotation = Quaternion.LookRotation(rotateDirection);
            _damageDealerPivot.rotation = Quaternion.Euler(0f, rotation.eulerAngles.y, 0f);
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            IDamagable damagable = other.GetComponent<IDamagable>();
            damagable?.TakeDamage(_requestedAttackData, transform.position, damagable);
        }

    }
}