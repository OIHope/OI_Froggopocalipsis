using Data;
using UnityEngine;

namespace Components
{
    public class HealthComponent : ComponentBase
    {
        private HealthDataSO _healthData;
        private int _currentHealth;

        public System.Action<HealthComponent> OnDeath;
        public int CurrentHP => _currentHealth;
        public bool CriticalCondition
        {
            get
            {
                int limit = (int)(_healthData.MaxHP * 0.2f);
                return _currentHealth <= limit;
            }
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                OnDeath?.Invoke(this);
            }
            UpdateProgressBar(_currentHealth, _healthData.MaxHP);
        }
        public void Heal(HealthComponent healthComponent, int healValue)
        {
            _currentHealth += healValue;
            if (_currentHealth >= _healthData.MaxHP)
            {
                _currentHealth = _healthData.MaxHP;
            }
            UpdateProgressBar(_currentHealth, _healthData.MaxHP);
        }

        public override void UpdateComponent() { }

        public HealthComponent(HealthDataSO healthData)
        {
            _healthData = healthData;
            _currentHealth = healthData.StartHP;

            _hasProgressBar = false;
        }
        public HealthComponent(HealthDataSO healthData, ProgressBarComponent healthBar)
        {
            _healthData = healthData;
            _currentHealth = healthData.StartHP;
            _progressBar = healthBar;

            _hasProgressBar = true;
            UpdateProgressBar(_currentHealth, _healthData.MaxHP);
        }
    }
}