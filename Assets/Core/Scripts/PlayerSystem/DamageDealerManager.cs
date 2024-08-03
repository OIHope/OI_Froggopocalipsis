using UnityEngine;
using System.Collections;

namespace PlayerSystem
{
    public class DamageDealerManager : MonoBehaviour
    {
        [SerializeField] private GameObject _damageDealer;
        [SerializeField] private float _damageDealerDistance = 2f;
        [SerializeField] private Vector3 _damageDealerInitPos = new Vector3(0f, 0.2f, 0f);

        [SerializeField] private int _basickAttackDamage = 10;

        public void ActivateDamageDealer(Vector3 playerPosition, Vector3 lookDirection)
        {
            _damageDealer.SetActive(true);
            PositionDamageDealer(playerPosition, lookDirection);
            StartCoroutine(DeactivateAfterDelay());
        }

        private IEnumerator DeactivateAfterDelay()
        {
            yield return new WaitForSeconds(0.1f);
            _damageDealer.SetActive(false);
        }

        private void PositionDamageDealer(Vector3 playerPosition, Vector3 lookDirection)
        {
            Vector3 position = playerPosition + _damageDealerInitPos + lookDirection * _damageDealerDistance;
            Quaternion rotation = Quaternion.LookRotation(lookDirection);
            _damageDealer.transform.SetPositionAndRotation(position, rotation);
        }
        private void OnTriggerEnter(Collider other)
        {
            IDamagable damagable = other.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(_basickAttackDamage);
            }
        }
    }
}