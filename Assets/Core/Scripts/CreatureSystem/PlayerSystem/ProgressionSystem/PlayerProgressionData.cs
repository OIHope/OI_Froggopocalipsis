using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Progression
{
    public enum PlayerSkill
    {
        AttackPower, CriticalDamageChance, AttackCooldown, DodgeCooldown, MoveSpeed, MaxHP
    }

    [CreateAssetMenu(menuName =("Data/Player/Progression Data"))]
    public class PlayerProgressionData : ScriptableObject
    {
        [Header("Just inspector data")]
        [SerializeField] private int _currentEXP = 0;
        [SerializeField] private int _currentLVL = 0;
        [SerializeField] private int _levelUpPoints = 0;

        [SerializeField] private AnimationCurve _levelUpCurve;

        [Header("Skills")]
        [SerializeField] private int _attackPowerPoints = 0;
        [SerializeField] private AnimationCurve _attackPowerCurve;

        [SerializeField] private int _critChancePoints = 0;
        [SerializeField] private AnimationCurve _critChanceCurve;

        [SerializeField] private int _attackCooldownPoints = 0;
        [SerializeField] private AnimationCurve _attackCooldownCurve;

        [SerializeField] private AttackDataSO _attackData;

        [SerializeField] private int _dodgeCooldownPoints = 0;
        [SerializeField] private AnimationCurve _dodgeCooldownCurve;

        [SerializeField] private DashDataSO _dodgeData;

        [SerializeField] private int _moveSpeedPoints = 0;
        [SerializeField] private AnimationCurve _moveSpeedCurve;

        [SerializeField] private MovementDataSO _movementData;

        [SerializeField] private int _maxHPPoints = 0;
        [SerializeField] private AnimationCurve _maxHPCurve;

        [SerializeField] private HealthDataSO _healthData;

        public int CurrentEXP => _currentEXP;
        public int CurrentLVL => _currentLVL;
        public int LevelUpPoints => _levelUpPoints;

        public int AttackPowerPoints => _attackPowerPoints;
        public int CritChancePoints => _critChancePoints;
        public int AttackCooldownPoints => _attackCooldownPoints;
        public int DodgeCooldownPoints => _dodgeCooldownPoints;
        public int MoveSpeedPoints => _moveSpeedPoints;
        public int MaxHPPoints => _maxHPPoints;

        public int AttackPowerValue => (int)_attackPowerCurve.Evaluate(_attackPowerPoints);
        public int CriticalDamageChanceValue => (int)_critChanceCurve.Evaluate(_critChancePoints);
        public float AttackCooldownValue => _attackCooldownCurve.Evaluate(_attackCooldownPoints);
        public float DodgeCooldownValue => _dodgeCooldownCurve.Evaluate(_dodgeCooldownPoints);
        public float MoveSpeedValue => _moveSpeedCurve.Evaluate(_moveSpeedPoints);
        public int MaxHPValue => (int)_maxHPCurve.Evaluate(_maxHPPoints);

        public void AddExperience(int expAmount)
        {
            if (IsMaxLevel()) return;

            _currentEXP += expAmount;

            if (_currentEXP >= GetRequiredEXPForNextLevel() && !IsMaxLevel())
            {
                _currentLVL++;
                _currentEXP = 0;
                _levelUpPoints++;
                PlayerProgressionManager.Instance.OnPlayerLavelUP?.Invoke(_currentLVL);
            }

            if (IsMaxLevel())
                _currentEXP = 0;
        }

        public void LevelUpSkill(PlayerSkill skill)
        {
            if (!CanLevelUpSkill(skill)) return;

            AddPointToSkill(skill);
        }

        public bool CanLevelUpSkill(PlayerSkill skill)
        {
            if (_levelUpPoints <= 0) return false;

            switch (skill)
            {
                case PlayerSkill.AttackPower:
                    return _attackPowerPoints < MaxSkillLevel(_attackPowerCurve);
                case PlayerSkill.CriticalDamageChance:
                    return _critChancePoints < MaxSkillLevel(_critChanceCurve);
                case PlayerSkill.AttackCooldown:
                    return _attackCooldownPoints < MaxSkillLevel(_attackCooldownCurve);
                case PlayerSkill.DodgeCooldown:
                    return _dodgeCooldownPoints < MaxSkillLevel(_dodgeCooldownCurve);
                case PlayerSkill.MoveSpeed:
                    return _moveSpeedPoints < MaxSkillLevel(_moveSpeedCurve);
                case PlayerSkill.MaxHP:
                    return _maxHPPoints < MaxSkillLevel(_maxHPCurve);
                default:
                    return false;
            }
        }

        private void AddPointToSkill(PlayerSkill skill)
        {
            if (_levelUpPoints <= 0) return;

            switch (skill)
            {
                case PlayerSkill.AttackPower:
                    _attackPowerPoints++;
                    _attackData.SetDamageValue(AttackPowerValue);
                    break;
                case PlayerSkill.CriticalDamageChance:
                    _critChancePoints++;
                    _attackData.SetCritChance(CriticalDamageChanceValue);
                    break;
                case PlayerSkill.AttackCooldown:
                    _attackCooldownPoints++;
                    _attackData.SetCooldownTime(AttackCooldownValue);
                    break;
                case PlayerSkill.DodgeCooldown:
                    _dodgeCooldownPoints++;
                    _dodgeData.SetCooldownTime(DodgeCooldownValue);
                    break;
                case PlayerSkill.MoveSpeed:
                    _moveSpeedPoints++;
                    _movementData.SetRunSpeed(MoveSpeedValue);
                    break;
                case PlayerSkill.MaxHP:
                    _maxHPPoints++;
                    _healthData.SetMaxHP(MaxHPValue);
                    PlayerManager.Instance.OnPlayerRestoreRequest?.Invoke();
                    break;
            }

            _levelUpPoints--;
        }

        public int GetRequiredEXPForNextLevel()
        {
            return (int)_levelUpCurve.Evaluate(_currentLVL + 1);
        }
        private int MaxSkillLevel(AnimationCurve skillCurve)
        {
            Keyframe lastKey = skillCurve.keys[skillCurve.keys.Length - 1];
            return (int)lastKey.time;
        }
        private bool IsMaxLevel()
        {
            return _currentLVL >= MaxSkillLevel(_levelUpCurve);
        }

        public void ResetValues()
        {
            _attackData.SetDamageValue(AttackPowerValue);
            _attackData.SetCritChance(CriticalDamageChanceValue);
            _attackData.SetCooldownTime(AttackCooldownValue);
            _dodgeData.SetCooldownTime(DodgeCooldownValue);
            _movementData.SetRunSpeed(MoveSpeedValue);
            _healthData.SetMaxHP(MaxHPValue);
            PlayerManager.Instance.OnPlayerRestoreRequest?.Invoke();
        }
        public void ResetAllData()
        {
            _currentLVL = 0;
            _currentEXP = 0;
            _levelUpPoints = 0;
            _attackCooldownPoints = 0;
            _attackPowerPoints = 0;
            _critChancePoints = 0;
            _dodgeCooldownPoints = 0;
            _maxHPPoints = 0;
            _moveSpeedPoints = 0;

            ResetValues();
        }
    }

}