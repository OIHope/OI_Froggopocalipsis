using UnityEngine;
using System.Collections;
using BehaviourSystem;

namespace PlayerSystem
{
    public class DamageDealerManager : MonoBehaviour
    {
        [SerializeField] private Transform _damageDealerPivot;
        [SerializeField] private Collider _damageDealerCollider;
        [SerializeField] private GameObject _render;

        private AttackDataSO _attackDataSO;

        private void Awake()
        {
            ToggleDamageDealer(false);
        }
        public void PerformAttack(AttackDataSO attackDataSO, Vector3 rotateDirection)
        {
            _attackDataSO = attackDataSO;
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
            if (damagable != null)
            {
                damagable.TakeDamage(GetAttackDamage());
            }
        }
        private int GetAttackDamage()
        {
            int chance = Random.Range(1, 101);
            return chance <= _attackDataSO.CritChance ? _attackDataSO.CritDamage : _attackDataSO.Damage;
        }
    }
}