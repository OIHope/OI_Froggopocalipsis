using Core.DialogueSystem;
using Core.System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Progression
{
    public class ResetProgression : Manager
    {
        public static ResetProgression Instance { get; private set; }
        public Action OnGameStageResetRequested;

        [SerializeField] private List<DialogueBlockData> _allDialogueBlocksInGame;

        private void Awake()
        {
            SingletonAwakeMethod();
        }
        public void ResetAllGameData()
        {
            ResetDialogues();
            ResetGameStageProgresstion();
            ResetPlayerProgression();
        }
        private void ResetDialogues()
        {
            foreach(DialogueBlockData block in _allDialogueBlocksInGame)
            {
                block.ResetDialogue();
            }
        }
        private void ResetGameStageProgresstion()
        {
            OnGameStageResetRequested?.Invoke();
        }
        private void ResetPlayerProgression()
        {
            GameEventsBase.OnGameReset?.Invoke();
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

        public override IEnumerator InitManager()
        {
            yield return null;
        }

        public override IEnumerator SetupManager()
        {
            ResetAllGameData();
            yield return null;
        }
    }
}