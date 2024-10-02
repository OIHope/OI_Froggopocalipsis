using Core.Language;
using UnityEngine;
using System;

namespace Core.DialogueSystem
{
    [Serializable]
    public class DialogueData
    {
        [SerializeField] private string _englishLine;
        [SerializeField] private string _ukrainianLine;

        [SerializeField] private SpeakerSpriteData _speakerData;

        [SerializeField] private SpeakerEmotion _thisLineEmotion;

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
        public string SpeakerName => _speakerData.SpeakerName;
        public Sprite SpeakerSprite => _speakerData.SpeakerSprite(_thisLineEmotion);
    }
}