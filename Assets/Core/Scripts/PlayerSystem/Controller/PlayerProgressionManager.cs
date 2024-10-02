using Data;
using System;
using UnityEngine;

namespace Core.Progression
{
    public class PlayerProgressionManager : MonoBehaviour
    {
        public static PlayerProgressionManager Instance { get; private set; }

        public Action<int> OnPlayerEarnSomeEXP;
        public Action<int> OnPlayerLavelUP;
        public Action<PlayerSkill> OnPlayerWantsToUpSkill;
        public Action OnOpenLevelUpMenu;
        public Action OnCloseLevelUpMenu;

        [SerializeField] private PlayerProgressionData _progressionData;

        public PlayerProgressionData SkillData() => _progressionData;
        public bool CanUseSkillPointOnSkill(PlayerSkill skill) => _progressionData.CanLevelUpSkill(skill);

        private void Awake()
        {
            SingletonMethod();
            OnPlayerEarnSomeEXP += GivePlayerSomeEXP;
            OnPlayerWantsToUpSkill += LevelUpSkill;
            _progressionData.ResetValues();

            GameEventsBase.OnGameReset += () => _progressionData.ResetAllData();
        }

        private void LevelUpSkill(PlayerSkill skill) => _progressionData.LevelUpSkill(skill);
        private void GivePlayerSomeEXP(int value) => _progressionData.AddExperience(value);

        private void SingletonMethod()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}