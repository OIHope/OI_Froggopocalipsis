using System;
using UnityEngine;

namespace Core.Language
{
    [Serializable]
    public class TextData
    {
        [SerializeField] private string _englishLine;
        [SerializeField] private string _ukrainianLine;

        public string Line
        {
            get
            {
                GameLanguage currentLanguage = LanguageManager.Instance.CurrentGameLanguage;
                return currentLanguage switch
                {
                    GameLanguage.English => _englishLine,
                    GameLanguage.Ukrainian => _ukrainianLine,
                    _ => _englishLine,
                };
            }
        }
    }
}