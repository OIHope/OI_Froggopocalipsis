using Core.System;
using Data;
using System;
using System.Collections;
using UnityEngine;

namespace Core.Progression
{
    public class PlayerProgressionManager : Manager
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


        public override IEnumerator InitManager()
        {
            OnPlayerEarnSomeEXP += GivePlayerSomeEXP;
            OnPlayerWantsToUpSkill += LevelUpSkill;
            _progressionData.ResetValues();

            GameEventsBase.OnGameReset += () => _progressionData.ResetAllData();
            yield return null;
        }

        public override IEnumerator SetupManager()
        {
            yield return null;
        }

        private void Awake()
        {
            SingletonMethod();
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
                //DontDestroyOnLoad(gameObject);
            }
        }
    }
}