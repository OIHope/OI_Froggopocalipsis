using Core.DialogueSystem;
using Core.System;
using System.Collections;
using UnityEngine;

namespace Core.Progression
{
    public enum GameStage
    {
        Intro, Arrival, ArrivalComplete,
        FieldEnter, FieldDeath, FieldComplete, 
        SwampEnter, SwampDeath, SwampComplete,
        ForestEnter, ForestDeath, ForestComplete,
        Outro
    }
    public class GameStageManager : Manager
    {
        public static GameStageManager Instance { get; private set; }
        public GameStage CurrentGameStage => _currentStageData.CurrentGameStage;

        [SerializeField] private GameStageData _currentStageData;

        public override IEnumerator InitManager()
        {
            ResetGameStage();
            yield return null;
        }

        public override IEnumerator SetupManager()
        {
            GameEventsBase.OnReachNewGameStage += ChangeGameStage;
            DialogueManager.Instance.OnDialogueFinish += ChangeGameStage;
            ResetProgression.Instance.OnGameStageResetRequested += ResetGameStage;
            yield return null;
        }

        private void Awake()
        {
            SingletonAwakeMethod();
        }

        private void ChangeGameStage(GameStage stage)
        {
            _currentStageData.CurrentGameStage = stage;
        }

        private void ResetGameStage()
        {
            _currentStageData.ResetGameStage();
        }

        private void SingletonAwakeMethod()
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