using System.Collections;
using UnityEngine;

namespace Components
{
    public class CooldownComponent : ComponentBase
    {
        private float _cooldownTimer = 1f;
        private float _cooldownTimerDuration = 1f;

        public bool CanUseAbility => _cooldownTimer >= _cooldownTimerDuration;

        public void Cooldown(float duration)
        {
            if (!CanUseAbility) return;

            _cooldownTimer = 0f;
            _cooldownTimerDuration = duration;
        }

        private IEnumerator ExecuteCooldown(float duration)
        {
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                UpdateProgressBar(elapsedTime, duration);
                yield return null;
            }
        }

        public override void UpdateComponent()
        {
            if (CanUseAbility) return;
            _cooldownTimer += Time.deltaTime;
            UpdateProgressBar(_cooldownTimer, _cooldownTimerDuration);
        }

        public CooldownComponent(ProgressBarComponent progressBar)
        {
            _progressBar = progressBar;
            _hasProgressBar = true;
        }
        public CooldownComponent()
        {
            _hasProgressBar = false;
        }
    }
}