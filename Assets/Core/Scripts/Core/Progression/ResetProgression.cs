using Core.DialogueSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Progression
{
    public class ResetProgression : MonoBehaviour
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
            GameEventsBase.OnGameReset?.Invoke();
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
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}