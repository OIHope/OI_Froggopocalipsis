using Core.System;
using System;
using System.Collections;
using UnityEngine;

namespace Core.Language
{
    public enum GameLanguage
    {
        English, Ukrainian
    }
    public class LanguageManager : Manager
    {
        public static LanguageManager Instance {  get; private set; }
        public Action<GameLanguage> OnRequestToChangeGameLanguage;

        [SerializeField] private GameLanguage _gameLanguage;

        public GameLanguage CurrentGameLanguage => _gameLanguage;

        private void Awake()
        {
            SingletonMethod();
        }
        public override IEnumerator InitManager()
        {
            OnRequestToChangeGameLanguage += ChangeGameLanguage;
            yield return null;
        }
        public override IEnumerator SetupManager()
        {
            yield return null;
        }

        private void ChangeGameLanguage(GameLanguage request)
        {
            if (_gameLanguage == request) return;
            _gameLanguage = request;

            GameEventsBase.OnLanguageChanged?.Invoke();
        }

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