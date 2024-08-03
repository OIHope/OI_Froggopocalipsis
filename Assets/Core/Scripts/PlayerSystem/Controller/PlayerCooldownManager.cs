using System.Collections;
using UnityEngine;

namespace PlayerSystem
{
    public class PlayerCooldownManager : MonoBehaviour
    {
        private bool _attackIsReady = true;
        private bool _dashIsReady = true;

        public bool CanAttack => _attackIsReady;
        public bool CanDash => _dashIsReady;

        public void ReloadAttack(float duration)
        {
            if (!_attackIsReady) return;
            _attackIsReady = false;
            StartCoroutine(ReloadingAttack(duration));
        }
        public void ReloadDash(float duration)
        {
            if (!_dashIsReady) return;
            _dashIsReady = false;
            StartCoroutine(ReloadingDash(duration));
        }
        private IEnumerator ReloadingAttack(float duration)
        {
            float attackCooldownElapsedTime = 0f;
            while(attackCooldownElapsedTime < duration)
            {
                attackCooldownElapsedTime += Time.deltaTime;
                yield return null;
            }
            _attackIsReady = true;
        }
        private IEnumerator ReloadingDash(float duration)
        {
            float _dashCooldownElapsedTime = 0f;
            while (_dashCooldownElapsedTime < duration)
            {
                _dashCooldownElapsedTime += Time.deltaTime;
                yield return null;
            }
            _dashIsReady = true;
        }
    }
}